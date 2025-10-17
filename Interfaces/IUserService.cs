using WebBuySource.Dto.Request;
using WebBuySource.Dto.Response;

namespace WebBuySource.Interfaces
{
    public interface IUserService
    {
        public Task<BaseAPIResponse> ForgotPassword(ForgotPaswordRequestDTO request);


    }
}
