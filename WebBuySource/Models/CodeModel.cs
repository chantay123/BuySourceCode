using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Models
{
    public class Code:BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string? Title { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; } 
        public string? Currency { get; set; } 

        public string? DemoUrl { get; set; }
        public string? PreviewImage { get; set; }
        public string? ThumbnailUrl { get; set; }

        public int SellerId { get; set; }
        public User Seller { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ProgrammingLanguageId { get; set; }
        public ProgrammingLanguage ProgrammingLanguage { get; set; }

        public string Status { get; set; } 
        public int Views { get; set; }
        public int Downloads { get; set; } 
        public float AvgRating { get; set; } 
        public bool IsFeatured { get; set; } 
        public string LicenseType { get; set; }

       
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Download> Download { get; set; }
        public ICollection<UserFavorites> Favorites { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<CodeTag> CodeTags { get; set; }

        public ICollection<Conversation> Conversations { get; set; }

        public ICollection<CodeFile> CodeFiles { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<CodePromotion > CodePromotions { get; set; }
    }
}
