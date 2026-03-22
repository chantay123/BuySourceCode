using DotNetEnv;
using WebBuySource.Dto.Request.Payment;
using WebBuySource.Interfaces;
using WebBuySource.Utilities.MomoSecurity;

namespace WebBuySource.Services
{
	public class MomoPaymentService : IMomoPaymentService
	{
		private readonly MomoConfig _momoConfig;

		public MomoPaymentService()
		{
			_momoConfig = new MomoConfig
			{
				PartnerCode = Env.GetString("MOMO_PARTNER_CODE"),
				AccessKey = Env.GetString("MOMO_ACCESS_KEY"),
				SecretKey = Env.GetString("MOMO_SECRET_KEY"),
				ApiEndpoint = Env.GetString("MOMO_API_ENDPOINT"),
				ReturnUrl = Env.GetString("MOMO_RETURN_URL"),
				NotifyUrl = Env.GetString("MOMO_NOTIFY_URL")
			};
		}

		public async Task<MomoPaymentRequestDTO> CreatePaymentRequestAsync(long amount, string orderId, string orderInfo, string returnUrl)
		{
			var requestId = Guid.NewGuid().ToString();
			var request = new MomoPaymentRequestDTO
			{
				PartnerCode = _momoConfig.PartnerCode,
				RequestId = requestId,
				Amount = amount,
				OrderId = orderId,
				OrderInfo = orderInfo,
				RedirectUrl = returnUrl ?? _momoConfig.ReturnUrl,
				IpnUrl = _momoConfig.NotifyUrl,
				RequestType = "captureWallet",
				ExtraData = ""
			};

			// Tạo signature
			var rawHash = $"accessKey={_momoConfig.AccessKey}&amount={amount}&extraData={request.ExtraData}&ipnUrl={_momoConfig.NotifyUrl}&orderId={orderId}&orderInfo={orderInfo}&partnerCode={_momoConfig.PartnerCode}&redirectUrl={request.RedirectUrl}&requestId={requestId}&requestType={request.RequestType}";
			request.Signature = MomoHash.HmacSHA256(rawHash, _momoConfig.SecretKey);

			return request;
		}

		public Task<bool> VerifyPaymentCallbackAsync(string signature, string orderId, long amount, string message, string resultCode)
		{
			// Tạo raw hash từ callback data
			var rawHash = $"accessKey={_momoConfig.AccessKey}&amount={amount}&message={message}&orderId={orderId}&partnerCode={_momoConfig.PartnerCode}&resultCode={resultCode}";
			var expectedSignature = MomoHash.HmacSHA256(rawHash, _momoConfig.SecretKey);

			return Task.FromResult(signature.Equals(expectedSignature, StringComparison.OrdinalIgnoreCase));
		}
	}
}