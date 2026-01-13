using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace WebBuySource.Models
{
    public class Payout
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SellerId { get; set; }
        public User Seller { get; set; }

        public int? TransactionId { get; set; }
        public Transaction? Transaction { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [MaxLength(10)]
        public string Currency { get; set; } = "USD";

        public string? PayoutMethod { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Fee { get; set; } = 0.00m;

        public TransactionStatus Status { get; set; } 

        public DateTime? ProcessedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
