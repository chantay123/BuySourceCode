using WebBuySource.Dto.Request.Payment;
using WebBuySource.Dto.Response;

namespace WebBuySource.Interfaces
{
	public interface IMomoPaymentService
	{
		Task<MomoPaymentRequestDTO> CreatePaymentRequestAsync(long amount, string orderId, string orderInfo, string returnUrl);
		Task<bool> VerifyPaymentCallbackAsync(string signature, string orderId, long amount, string message, string resultCode);
	}
}