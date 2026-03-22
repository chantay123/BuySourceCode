using WebBuySource.Dto.Request.Payment;
using WebBuySource.Dto.Response;

namespace WebBuySource.Interfaces
{
	public interface IPaymentService
	{
		Task<BaseAPIResponse> CreatePaymentAsync(int userId, CreatePaymentRequestDTO request);
		Task<BaseAPIResponse> PaymentCallbackAsync(IQueryCollection query);
		Task<BaseAPIResponse> MomoNotifyAsync(MomoNotifyDTO notifyData);
	}
}