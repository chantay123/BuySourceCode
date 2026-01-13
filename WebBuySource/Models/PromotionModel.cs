using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebBuySource.Models.Enums;

namespace WebBuySource.Models
{
    public class Promotion : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Code { get; set; } // unique

        public string? Description { get; set; }

        [Required]
        public PromotionDiscountType DiscountType { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal DiscountValue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MinPurchaseAmount { get; set; } = 0.00m;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int? UsageLimit { get; set; }
        public int UsageCount { get; set; } = 0;
        public PromotionStatus Status { get; set; } = PromotionStatus.ACTIVE;

        public int? CreatedById { get; set; }
        public User? CreatedBy { get; set; }

        public int? UpdatedById { get; set; }
        public User? UpdatedBy { get; set; }

        public int? DeletedById { get; set; }
        public User? DeletedBy { get; set; }

        public ICollection<CodePromotion> CodePromotions { get; set; } = new List<CodePromotion>();
    }
}
