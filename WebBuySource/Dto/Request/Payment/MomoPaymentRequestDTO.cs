namespace WebBuySource.Dto.Request.Payment
{
	public class MomoPaymentRequestDTO
	{
		public string PartnerCode { get; set; } = string.Empty;
		public string RequestId { get; set; } = string.Empty;
		public long Amount { get; set; }
		public string OrderId { get; set; } = string.Empty;
		public string OrderInfo { get; set; } = string.Empty;
		public string RedirectUrl { get; set; } = string.Empty;
		public string IpnUrl { get; set; } = string.Empty;
		public string RequestType { get; set; } = "captureWallet";
		public string ExtraData { get; set; } = "";
		public string Signature { get; set; } = string.Empty;
	}
}