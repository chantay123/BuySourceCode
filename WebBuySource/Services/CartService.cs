using Microsoft.EntityFrameworkCore;
using WebBuySource.Dto.Request.Cart;
using WebBuySource.Dto.Response;
using WebBuySource.Dto.Response.Cart;
using WebBuySource.Interfaces;
using WebBuySource.Models;
using WebBuySource.Utilities;

namespace WebBuySource.Services
{
	public class CartService : ICartService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CartService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<BaseAPIResponse> AddToCartAsync(int userId, AddToCartDTO request)
		{
			// 1. Kiểm tra code tồn tại
			var code = await _unitOfWork.CodeRepository
				.FirstOrDefaultAsyncAsNoTracking(x => x.Id == request.CodeId && x.DeletedAt == null);

			if (code == null)
				return BaseApiResponse.NotFound("CODE_NOT_FOUND");

			// 2. Kiểm tra user không mua code của chính mình
			if (code.SellerId == userId)
				return BaseApiResponse.Error("CANNOT_BUY_OWN_CODE");

			// 3. Kiểm tra code đã có trong giỏ chưa
			var existing = await _unitOfWork.CartRepository
				.FirstOrDefaultAsync(x =>
					x.UserId == userId &&
					x.CodeId == request.CodeId &&
					x.DeletedAt == null);

			if (existing != null)
			{
				// Nếu có rồi thì tăng số lượng
				existing.Quantity += request.Quantity;
				existing.UpdatedAt = DateTime.UtcNow;
				_unitOfWork.CartRepository.Update(existing);
				_unitOfWork.Commit();

				return BaseApiResponse.OK(new
				{
					cartId = existing.Id,
					message = "Cart updated successfully"
				});
			}

			// 4. Thêm mới vào giỏ
			var cart = new Cart
			{
				UserId = userId,
				CodeId = request.CodeId,
				Quantity = request.Quantity,
				PriceAtAdd = code.Price,
				CreatedAt = DateTime.UtcNow,
				ExpiredAt = DateTime.UtcNow.AddDays(1)
			};

			await _unitOfWork.CartRepository.AddAsync(cart);
			_unitOfWork.Commit();

			return BaseApiResponse.OK(new
			{
				cartId = cart.Id,
				message = "Added to cart successfully"
			});
		}

		public async Task<BaseAPIResponse> GetCartAsync(int userId)
		{
			var carts = await _unitOfWork.CartRepository
				.GetAll()
				.Include(c => c.Code)
				.Where(x => x.UserId == userId && x.DeletedAt == null)
				.OrderByDescending(x => x.CreatedAt)
				.ToListAsync();

			// Xóa các item hết hạn
			var expiredItems = carts.Where(x => x.ExpiredAt < DateTime.UtcNow).ToList();
			if (expiredItems.Any())
			{
				foreach (var item in expiredItems)
				{
					item.DeletedAt = DateTime.UtcNow;
					_unitOfWork.CartRepository.Update(item);
				}
				_unitOfWork.Commit();
				carts = carts.Where(x => x.ExpiredAt >= DateTime.UtcNow).ToList();
			}

			var result = carts.Select(x => new CartResponseDTO
			{
				CartId = x.Id,
				CodeId = x.CodeId,
				CodeTitle = x.Code?.Title ?? "Unknown",
				CodeDescription = x.Code?.Description,
				PreviewImage = x.Code?.PreviewImage,
				Price = x.Code?.Price ?? 0,
				Quantity = x.Quantity,
				IsSelected = x.IsSelected,
				CreatedAt = x.CreatedAt,
				ExpiredAt = x.ExpiredAt
			}).ToList();

			var summary = new
			{
				items = result,
				totalItems = result.Count,
				totalSelectedItems = result.Count(x => x.IsSelected),
				totalPrice = result.Where(x => x.IsSelected).Sum(x => x.TotalPrice),
				totalPriceAll = result.Sum(x => x.TotalPrice)
			};

			return BaseApiResponse.OK(summary);
		}

		public async Task<BaseAPIResponse> RemoveCartAsync(int cartId, int userId)
		{
			var cart = await _unitOfWork.CartRepository
				.FirstOrDefaultAsync(x =>
					x.Id == cartId &&
					x.UserId == userId &&
					x.DeletedAt == null);

			if (cart == null)
				return BaseApiResponse.NotFound("CART_ITEM_NOT_FOUND");

			// Soft delete
			cart.DeletedAt = DateTime.UtcNow;
			_unitOfWork.CartRepository.Update(cart);
			_unitOfWork.Commit();

			return BaseApiResponse.OK(new
			{
				cartId = cart.Id,
				message = "Item removed from cart"
			});
		}

		public async Task<BaseAPIResponse> UpdateQuantityAsync(int cartId, int userId, int quantity)
		{
			if (quantity <= 0)
				return BaseApiResponse.Error("INVALID_QUANTITY");

			var cart = await _unitOfWork.CartRepository
				.FirstOrDefaultAsync(x =>
					x.Id == cartId &&
					x.UserId == userId &&
					x.DeletedAt == null);

			if (cart == null)
				return BaseApiResponse.NotFound("CART_ITEM_NOT_FOUND");

			cart.Quantity = quantity;
			cart.UpdatedAt = DateTime.UtcNow;
			_unitOfWork.CartRepository.Update(cart);
			_unitOfWork.Commit();

			return BaseApiResponse.OK(new
			{
				cartId = cart.Id,
				quantity = cart.Quantity
			});
		}

		public async Task<BaseAPIResponse> ClearCartAsync(int userId)
		{
			var carts = await _unitOfWork.CartRepository
				.GetAll()
				.Where(x => x.UserId == userId && x.DeletedAt == null)
				.ToListAsync();

			foreach (var cart in carts)
			{
				cart.DeletedAt = DateTime.UtcNow;
				_unitOfWork.CartRepository.Update(cart);
			}
			_unitOfWork.Commit();

			return BaseApiResponse.OK(new
			{
				message = "Cart cleared successfully"
			});
		}

		public async Task<BaseAPIResponse> ToggleSelectItemAsync(int cartId, int userId, bool isSelected)
		{
			var cart = await _unitOfWork.CartRepository
				.FirstOrDefaultAsync(x =>
					x.Id == cartId &&
					x.UserId == userId &&
					x.DeletedAt == null);

			if (cart == null)
				return BaseApiResponse.NotFound("CART_ITEM_NOT_FOUND");

			cart.IsSelected = isSelected;
			cart.UpdatedAt = DateTime.UtcNow;
			_unitOfWork.CartRepository.Update(cart);
			_unitOfWork.Commit();

			return BaseApiResponse.OK(new
			{
				cartId = cart.Id,
				isSelected = cart.IsSelected
			});
		}

		public async Task<BaseAPIResponse> GetSelectedItemsAsync(int userId)
		{
			var selectedItems = await _unitOfWork.CartRepository
				.GetAll()
				.Include(c => c.Code)
				.Where(x => x.UserId == userId &&
						   x.DeletedAt == null &&
						   x.IsSelected)
				.ToListAsync();

			var result = selectedItems.Select(x => new CartResponseDTO
			{
				CartId = x.Id,
				CodeId = x.CodeId,
				CodeTitle = x.Code?.Title ?? "Unknown",
				Price = x.Code?.Price ?? 0,
				Quantity = x.Quantity,
				IsSelected = x.IsSelected
				//TotalPrice = (x.Code?.Price ?? 0) * x.Quantity,
			}).ToList();

			return BaseApiResponse.OK(new
			{
				items = result,
				totalSelected = result.Count,
				totalAmount = result.Sum(x => x.TotalPrice)
			});
		}
	}
}