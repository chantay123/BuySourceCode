using System.ComponentModel.DataAnnotations;
using WebBuySource.Models.Enums;

namespace WebBuySource.Dto.Request.JWT
{
    public class SendOtpRequestDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string Email { get; set; }

        [EnumDataType(typeof(VerificationCodeType), ErrorMessage = "Invalid verification code type")]
        public VerificationCodeType Type { get; set; }
    }
}
