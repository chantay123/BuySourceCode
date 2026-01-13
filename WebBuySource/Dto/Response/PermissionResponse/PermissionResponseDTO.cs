using WebBuySource.Models.Enums;

namespace WebBuySource.Dto.Response.PermissionResponse
{
    public class PermissionResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Module { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string? Path { get; set; }
        public HTTPMethod? Method { get; set; }
    }
}
