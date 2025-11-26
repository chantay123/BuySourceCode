using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Dto.Request.JWT
{
    public class ChangePasswordRequestDTO
    {
        [Required(ErrorMessage = "Current password is required")]
        public required string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "New password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [MaxLength(16, ErrorMessage = "Password cannot exceed 16 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,16}$",
            ErrorMessage = "Password must be 8-16 characters long, include uppercase, lowercase, number, and special character")]
        public required string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("NewPassword", ErrorMessage = "Password and confirm password do not match")]
        public required string ConfirmPassword { get; set; }
    }
}
