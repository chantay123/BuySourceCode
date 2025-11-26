using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Dto.Request.JWT
{
    public class ForgotPaswordRequestDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(gmail\.com)$",
            ErrorMessage = "Only Gmail accounts are supported (no edu, outlook, etc.)")]
        public required string Email { get; set; }
    }
}
