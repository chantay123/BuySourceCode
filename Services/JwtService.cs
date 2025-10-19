using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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

          // Create a new user entity with default role 
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
            // Load environment variables (values from your .env file)
            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            var jwtExpireMinutes = Environment.GetEnvironmentVariable("JWT_EXPIRE_MINUTES");

            // Validate environment variables to prevent null errors
            if (string.IsNullOrEmpty(jwtKey))
                throw new Exception(MessageConstants.MissingJwtKey);
            if (string.IsNullOrEmpty(jwtIssuer))
                throw new Exception(MessageConstants.MissingJwtIssuer);
            if (string.IsNullOrEmpty(jwtAudience))
                throw new Exception(MessageConstants.MissingJwtAudience);
            if (string.IsNullOrEmpty(jwtExpireMinutes))
                jwtExpireMinutes = "60"; // default fallback // default expiration time (in minutes)

            // Create the signing key using the secret from the environment variable
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Define issue and expiration times
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(Convert.ToInt32(jwtExpireMinutes));

            // Convert to Unix timestamps for JWT standard claims
            var iat = new DateTimeOffset(now).ToUnixTimeSeconds();
            var exp = new DateTimeOffset(expires).ToUnixTimeSeconds();

            // Define claims (user information embedded in the token)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),   // subject (username)
                new Claim("id", user.Id.ToString()),                     // custom claim: user ID
                new Claim("name", user.Fullname ?? string.Empty),        // custom claim: user full name
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // unique token ID
                new Claim(JwtRegisteredClaimNames.Iat, iat.ToString(), ClaimValueTypes.Integer64), // issued at
                new Claim(JwtRegisteredClaimNames.Exp, exp.ToString(), ClaimValueTypes.Integer64), // expiration
            };

            // Create and sign the JWT
            var token = new JwtSecurityToken(
                issuer: jwtIssuer,         // token issuer
                audience: jwtAudience,     // token audience
                claims: claims,            // user claims
                expires: expires,          // expiration date
                signingCredentials: credentials // signature credentials
            );

            // Generate and return the final token string
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
            var jwtToken = handler.ReadJwtToken(request.accessToken);
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

            if (storedRefreshToken != request.refreshToken)
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
