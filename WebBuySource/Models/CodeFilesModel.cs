using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Models
{
    public class CodeFile : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CodeId { get; set; }
        public Code Code { get; set; }

        [Required, MaxLength(500)]
        public string? FileUrl { get; set; }

        public long? FileSize { get; set; }

        [Required, MaxLength(50)]
        public string? Version { get; set; }

        public bool IsCurrent { get; set; } = true;

        public int? UploadedById { get; set; }
  
        public User? UploadedBy { get; set; }
        public User? UpdatedBy { get; set; }
        public User? DeletedBy { get; set; }
    }
}
