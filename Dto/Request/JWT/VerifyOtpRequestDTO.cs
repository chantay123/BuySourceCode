using WebBuySource.Models.Enums;

namespace WebBuySource.Dto.Request.JWT
{
    public class VerifyOtpRequestDTO
    {
        public required string Email { get; set; }
        public required string Otp { get; set; }

        public VerificationCodeType Type { get; set; }
    }
}
