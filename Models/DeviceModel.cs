using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebBuySource.Models;

namespace YourNamespace.Models
{
    [Table("Devices")]
    public class Device
    {
        [Key]
        public int Id { get; set; }   // PK int

        [Required]
        public int UserId { get; set; }   // FK int -> User.Id

        [Required]
        [MaxLength(200)]
        public string DeviceId { get; set; } = string.Empty;  // unique device identifier

        [MaxLength(200)]
        public string? DeviceName { get; set; }

        [MaxLength(100)]
        public string? DeviceType { get; set; }   // mobile, desktop, tablet, tv, watch

        [MaxLength(100)]
        public string? Platform { get; set; }     // iOS, Android, Windows, macOS, Linux, Web

        [MaxLength(100)]
        public string? Browser { get; set; }

        [MaxLength(50)]
        public string? BrowserVersion { get; set; }

        [MaxLength(100)]
        public string? IpAddress { get; set; }

        public string? Location { get; set; } 

        public bool IsTrusted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public bool IsBlocked { get; set; } = false;

        public DateTime? LastSeenAt { get; set; }
        public DateTime FirstSeenAt { get; set; } = DateTime.UtcNow;

        public int LoginCount { get; set; } = 0;
        public int SessionCount { get; set; } = 0;
        public int MaxSessions { get; set; } = 5;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

   
        public virtual User User { get; set; } = null!;

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public virtual ICollection<VerificationCode> VerificationCodes { get; set; } = new List<VerificationCode>();
    }
}
