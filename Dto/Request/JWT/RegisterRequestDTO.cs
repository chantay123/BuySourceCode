using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Dto.Request.JWT
{
    public class RegisterRequestDTO
    {
        // EMAIL
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(150, ErrorMessage = "Email cannot exceed 150 characters")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$",
            ErrorMessage = "Email must be a valid Gmail address")]
        public required string Email { get; set; }


        // FULLNAME
        [Required(ErrorMessage = "Fullname is required")]
        [MinLength(3, ErrorMessage = "Fullname must be at least 3 characters long")]
        [MaxLength(30, ErrorMessage = "Fullname cannot exceed 30 characters")]
        [RegularExpression(@"^(?!\s*$)(?!.*\s{2,})[a-zA-ZÀ-ỹ]+(?:\s[a-zA-ZÀ-ỹ]+)*$",
            ErrorMessage = "Fullname cannot contain numbers, special characters, or extra spaces")]
        public required string Fullname { get; set; }


        // PASSWORD
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 6 characters long")]
        [MaxLength(16, ErrorMessage = "Password cannot exceed 50 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain uppercase, lowercase, number, and special character")]
        public required string Password { get; set; }


        // CONFIRM PASSWORD
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public required string ConfirmPassword { get; set; }
    }

}
