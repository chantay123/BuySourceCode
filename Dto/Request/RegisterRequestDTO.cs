using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Dto.Request
{
    public class RegisterRequestDTO
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Fullname is required")]
        [MaxLength(100, ErrorMessage = "Fullname cannot exceed 100 characters")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^(0|\+84)(\d{9})$", ErrorMessage = "Phone number must be a valid Vietnamese number")]
        public string Phone { get; set; }
    }
}
