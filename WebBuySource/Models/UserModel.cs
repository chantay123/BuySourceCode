
using System.ComponentModel.DataAnnotations;
using WebBuySource.Models.Enums;
using YourNamespace.Models;

namespace WebBuySource.Models
{
    public class User : BaseModel
    {
        [Key]
        public int Id { get; set; }

        public required string Username { get; set; }
        public required string Fullname { get; set; }

        [Required, MaxLength(150)]
        public required string Email { get; set; }

        [MaxLength(100)]
        public required string Password { get; set; }
      
        public string? PhoneNumber { get; set; }
        public Gender? Gender { get; set; }
        public string? Avatar { get; set; }

        public string? TotpSecret { get; set; }
        public bool TotpEnabled { get; set; } = false;

        public DateTime? LastLoginAt { get; set; }
        public string? LastLoginIp { get; set; }
        public DateTime? PasswordChangedAt { get; set; }
        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? LockedUntil { get; set; }

        public string Timezone { get; set; } = "Asia/Ho_Chi_Minh";

        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }
         
        public RefreshToken RefreshToken { get; set; }
        public string? GoogleId { get; set; }
        public string? FacebookId { get; set; }

        public UserStatus Status { get; set; } = UserStatus.ACTIVE;
        public DateTime? SuspendedUntil { get; set; }
        public string? SuspensionReason { get; set; }
        public bool IsVerified { get; set; } = false;
        public decimal Balance { get; set; } = 0.00m;

        public ICollection<Conversation> ConversationsAsBuyer { get; set; }
        public ICollection<Conversation> ConversationsAsAdmin { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Payout> Payouts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Download> Downloads { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<AuditLog> AuditLogs { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<UserFavorites> UserFavorites { get; set; }
        public ICollection<Code> Codes { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public ICollection<CodeLike> CodeLikes { get; set; } 
    }
}
