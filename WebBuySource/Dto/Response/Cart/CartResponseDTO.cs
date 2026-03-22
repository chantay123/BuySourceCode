namespace WebBuySource.Dto.Response.Cart
{
	public class CartResponseDTO
	{
		public int CartId { get; set; }
		public int CodeId { get; set; }
		public string? CodeTitle { get; set; }
		public string? CodeDescription { get; set; }
		public string? PreviewImage { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice => Price * Quantity;
		public bool IsSelected { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ExpiredAt { get; set; }
	}
}