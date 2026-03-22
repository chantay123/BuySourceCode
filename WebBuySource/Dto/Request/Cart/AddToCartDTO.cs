namespace WebBuySource.Dto.Request.Cart
{
	public class AddToCartDTO
	{
		public int CodeId { get; set; }
		public int Quantity { get; set; } = 1; 
	}
}