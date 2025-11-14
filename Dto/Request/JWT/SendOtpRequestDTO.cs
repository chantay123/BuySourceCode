using WebBuySource.Models.Enums;

namespace WebBuySource.Dto.Request.JWT
{
    public class SendOtpRequestDTO
    {
        public required string Email { get; set; }

        public VerificationCodeType Type { get; set; }
    }
}
