using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Models
{
    public class Tag : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Slug { get; set; }

        public string? Description { get; set; }

        public ICollection<CodeTag> CodeTags { get; set; }
    }
}
