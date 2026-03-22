using WebBuySource.Dto.Request.Cart;
using WebBuySource.Dto.Response;

namespace WebBuySource.Interfaces
{
	public interface ICartService
	{
		Task<BaseAPIResponse> AddToCartAsync(int userId, AddToCartDTO request);
		Task<BaseAPIResponse> GetCartAsync(int userId);
		Task<BaseAPIResponse> RemoveCartAsync(int cartId, int userId);
		Task<BaseAPIResponse> UpdateQuantityAsync(int cartId, int userId, int quantity);
		Task<BaseAPIResponse> ClearCartAsync(int userId);
		Task<BaseAPIResponse> ToggleSelectItemAsync(int cartId, int userId, bool isSelected);
		Task<BaseAPIResponse> GetSelectedItemsAsync(int userId);
	}
}