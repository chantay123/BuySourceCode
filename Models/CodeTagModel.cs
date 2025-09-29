using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Models
{
    public class CodeTag
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public int CodeId { get; set; }
        public Code Code { get; set; }

        [Required]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
