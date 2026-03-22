using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebBuySource.Dto.Request.Cart;
using WebBuySource.Interfaces;

namespace WebBuySource.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/v1/cart")]
	public class CartController : ControllerBase
	{
		private readonly ICartService _cartService;

		public CartController(ICartService cartService)
		{
			_cartService = cartService;
		}

		/// <summary>
		/// Thêm sản phẩm vào giỏ hàng
		/// </summary>
		[HttpPost("add")]
		public async Task<IActionResult> AddToCart([FromBody] AddToCartDTO request)
		{
			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
			var result = await _cartService.AddToCartAsync(userId, request);
			return Ok(result);
		}

		/// <summary>
		/// Lấy giỏ hàng của user
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> GetCart()
		{
			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
			var result = await _cartService.GetCartAsync(userId);
			return Ok(result);
		}

		/// <summary>
		/// Xóa item khỏi giỏ hàng
		/// </summary>
		[HttpDelete("{cartId}")]
		public async Task<IActionResult> RemoveFromCart(int cartId)
		{
			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
			var result = await _cartService.RemoveCartAsync(cartId, userId);
			return Ok(result);
		}

		/// <summary>
		/// Cập nhật số lượng
		/// </summary>
		[HttpPut("{cartId}/quantity")]
		public async Task<IActionResult> UpdateQuantity(int cartId, [FromBody] int quantity)
		{
			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
			var result = await _cartService.UpdateQuantityAsync(cartId, userId, quantity);
			return Ok(result);
		}

		/// <summary>
		/// Xóa toàn bộ giỏ hàng
		/// </summary>
		[HttpDelete("clear")]
		public async Task<IActionResult> ClearCart()
		{
			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
			var result = await _cartService.ClearCartAsync(userId);
			return Ok(result);
		}

		/// <summary>
		/// Chọn/bỏ chọn item
		/// </summary>
		[HttpPut("{cartId}/select")]
		public async Task<IActionResult> ToggleSelect(int cartId, [FromBody] bool isSelected)
		{
			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
			var result = await _cartService.ToggleSelectItemAsync(cartId, userId, isSelected);
			return Ok(result);
		}

		/// <summary>
		/// Lấy các item đã chọn
		/// </summary>
		[HttpGet("selected")]
		public async Task<IActionResult> GetSelectedItems()
		{
			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
			var result = await _cartService.GetSelectedItemsAsync(userId);
			return Ok(result);
		}
	}
}