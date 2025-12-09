namespace WebBuySource.Dto.Response.Code
{
    public class CodeResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string? Description { get; set; }

        public decimal Price { get; set; }
        public string? Currency { get; set; }

        public string? DemoUrl { get; set; }
        public string? PreviewImage { get; set; }
        public string? ThumbnailUrl { get; set; }

        public string Status { get; set; }

        public int Views { get; set; }
        public int Downloads { get; set; }
        public float AvgRating { get; set; }

        public bool IsFeatured { get; set; }
        public string LicenseType { get; set; }

        // Lookup IDs
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        public int ProgrammingLanguageId { get; set; }
    }
}
