using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public string? ImageUrl { get; set; }
        public Category? Parent { get; set; }
        public ICollection<Category> SubCategories { get; set; }
        public ICollection<Code> Codes { get; set; }
    }
}
