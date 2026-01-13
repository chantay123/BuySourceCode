using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Dto.Request.JWT
{
    public class LogoutRequestDTO
    {
        [Required(ErrorMessage = "RefreshToken is required")]
        public required string RefreshToken { get; set; }
    }
}
