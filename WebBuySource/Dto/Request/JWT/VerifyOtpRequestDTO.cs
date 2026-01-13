using System.ComponentModel.DataAnnotations;
using WebBuySource.Models.Enums;

namespace WebBuySource.Dto.Request.JWT
{
    public class VerifyOtpRequestDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "OTP is required")]
        [StringLength(6, MinimumLength = 4, ErrorMessage = "OTP must be 4-6 digits")]
        public required string Otp { get; set; }

        [EnumDataType(typeof(VerificationCodeType), ErrorMessage = "Invalid verification code type")]
        public VerificationCodeType Type { get; set; }
    }
}
