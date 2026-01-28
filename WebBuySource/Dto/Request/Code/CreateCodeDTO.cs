using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Dto.Request.Code
{
    public class CreateCodeDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title must be <= 200 characters.")]
        public string Title { get; set; }

        [MaxLength(2000, ErrorMessage = "Description is too long.")]
        public string? Description { get; set; }

        [Range(0, 999999, ErrorMessage = "Price must be >= 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Currency is required.")]
        public string Currency { get; set; }

        [Url(ErrorMessage = "Invalid DemoUrl format.")]
        public string? DemoUrl { get; set; }

        [Url(ErrorMessage = "Invalid ThumbnailUrl format.")]
        public string? ThumbnailUrl { get; set; }

        public string? PreviewImage { get; set; }

        [Required(ErrorMessage = "SellerId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "SellerId must be > 0.")]
        public int SellerId { get; set; }

        [Required(ErrorMessage = "CategoryId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be > 0.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "ProgrammingLanguageId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ProgrammingLanguageId must be > 0.")]
        public int ProgrammingLanguageId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("ACTIVE|INACTIVE|DELETED",
            ErrorMessage = "Status must be ACTIVE, INACTIVE, or DELETED.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "LicenseType is required.")]
        public string LicenseType { get; set; }
    }
}
