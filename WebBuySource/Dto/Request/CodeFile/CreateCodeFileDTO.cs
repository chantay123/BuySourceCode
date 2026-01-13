using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Dto.Request.CodeFile
{
    public class CreateCodeFileDTO
    {
        [Required]
        public int CodeId { get; set; }

        [Required, MaxLength(500)]
        public string FileUrl { get; set; }
        public long? FileSize { get; set; }

        [Required, MaxLength(50)]
        public string Version { get; set; }

        public bool IsCurrent { get; set; } = true;
    }
}
