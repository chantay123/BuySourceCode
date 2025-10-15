using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebBanNongSan.Dto.Response;
using WebBuySource.Dto.Request;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;
using WebBuySource.Models;
using WebBuySource.Utilities;
using WebBuySource.Utilities.Constants;


namespace WebBuySource.Services
{
    public class JwtService : BaseService, IJwtService
    {
        private static readonly Dictionary<string, string> RefreshTokens = new();
        #region Repository references
        /// <summary>
        /// Repository for accessing and managing user data.
        /// </summary>
        private IRepository<User> UserRepository => UnitOfWork.UserRepository;
        #endregion  

        private readonly IConfiguration _config;

        public JwtService(IUnitOfWork unitOfWork, IConfiguration config) : base(unitOfWork)
        {
            _config = config;
        }

        /// <summary>
        /// Registers a new user with encrypted password and default role.
        /// </summary>
        /// <param name="request">Registration data provided by the client.</param>
        /// <returns>API response indicating registration success or failure.</returns>
        public async Task<BaseAPIResponse> Register(RegisterRequestDTO request)
        {
            // Validate password confirmation
            if (request.Password != request.ConfirmPassword)
                return BaseApiResponse.Error(MessageConstants.PASSWORD_NOT_MATCH);

            // Check if username or email already exists
            var existingUser = UserRepository.GetAllAsNoTracking()
                .FirstOrDefault(u => u.Email == request.Email || u.Email == request.Email);

            if (existingUser != null)
                return BaseApiResponse.Error(MessageConstants.USERNAME_OR_EMAIL_EXISTS);

            //Hash the user's password using BCrypt
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

          // Create a new user entity with default role = User
            var newUser = new User
            {
                Username = request.Email.Trim(),
                Fullname = request.Fullname.Trim(),
                Password = hashedPassword,
                Email = request.Email.Trim(),
                RoleId = 1, 
                PhoneNumber = request.Phone,        
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            //Save the new user to the database
            await UserRepository.AddAsync(newUser).ConfigureAwait(false);
            await UserRepository.SaveChangesAsync();

            //Return success response
            return BaseApiResponse.OK(new
            {
                message = MessageConstants.REGISTER_SUCCESS,
                username = newUser.Username,
                email = newUser.Email
            });
        }



        /// <summary>
        /// Authenticates a user and generates a JWT token if credentials are valid.
        /// </summary>
        /// <param name="request">Login credentials (username and password).</param>
        /// <returns>API response containing JWT token and expiration info.</returns>
        public async Task<BaseAPIResponse> Login(LoginRequestDTO request)
        {
            //  Find user by username
            var user = UserRepository.GetAllAsNoTracking()
                .FirstOrDefault(u => u.Email == request.Email);

            if (user == null)
                return BaseApiResponse.NotFound(MessageConstants.USER_NOT_FOUND);

            //  Verify password using BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!isPasswordValid)
                return BaseApiResponse.Error(MessageConstants.INVALID_PASSWORD);

            //  Generate new access token and refresh token
            var accessToken = GenerateToken(user);
            var refreshToken = GenerateRefreshToken();

            //Store the refresh token (you can store it in DB or cache)
            RefreshTokens[user.Username] = refreshToken;

            //Return response to client
            return BaseApiResponse.OK(new AuthResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            }, MessageConstants.LOGIN_SUCCESS);
        }


        /// <summary>
        /// Generates a signed JWT access token for the authenticated user.
        /// </summary>
        /// <param name="user">User data to embed in the token claims.</param>
        /// <returns>Signed JWT token string.</returns>S
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;
            var expireMinutes = Convert.ToInt32(_config["Jwt:ExpireMinutes"]);
            var expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"]));
            var iat = new DateTimeOffset(now).ToUnixTimeSeconds();
            var exp = new DateTimeOffset(expires).ToUnixTimeSeconds();

            // Define claims (user identity and metadata)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Fullname.ToString()),
                new Claim("role", "User"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, iat.ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Exp, exp.ToString(), ClaimValueTypes.Integer64),
            };

            // Create and sign the token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<BaseAPIResponse> RefreshToken(RefreshTokenRequestDTO request)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(request.AccessToken);
            var username = jwtToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var expClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp)?.Value;

            string tokenStatusMessage = string.Empty;

            if (long.TryParse(expClaim, out var exp))
            {
                var expDate = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;

                if (expDate < DateTime.UtcNow)
                    tokenStatusMessage = MessageConstants.ACCESS_TOKEN_EXPIRED;
                else
                    tokenStatusMessage = MessageConstants.ACCESS_TOKEN_VALID;
            }

            if (string.IsNullOrEmpty(username))
                return BaseApiResponse.Error(MessageConstants.INVALID_ACCESS_TOKEN);

            if (!RefreshTokens.TryGetValue(username, out var storedRefreshToken))
                return BaseApiResponse.Error(MessageConstants.REFRESH_TOKEN_NOT_FOUND);

            if (storedRefreshToken != request.RefreshToken)
                return BaseApiResponse.Error(MessageConstants.INVALID_REFRESH_TOKEN);

            var user = UserRepository.GetAllAsNoTracking()
                .FirstOrDefault(u => u.Username == username);

            if (user == null)
                return BaseApiResponse.Error(MessageConstants.USER_NOT_FOUND);

            var newAccessToken = GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();

            RefreshTokens[username] = newRefreshToken;

            return BaseApiResponse.OK(new AuthResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
               
            },MessageConstants.NEW_ACCESS_TOKEN_ISSUED);
        }


    }
}
