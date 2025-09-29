using Microsoft.AspNetCore.Mvc;
using WebBanNongSan.Dto.Response;
using WebBuySource.Dto.Request;
using WebBuySource.Interfaces;

namespace WebBuySource.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<BaseAPIResponse> Register([FromQuery] RegisterRequestDTO request)
        {
            
            return  await _jwtService.Register(request);
        }

        [HttpPost("login")]
        public async Task<BaseAPIResponse> Login([FromQuery] LoginRequestDTO request)
        {
            return await _jwtService.Login(request); 
        }
    }
}
