using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebBuySource.Models.Enums;

namespace WebBuySource.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BuyerId { get; set; }
        public User Buyer { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [MaxLength(10)]
        public string Currency { get; set; }

        public TransactionType Type { get; set; }

        public string? PaymentMethod { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; } 
        public int? CodeId { get; set; }
        public Code? Code { get; set; }

        public int? PromotionId { get; set; }
        public Promotion? Promotion { get; set; }

        public TransactionStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
