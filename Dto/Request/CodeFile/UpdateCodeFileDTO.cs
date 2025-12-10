using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Dto.Request.CodeFile
{
    public class UpdateCodeFileDTO
    {
        [MaxLength(500)]
        public string? FileUrl { get; set; }

        public long? FileSize { get; set; }

        [MaxLength(50)]
        public string? Version { get; set; }

        public bool? IsCurrent { get; set; }
    }
}
