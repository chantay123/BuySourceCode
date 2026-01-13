using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Dto.Request.JWT
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(gmail\.com)$",
            ErrorMessage = "Only Gmail accounts are supported (no edu, outlook, etc.)")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [MaxLength(16, ErrorMessage = "Password cannot exceed 16 characters")]
        public required string Password { get; set; }
    }
}