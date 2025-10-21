
using WebBuySource.Dto.Request;
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
        string GenerateToken(User user);

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
    }
}
