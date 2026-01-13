using WebBuySource.Dto.Request.JWT;
using WebBuySource.Dto.Response;
using WebBuySource.Models;

namespace WebBuySource.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// Generates a JWT access token for a given user.
        /// </summary>
        /// <param name="user">User information used to create the token.</param>
        /// <returns>JWT token string.</returns>
        string GenerateAccessToken(User user);


        /// <summary>
        /// Generates a JWT refres token for a given user.
        /// </summary>
        /// <param name="user">User information used to create the token.</param>
        /// <returns>JWT token string.</returns>
        string GenerateRefreshToken(User user);

        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <param name="request">Registration request data.</param>
        /// <returns>API response containing status and message.</returns>
        Task<BaseAPIResponse> Register(RegisterRequestDTO request);

        /// <summary>
        /// Authenticates a user and returns a JWT token if valid.
        /// </summary>
        /// <param name="request">Login credentials.</param>
        /// <returns>API response containing token and expiration details.</returns>
        Task<BaseAPIResponse> Login(LoginRequestDTO request);

  
        /// <summary>
        /// Generates a new access token using a valid refresh token.
        /// </summary>
        /// <param name="accessToken">Expired access token.</param>
        /// <param name="refreshToken">Current valid refresh token.</param>
        /// <returns>New access token and refresh token pair.</returns>
        Task<BaseAPIResponse> RefreshToken(RefreshTokenRequestDTO request);

        /// <summary>
        ///  logout
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> Logout(LogoutRequestDTO  request);

        /// <summary>
        /// forgot-password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> ForgotPassword( ForgotPaswordRequestDTO request);


        /// <summary>
        /// Reset- password 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> ResetPassword(ResetPasswordRequestDTO request);

        /// <summary>
        /// Change-Password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> ChangePassword(ChangePasswordRequestDTO request ,int userId);

        /// <summary>
        /// Login or register user via Google OAuth
        /// </summary>
        Task<BaseAPIResponse> LoginWithGoogle(GoogleLoginRequestDTO request);
    }
}
