using System.Text.Json;
using WebBuySource.Dto.Request.Payment;
using WebBuySource.Dto.Response;
using WebBuySource.Dto.Response.Payment;
using WebBuySource.Interfaces;
using WebBuySource.Models;
using WebBuySource.Models.Enums;
using WebBuySource.Utilities;

namespace WebBuySource.Services
{
	public class PaymentService : IPaymentService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IConfiguration _configuration;
		private readonly IMomoPaymentService _momoPaymentService;
		private readonly IHttpClientFactory _httpClientFactory;

		public PaymentService(
			IUnitOfWork unitOfWork,
			IConfiguration configuration,
			IMomoPaymentService momoPaymentService,
			IHttpClientFactory httpClientFactory)
		{
			_unitOfWork = unitOfWork;
			_configuration = configuration;
			_momoPaymentService = momoPaymentService;
			_httpClientFactory = httpClientFactory;
		}

		public async Task<BaseAPIResponse> CreatePaymentAsync(int userId, CreatePaymentRequestDTO request)
		{
			// 1. Kiểm tra code tồn tại
			var code = await _unitOfWork.CodeRepository
				.FirstOrDefaultAsyncAsNoTracking(x => x.Id == request.CodeId && x.DeletedAt == null);

			if (code == null)
				return BaseApiResponse.NotFound("CODE_NOT_FOUND");

			// 2. Kiểm tra đã mua chưa
			var existingTransaction = await _unitOfWork.TransactionRepository
				.FirstOrDefaultAsync(x =>
					x.BuyerId == userId &&
					x.CodeId == request.CodeId &&
					x.Status == TransactionStatus.COMPLETED);

			if (existingTransaction != null)
				return BaseApiResponse.Error("ALREADY_PURCHASED");

			// 3. Tạo transaction mới
			var transaction = new Transaction
			{
				BuyerId = userId,
				CodeId = code.Id,
				Amount = code.Price,
				Currency = "VND",
				Type = TransactionType.PURCHASE, // Sửa thành PURCHASE thay vì PENDING
				Status = TransactionStatus.PENDING,
				PaymentMethod = request.PaymentMethod,
				CreatedAt = DateTime.UtcNow
			};

			await _unitOfWork.TransactionRepository.AddAsync(transaction);
			await _unitOfWork.TransactionRepository.SaveChangesAsync();

			// 4. Xử lý theo phương thức thanh toán
			//if (request.PaymentMethod.ToUpper() == "MOMO")
			if (string.Equals(request.PaymentMethod, "MOMO", StringComparison.OrdinalIgnoreCase))
			{
				return await ProcessMomoPayment(transaction, request.ReturnUrl);
			}
			else
			{
				return BaseApiResponse.Error("PAYMENT_METHOD_NOT_SUPPORTED");
			}
		}

		private async Task<BaseAPIResponse> ProcessMomoPayment(Transaction transaction, string? returnUrl)
		{
			try
			{
				var orderId = $"{transaction.Id}_{DateTime.Now.Ticks}";
				var orderInfo = $"Thanh toán mã nguồn #{transaction.CodeId}";

				// Tạo request MoMo
				var momoRequest = await _momoPaymentService.CreatePaymentRequestAsync(
					(long)transaction.Amount,
					orderId,
					orderInfo,
					returnUrl ?? _configuration["Momo:ReturnUrl"] ?? ""
				);

				// Gọi API MoMo
				var httpClient = _httpClientFactory.CreateClient();
				var content = new StringContent(
					JsonSerializer.Serialize(momoRequest),
					System.Text.Encoding.UTF8,
					"application/json"
				);

				var momoApiEndpoint = _configuration["Momo:ApiEndpoint"] ??
					"https://test-payment.momo.vn/v2/gateway/api/create";

				var response = await httpClient.PostAsync(momoApiEndpoint, content);
				var responseContent = await response.Content.ReadAsStringAsync();
				var momoResponse = JsonSerializer.Deserialize<MomoResponseDTO>(responseContent);

				if (momoResponse != null && momoResponse.resultCode == 0)
				{
					// Tạo response DTO
					var paymentResponse = new PaymentResponseDTO
					{
						TransactionId = transaction.Id,
						PaymentUrl = momoResponse.payUrl
					};

					// Có thể thêm thông tin bổ sung qua dynamic object
					return BaseApiResponse.OK(new
					{
						transactionId = transaction.Id,
						paymentUrl = momoResponse.payUrl,
						orderId = orderId
					});
				}
				else
				{
					// Cập nhật transaction thất bại
					transaction.Status = TransactionStatus.FAILED;
					_unitOfWork.TransactionRepository.Update(transaction);
					_unitOfWork.Commit(); // Dùng Commit() thay vì SaveChangesAsync()

					return BaseApiResponse.Error(
						"MOMO_PAYMENT_ERROR",
						momoResponse?.message ?? "Không thể tạo thanh toán MoMo",
						null
					);
				}
			}
			catch (Exception ex)
			{
				// Log exception
				transaction.Status = TransactionStatus.FAILED;
				_unitOfWork.TransactionRepository.Update(transaction);
				_unitOfWork.Commit(); // Dùng Commit() thay vì SaveChangesAsync()

				return BaseApiResponse.Error(
					"PAYMENT_SYSTEM_ERROR",
					ex.Message,
					null
				);
			}
		}

		public async Task<BaseAPIResponse> PaymentCallbackAsync(IQueryCollection query)
		{
			// Callback từ frontend sau khi thanh toán xong
			var resultCode = query["resultCode"].ToString();
			var orderId = query["orderId"].ToString();
			var transactionId = ExtractTransactionIdFromOrderId(orderId);

			var transaction = await _unitOfWork.TransactionRepository
				.GetByIdAsync(transactionId);

			if (transaction == null)
				return BaseApiResponse.NotFound("TRANSACTION_NOT_FOUND");

			if (resultCode == "0") // Thành công
			{
				transaction.Status = TransactionStatus.COMPLETED;
				_unitOfWork.TransactionRepository.Update(transaction);

				// Kiểm tra nếu có DownloadRepository trong IUnitOfWork
				// Nếu không có, bạn có thể bỏ qua phần này hoặc thêm sau
				_unitOfWork.Commit();

				return BaseApiResponse.OK(new
				{
					success = true,
					message = "Thanh toán thành công",
					transactionId = transaction.Id,
					codeId = transaction.CodeId
				});
			}
			else
			{
				transaction.Status = TransactionStatus.FAILED;
				_unitOfWork.TransactionRepository.Update(transaction);
				_unitOfWork.Commit();

				return BaseApiResponse.Error(
					"PAYMENT_FAILED",
					$"Thanh toán thất bại. Mã lỗi: {resultCode}",
					null
				);
			}
		}

		public async Task<BaseAPIResponse> MomoNotifyAsync(MomoNotifyDTO notifyData)
		{
			// IPN từ MoMo (server-to-server)
			var isValid = await _momoPaymentService.VerifyPaymentCallbackAsync(
				notifyData.Signature,
				notifyData.OrderId,
				notifyData.Amount,
				notifyData.Message,
				notifyData.ResultCode.ToString()
			);

			if (!isValid)
				return BaseApiResponse.Error("INVALID_SIGNATURE");

			var transactionId = ExtractTransactionIdFromOrderId(notifyData.OrderId);
			var transaction = await _unitOfWork.TransactionRepository
				.GetByIdAsync(transactionId);

			if (transaction == null)
				return BaseApiResponse.NotFound("TRANSACTION_NOT_FOUND");

			if (notifyData.ResultCode == 0) // Thành công
			{
				transaction.Status = TransactionStatus.COMPLETED;
				// Thêm các xử lý khác nếu cần
			}
			else
			{
				transaction.Status = TransactionStatus.FAILED;
			}

			_unitOfWork.TransactionRepository.Update(transaction);
			_unitOfWork.Commit(); // Dùng Commit() thay vì SaveChangesAsync()

			return BaseApiResponse.OK(new { success = true });
		}

		private int ExtractTransactionIdFromOrderId(string orderId)
		{
			// OrderId format: "123_1234567890" -> transactionId = 123
			if (string.IsNullOrEmpty(orderId))
				return 0;

			var parts = orderId.Split('_');
			return int.TryParse(parts[0], out var id) ? id : 0;
		}

		private class MomoResponseDTO
		{
			public string partnerCode { get; set; } = string.Empty;
			public string orderId { get; set; } = string.Empty;
			public string requestId { get; set; } = string.Empty;
			public long amount { get; set; }
			public long responseTime { get; set; }
			public string message { get; set; } = string.Empty;
			public int resultCode { get; set; }
			public string payUrl { get; set; } = string.Empty;
			public string deeplink { get; set; } = string.Empty;
			public string qrCodeUrl { get; set; } = string.Empty;
		}
	}
}