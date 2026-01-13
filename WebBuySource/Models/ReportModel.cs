using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CodeId { get; set; }
        public Code Code { get; set; }

        [Required]
        public int ReporterId { get; set; }
        public User Reporter { get; set; }

        [Required]
        public string Reason { get; set; }

        public string Status { get; set; } = "PENDING"; 

        public int? ResolvedById { get; set; }
        public User? ResolvedBy { get; set; }

        public string? ResolutionNotes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
