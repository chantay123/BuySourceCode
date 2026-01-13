using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Models
{
    public class RefreshToken: BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
