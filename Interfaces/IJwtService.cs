
using WebBanNongSan.Dto.Response;
using WebBuySource.Dto.Request;
using WebBuySource.Models;

namespace WebBuySource.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// Generate access token cho user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>JWT token string</returns>
        string GenerateToken(User user);

       
        /// <summary>
        /// Đăng ký user mới
        /// </summary>
        Task<BaseAPIResponse> Register(RegisterRequestDTO request);

        /// <summary>
        /// Đăng nhập user 
        /// </summary>
        Task<BaseAPIResponse> Login(LoginRequestDTO request);
    }
}
