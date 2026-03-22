namespace WebBuySource.Dto.Request.Payment
{
	public class CreatePaymentRequestDTO
	{
		public int CodeId { get; set; }
		public string PaymentMethod { get; set; } = "MOMO"; // MOMO, VNPAY, etc.
		public string? ReturnUrl { get; set; } // Frontend URL để redirect sau thanh toán
	}
}