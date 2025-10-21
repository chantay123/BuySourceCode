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
    /// <summary>
    /// Service responsible for handling JWT authentication,
    /// including token generation, validation, and refresh logic.
    /// </summary>
    public class JwtService : BaseService, IJwtService
    {
        #region Repository references
        private IRepository<User> UserRepository => UnitOfWork.UserRepository;
        private IRepository<RefreshToken> RefreshTokenRepository => UnitOfWork.RefreshTokenRepository;
        #endregion

        private readonly IConfiguration _config;

        public JwtService(IUnitOfWork unitOfWork, IConfiguration config) : base(unitOfWork)
        {
            _config = config;
        }

        #region Register
        /// <summary>
        /// Registers a new user with encrypted password and default role.
        /// </summary>
        public async Task<BaseAPIResponse> Register(RegisterRequestDTO request)
        {
            // Validate password confirmation
            if (request.Password != request.ConfirmPassword)
                return BaseApiResponse.Error(MessageConstants.PASSWORD_NOT_MATCH);

            // Check for existing user
            var existingUser = UserRepository.GetAllAsNoTracking()
                .FirstOrDefault(u => u.Email == request.Email);

            if (existingUser != null)
                return BaseApiResponse.Error(MessageConstants.USERNAME_OR_EMAIL_EXISTS);

            // Hash password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Create new user
            var newUser = new User
            {
                Username = request.Email.Trim(),
                Fullname = request.Fullname.Trim(),
                Password = hashedPassword,
                Email = request.Email.Trim(),
                RoleId = 1, // Default: user
                PhoneNumber = request.Phone,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await UserRepository.AddAsync(newUser);
            await UserRepository.SaveChangesAsync();

            return BaseApiResponse.OK(new
            {
                message = MessageConstants.REGISTER_SUCCESS,
                username = newUser.Username,
                email = newUser.Email
            });
        }
        #endregion

        #region Login
        /// <summary>
        /// Authenticates user credentials and issues JWT + Refresh Token.
        /// </summary>
        public async Task<BaseAPIResponse> Login(LoginRequestDTO request)
        {
            // Find user by email
            var user = UserRepository.GetAllAsNoTracking()
                .FirstOrDefault(u => u.Email == request.Email);

            if (user == null)
                return BaseApiResponse.NotFound(MessageConstants.USER_NOT_FOUND);

            // Verify password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!isPasswordValid)
                return BaseApiResponse.Error(MessageConstants.INVALID_PASSWORD);

            // Generate tokens
            var accessToken = GenerateToken(user);
            var refreshToken = GenerateToken(user);

            // Store refresh token in DB
            var token = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await RefreshTokenRepository.AddAsync(token);
            await RefreshTokenRepository.SaveChangesAsync();

            return BaseApiResponse.OK(new AuthResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            }, MessageConstants.LOGIN_SUCCESS);
        }
        #endregion

        #region RefreshToken
        /// <summary>
        /// Validates refresh token and issues new access token.
        /// </summary>
        public async Task<BaseAPIResponse> RefreshToken(RefreshTokenRequestDTO request)
        {
            // Find refresh token in DB
            var existingToken = RefreshTokenRepository.GetAll()
                .FirstOrDefault(rt => rt.Token == request.refreshToken);

            if (existingToken == null)
                return BaseApiResponse.Error(MessageConstants.REFRESH_TOKEN_NOT_FOUND);

            //Delete Refresh token expired
            if (existingToken.ExpiresAt < DateTime.UtcNow)
            {
                RefreshTokenRepository.Delete(existingToken);
                await RefreshTokenRepository.SaveChangesAsync();
                return BaseApiResponse.Error("Refresh token expired.");
            }

            // Get related user
            var user = UserRepository.GetAll()
                .FirstOrDefault(u => u.Id == existingToken.UserId);

            if (user == null)
                return BaseApiResponse.NotFound(MessageConstants.USER_NOT_FOUND);

            // Generate new tokens
            var newAccessToken = GenerateToken(user);
            var newRefreshTokenValue = GenerateToken(user);

            // Optional: replace old token
            existingToken.Token = newRefreshTokenValue;
            existingToken.ExpiresAt = DateTime.UtcNow.AddDays(7);
            existingToken.UpdatedAt = DateTime.UtcNow;

            await RefreshTokenRepository.SaveChangesAsync();

            return BaseApiResponse.OK(new AuthResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshTokenValue
            }, MessageConstants.NEW_ACCESS_TOKEN_ISSUED);
        }
        #endregion

        #region Token generation
        /// <summary>
        /// Generates a signed JWT access token.
        /// </summary>
        public string GenerateToken(User user)
        {
            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? _config["Jwt:Key"];
            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? _config["Jwt:Issuer"];
            var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? _config["Jwt:Audience"];
            var jwtExpireMinutes = Environment.GetEnvironmentVariable("JWT_EXPIRE_MINUTES") ?? "60";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(Convert.ToInt32(jwtExpireMinutes));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Fullname ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}
