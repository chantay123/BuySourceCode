using WebBuySource.Dto.Request.JWT;
using WebBuySource.Dto.Response;

namespace WebBuySource.Interfaces
{
    public interface IEmailService
    {
        public Task<BaseAPIResponse> SendOtp(SendOtpRequestDTO request);

        public Task<BaseAPIResponse> VerifyOtp(VerifyOtpRequestDTO verifyOtpRequest);
    }
}

