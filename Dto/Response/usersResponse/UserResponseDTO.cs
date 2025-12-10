using WebBuySource.Models.Enums;

namespace WebBuySource.Dto.Response.users
{
    public class UserResponseDTO
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Gender? Gender { get; set; }
        public string? Avatar { get; set; }
        public string RoleName { get; set; } 
        public UserStatus Status { get; set; } 
     
    }
}
