using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBuySource.Models
{
	public class Cart : BaseModel
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int UserId { get; set; }

		[Required]
		public int CodeId { get; set; }

		// Navigation properties
		[ForeignKey("UserId")]
		public virtual User User { get; set; } = null!;

		[ForeignKey("CodeId")]
		public virtual Code Code { get; set; } = null!;

		// Cart item properties
		public int Quantity { get; set; } = 1;

		[Column(TypeName = "decimal(18,2)")]
		public decimal? PriceAtAdd { get; set; } // Lưu giá tại thời điểm thêm vào giỏ

		public bool IsSelected { get; set; } = true; // Chọn để thanh toán

		public DateTime? ExpiredAt { get; set; } // Giỏ hàng hết hạn sau 24h

		// Ghi đè từ BaseModel
		public new DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public new DateTime? UpdatedAt { get; set; }
	}
}



//namespace WebBuySource.Models
//{
//	public class Cart
//	{
//		public int Id { get; set; }

//		public int UserId { get; set; }
//		public User User { get; set; }

//		public int CodeId { get; set; }
//		public Code Code { get; set; }

//		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//	}
//}