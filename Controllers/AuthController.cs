using Microsoft.AspNetCore.Mvc;
using WebBuySource.Dto.Response;
using WebBuySource.Dto.Request;
using WebBuySource.Interfaces;


namespace WebBuySource.Controllers
{
    /// <summary>
    /// Handles all authentication-related endpoints such as registration, login, and token refresh.
    /// </summary>
    [ApiController]
    [Route("api/v1/auth")]

    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        private readonly IEmailService _emailService;


        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="jwtService">Service responsible for handling JWT authentication logic.</param>
        public AuthController(IJwtService jwtService, IEmailService emailService)
        {
            _jwtService = jwtService;
            _emailService = emailService;
        }

        /// <summary>
        /// Registers a new user with the system.
        /// </summary>
        /// <param name="request">User registration details (username, email, password, etc.).</param>
        /// <returns>Returns success or failure message.</returns>
        [HttpPost("register")]
        public async Task<BaseAPIResponse> Register([FromBody] RegisterRequestDTO request)
        {
            return await _jwtService.Register(request);
        }

        /// <summary>
        /// Authenticates a user and generates access & refresh tokens.
        /// </summary>
        /// <param name="request">Login credentials including username and password.</param>
        /// <returns>Returns an access token and a refresh token if login succeeds.</returns>
        [HttpPost("login")]
        public async Task<BaseAPIResponse> Login([FromBody] LoginRequestDTO request)
        {
            return await _jwtService.Login(request);
        }

        /// <summary>
        /// Refreshes the access token using a valid refresh token.
        /// </summary>
        /// <param name="request">Contains the expired access token and the current refresh token.</param>
        /// <returns>Returns a new access token and refresh token if the old refresh token is valid.</returns>
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status401Unauthorized)]
        public async Task<BaseAPIResponse> RefreshToken([FromBody] RefreshTokenRequestDTO request)
        {
            // Delegate the logic to JwtService
            return await _jwtService.RefreshToken(request);
        }

        [HttpPost("send-otp")]
        public async Task<BaseAPIResponse> SendOtp([FromBody] SendOtpRequestDTO request)
        {
            return await _emailService.SendOtp(request);
        }

        [HttpPost("verify-otp")]
        public async Task<BaseAPIResponse> VerifyOtp([FromBody] VerifyOtpRequestDTO request)
        {
            return await _emailService.VerifyOtp(request);
        } 

    }
}
