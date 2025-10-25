﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        private readonly IEmailService _emailService;

        public JwtService(IUnitOfWork unitOfWork, IConfiguration config, IEmailService emailService) : base(unitOfWork)
        {
            _config = config;
            _emailService = emailService;
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
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await UserRepository.AddAsync(newUser);
            await UserRepository.SaveChangesAsync();

            ///Sent email 
            var sendOtpResponse = await _emailService.SendOtp(new SendOtpRequestDTO
            {
                Email = request.Email
            });


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
            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateAccessToken(user);

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
                return BaseApiResponse.Error(MessageConstants.REFRESH_TOKEN_EXPIRED);
            }

            // Get related user
            var user = UserRepository.GetAll()
                .FirstOrDefault(u => u.Id == existingToken.UserId);

            if (user == null)
                return BaseApiResponse.NotFound(MessageConstants.USER_NOT_FOUND);

            // Generate new tokens
            var newAccessToken = GenerateAccessToken(user);
            var newRefreshToken = GenerateRefreshToken(user);

            var token = new RefreshToken
            {
                Token = newRefreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await RefreshTokenRepository.SaveChangesAsync();

            return BaseApiResponse.OK(new AuthResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            }, MessageConstants.NEW_ACCESS_TOKEN_ISSUED);
        }
        #endregion

      
        /// <summary>
        /// GenerateAccessToken 
        /// </summary>
        #region Generate Access Token
        public string GenerateAccessToken(User user)
        {
            var jwtAccessKey = Environment.GetEnvironmentVariable("JWT_ACCESS_KEY") ;
            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ;
            var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ;
            var jwtExpireMinutes = Environment.GetEnvironmentVariable("JWT_ACCESS_EXPIRE_MINUTES");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAccessKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(Convert.ToInt32(jwtExpireMinutes));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Fullname ?? string.Empty),
                new Claim ("email",user.Email.ToString()),
                new Claim(ClaimTypes.Role, "User"),
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

        /// <summary>
        /// Logout User     
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseAPIResponse> Logout(LogoutRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
                return BaseApiResponse.Error(MessageConstants.REFRESH_TOKEN_REQUIRED);

            var existingToken = RefreshTokenRepository.GetAll()
                .FirstOrDefault(rt => rt.Token == request.RefreshToken);

            if (existingToken == null)
                return BaseApiResponse.Error(MessageConstants.REFRESH_TOKEN_INVALID);

            RefreshTokenRepository.Delete(existingToken);
            await RefreshTokenRepository.SaveChangesAsync();

            return BaseApiResponse.OK(MessageConstants.LOGOUT_SUCCESS);
        }

        /// <summary>
        /// ForgotPassword
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseAPIResponse> ForgotPassword(ForgotPaswordRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BaseApiResponse.Error(MessageConstants.EMAIL_REQUIRED);

            var user = UserRepository.GetAllAsNoTracking()
                .FirstOrDefault(u => u.Email == request.Email);

            if (user == null)
                return BaseApiResponse.Error(MessageConstants.USER_NOT_FOUND);

            //  Sent OTP
            var sendOtpResponse = await _emailService.SendOtp(new SendOtpRequestDTO
            {
                Email = request.Email
            });

            return BaseApiResponse.OK(MessageConstants.OtpSentSuccess);
        }

        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseAPIResponse> ResetPassword(ResetPasswordRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BaseApiResponse.Error(MessageConstants.EMAIL_REQUIRED);

            if (request.NewPassword != request.ConfirmPassword)
                return BaseApiResponse.Error(MessageConstants.PASSWORD_NOT_MATCH);

            var user = UserRepository.GetAll().FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
                return BaseApiResponse.NotFound(MessageConstants.USER_NOT_FOUND);

            // Chage password
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            UserRepository.Update(user);
            await UserRepository.SaveChangesAsync();

            return BaseApiResponse.OK(MessageConstants.RESET_PASSWORD_SUCCESS);
        }



        #region Generate Refresh Token
        /// <summary>
        /// GenerateRefreshToken
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GenerateRefreshToken(User user)
        {
            var jwtRefreshKey = Environment.GetEnvironmentVariable("JWT_REFRESH_KEY");
            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            var jwtExpireDays = Environment.GetEnvironmentVariable("JWT_REFRESH_EXPIRE_DAYS"); 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtRefreshKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;
            var expires = now.AddDays(Convert.ToInt32(jwtExpireDays));

            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim ("email",user.Email.ToString()),
                new Claim ("username",user.Fullname.ToString()),
               
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
