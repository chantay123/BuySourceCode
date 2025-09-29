using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBuySource.Models;
using System;

namespace WebBuySource.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
           
            builder.HasData(
             new Role
             {
                 Id = 8,
                 Name = "User",
                 Description = "Regular system user",
                 CreatedAt = new DateTime(2025, 09, 29, 0, 0, 0, DateTimeKind.Utc),
                 UpdatedAt = new DateTime(2025, 09, 29, 0, 0, 0, DateTimeKind.Utc)
             },
             new Role
             {
                 Id = 9,
                 Name = "Admin",
                 Description = "Administrator with delegated permissions",
                 CreatedAt = new DateTime(2025, 09, 29, 0, 0, 0, DateTimeKind.Utc),
                 UpdatedAt = new DateTime(2025, 09, 29, 0, 0, 0, DateTimeKind.Utc)
             }
         );

        }
    }
}
