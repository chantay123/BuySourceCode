using System.ComponentModel.DataAnnotations;
using WebBuySource.Models.Enums;

namespace WebBuySource.Models
{
    public class Permission : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Module { get; set; }

        [Required, MaxLength(50)]
        public string Action { get; set; }

        public string? Path { get; set; }
        public HTTPMethod? Method { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
