using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Models
{
    public class Language : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }   

        [Required, MaxLength(50)]
        public string Code { get; set; }   

        [MaxLength(200)]
        public string? Paradigm { get; set; } 

        [MaxLength(500)]
        public string? Website { get; set; } 

        public bool IsActive { get; set; } = true;

        public User? CreatedBy { get; set; }

        public User? UpdatedBy { get; set; }
      
        public User? DeletedBy { get; set; }

       

        public ICollection<Code> Codes { get; set; }
    }
}
