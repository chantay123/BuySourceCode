using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Models
{
    public class ProgrammingLanguage : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }   

        [MaxLength(50)]
        public string? Description { get; set; }
        
        public string? Code { get; set; }

        public bool? isActive { get; set; }


        public ICollection<Code> Codes { get; set; }
    }
}
