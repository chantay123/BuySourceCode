using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Models
{
    public class Download
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CodeId { get; set; }
        public Code Code { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
