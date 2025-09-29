using System.ComponentModel.DataAnnotations;

namespace WebBuySource.Models
{
    public class Role : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }
        public bool IsSystem { get; set; } = false;

        public ICollection<User> Users { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
