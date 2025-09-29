using System.ComponentModel.DataAnnotations;
using WebBuySource.Models.Enums;

namespace WebBuySource.Models
{
    public class VerificationCode
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public VerificationCodeType Type { get; set; }

        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; } = false;

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
