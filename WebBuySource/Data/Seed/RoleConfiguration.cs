using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBuySource.Models;

namespace WebBuySource.Data.Seed
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {

            builder.HasData(
               new Role
               {
                   Id = 1,
                   Name = "User",
                   Description = "Regular system user",
                   CreatedAt = new DateTime(2025, 09, 29, 0, 0, 0, DateTimeKind.Utc),
                   UpdatedAt = new DateTime(2025, 09, 29, 0, 0, 0, DateTimeKind.Utc)
               },
               new Role
               {
                   Id = 2,
                   Name = "Admin",
                   Description = "Administrator with delegated permissions",
                   CreatedAt = new DateTime(2025, 09, 29, 0, 0, 0, DateTimeKind.Utc),
                   UpdatedAt = new DateTime(2025, 09, 29, 0, 0, 0, DateTimeKind.Utc)
               }
           );

        }
    }
}
