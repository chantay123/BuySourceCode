using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Models
{
    public class AuditLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string Action { get; set; } 

        [Required]
        public string TargetType { get; set; } 

        public string? TargetId { get; set; } 
        public string? Description { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
