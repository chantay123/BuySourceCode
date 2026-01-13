using Microsoft.AspNetCore.Mvc;
using WebBuySource.Interfaces;
using WebBuySource.Dto.Request.JWT;
using Microsoft.AspNetCore.Authorization;
using WebBuySource.Dto.Response;

namespace WebBuySource.Controllers
{
    /// <summary>
    /// Handles all authentication-related endpoints such as registration, login, token refresh, and OTP verification.
    /// </summary>
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;

        public AuthController(IJwtService jwtService, IEmailService emailService)
        {
            _jwtService = jwtService;
            _emailService = emailService;
        }

        /// <summary>
        /// Registers a new user with the system.
        /// </summary>
        /// <param name="request">User registration details.</param>
        /// <returns>Returns success or failure message.</returns>
        /// <response code="200">User registered successfully.</response>
        /// <response code="400">Invalid registration details.</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<BaseAPIResponse> Register([FromBody] RegisterRequestDTO request)
        {
            return await _jwtService.Register(request);
        }

        /// <summary>
        /// Authenticates a user and generates access & refresh tokens.
        /// </summary>
        /// <param name="request">Login credentials including username and password.</param>
        /// <returns>Returns an access token and a refresh token if login succeeds.</returns>
        /// <response code="200">Login successful.</response>
        /// <response code="400">Invalid login credentials.</response>
        /// <response code="401">Unauthorized, invalid username/password.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<BaseAPIResponse> Login([FromBody] LoginRequestDTO request)
        {
            return await _jwtService.Login(request);
        }

        /// <summary>
        /// Refreshes the access token using a valid refresh token.
        /// </summary>
        /// <param name="request">Contains the expired access token and the current refresh token.</param>
        /// <returns>Returns a new access token and refresh token if the old refresh token is valid.</returns>
        /// <response code="200">Token refreshed successfully.</response>
        /// <response code="401">Invalid or expired refresh token.</response>
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<BaseAPIResponse> RefreshToken([FromBody] RefreshTokenRequestDTO request)
        {
            return await _jwtService.RefreshToken(request);
        }

        /// <summary>
        /// Sends a One-Time Password (OTP) to the user's email.
        /// </summary>
        /// <param name="request">Contains email or user identifier.</param>
        /// <returns>Result of sending OTP.</returns>
        /// <response code="200">OTP sent successfully.</response>
        /// <response code="400">Invalid request or email not found.</response>
        [HttpPost("send-otp")]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<BaseAPIResponse> SendOtp([FromBody] SendOtpRequestDTO request)
        {
            return await _emailService.SendOtp(request);
        }

        /// <summary>
        /// Verifies a One-Time Password (OTP) sent to the user.
        /// </summary>
        /// <param name="request">Contains email/user identifier and OTP code.</param>
        /// <returns>Result of OTP verification.</returns>
        /// <response code="200">OTP verified successfully.</response>
        /// <response code="400">Invalid OTP or expired.</response>
        /// 
        [HttpPost("verify-otp")]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<BaseAPIResponse> VerifyOtp([FromBody] VerifyOtpRequestDTO request)
        {
            return await _emailService.VerifyOtp(request);
        }

        /// <summary>
        /// Logout 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        [HttpPost("logout")]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<BaseAPIResponse> Logout([FromBody] LogoutRequestDTO request)
        {
            return await _jwtService.Logout(request);
        }

        /// <summary>
        /// forgot-password 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("forgot-password")]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<BaseAPIResponse> ForgotPassword([FromBody] ForgotPaswordRequestDTO request)
        {
            return await _jwtService.ForgotPassword(request);
        }

        /// <summary>
        /// reset-password 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<BaseAPIResponse> ResetPassword([FromBody] ResetPasswordRequestDTO request)
        {
            return await _jwtService.ResetPassword(request);
        }

        [HttpPost("change-password")]
        [Authorize]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<BaseAPIResponse> ChangePassword([FromBody] ChangePasswordRequestDTO request, int userId)
        {
            return await _jwtService.ChangePassword(request,userId);
        }
        [HttpGet("google")]
        public IActionResult GoogleLogin()
        {
            var clientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID");
            var redirectUri = Environment.GetEnvironmentVariable("GOOGLE_CALLBACK_URL");

            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(redirectUri))
                return BadRequest("GOOGLE ENV NOT CONFIGURED");

            var url =
                "https://accounts.google.com/o/oauth2/v2/auth" +
                "?client_id=" + Uri.EscapeDataString(clientId) +
                "&redirect_uri=" + Uri.EscapeDataString(redirectUri) +
                "&response_type=code" +
                "&scope=openid%20email%20profile";

            return Ok(new { url });
        }


        [HttpGet("google/callback")]
        public async Task<IActionResult> GoogleCallback([FromQuery] string code)
        {
            var result = await _jwtService.LoginWithGoogle(
                new GoogleLoginRequestDTO
                {
                    Code = code
                }
            );  
            return Ok(result);
        }
    }
}