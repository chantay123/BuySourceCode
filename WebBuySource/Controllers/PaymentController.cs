using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebBuySource.Dto.Request.Payment;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;
using WebBuySource.Utilities;

namespace WebBuySource.Controllers
{
	[Route("api/v1/payment")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly IPaymentService _paymentService;

		public PaymentController(IPaymentService paymentService)
		{
			_paymentService = paymentService;
		}

		/// <summary>
		/// Tạo giao dịch thanh toán
		/// </summary>
		[Authorize]
		[HttpPost("create")]
		public async Task<BaseAPIResponse> CreatePayment([FromBody] CreatePaymentRequestDTO request)
		{
			// Lấy userId an toàn, tránh NullReferenceException
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)
							  ?? User.FindFirst("sub")  // Fallback to 'sub' claim
							  ?? User.FindFirst("nameid")  // Fallback to 'nameid'
							  ?? User.FindFirst("id");  // Fallback to 'id'

			if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
			{
				return BaseApiResponse.Error("INVALID_TOKEN", "User ID not found in token", null);
			}

			return await _paymentService.CreatePaymentAsync(userId, request);
		}

		/// <summary>
		/// IPN từ MoMo (server-to-server)
		/// </summary>
		[HttpPost("momo-notify")]
		public async Task<BaseAPIResponse> MomoNotify([FromBody] MomoNotifyDTO notifyData)
		{
			return await _paymentService.MomoNotifyAsync(notifyData);
		}
	}
}