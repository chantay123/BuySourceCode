using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Models
{
    public class Tag : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }
        public User? CreatedBy { get; set; }
        public User? UpdatedBy { get; set; }
        public User? DeletedBy { get; set; }

 
        public ICollection<CodeTag> CodeTags { get; set; }
    }
}
