
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebBanNongSan.Dto.Response;
using WebBuySource.Dto.Request;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;
using WebBuySource.Models;
using WebBuySource.Utilities;

namespace WebBuySource.Services
{
    public class JwtService : BaseService, IJwtService
    {
        #region Reference repository
        /// <summary>
        /// Gets the user repository.
        /// </summary>
        private IRepository<User> UserRepository => UnitOfWork.UserRepository;
        #endregion

        private readonly IConfiguration _config;

        public JwtService(IUnitOfWork unitOfWork, IConfiguration config) : base(unitOfWork)
        {
            _config = config;
        }

        public async Task<BaseAPIResponse> Register(RegisterRequestDTO request)
        {
            
            var newUser = new User
            {
                Username = request.Username,
                Fullname = request.Fullname,
                Email = request.Email,
                RoleId = 1 
            };

            await UserRepository.AddAsync(newUser).ConfigureAwait(false);
            await UserRepository.SaveChangesAsync();

            return BaseApiResponse.OK("Register success");
        }

        public async Task<BaseAPIResponse> Login(LoginRequestDTO request)
        {
      
            var user = UserRepository.GetAllAsNoTracking()
                                     .FirstOrDefault(u => u.Username == request.Username);

            if (user == null)
            {
                return BaseApiResponse.NotFound("User not found");
            }

           

            var token = GenerateToken(user);

            return BaseApiResponse.OK(new AuthResponseDTO
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"]))
            });
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("id", user.Id.ToString()),
                new Claim("role", user.RoleId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
