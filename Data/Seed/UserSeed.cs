using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBuySource.Models;
using WebBuySource.Models.Enums;

namespace WebBuySource.Data.Seed
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
 
            Env.Load(); 
            var adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL") ?? "admin@example.com";
            var adminPasswordRaw = Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? "Admin@123";
            var superAdminEmail = Environment.GetEnvironmentVariable("ADMIN1_EMAIL") ?? "superadmin@example.com";
            var superAdminPasswordRaw = Environment.GetEnvironmentVariable("ADMIN1_PASSWORD") ?? "SuperAdmin@123";

            var adminPassword = BCrypt.Net.BCrypt.HashPassword(adminPasswordRaw);
            var superAdminPassword = BCrypt.Net.BCrypt.HashPassword(superAdminPasswordRaw);

            builder.HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Fullname = "c",
                    Email = adminEmail,
                    Password = adminPassword,
                    PhoneNumber = "0123456789",
                    Gender = Gender.Male,
                    TotpEnabled = false,
                    Timezone = "Asia/Ho_Chi_Minh",
                    RoleId = 2, // Admin role ID bạn seed ở trên
                    Status = UserStatus.ACTIVE,
                    IsVerified = true,
                    Balance = 0.00m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new User
                { 
                    Id = 2,
                    Username = "admin",
                    Fullname = " Administrator",
                    Email = superAdminEmail,
                    Password = superAdminPassword,
                    PhoneNumber = "0987654321",
                    Gender = Gender.Male,
                    TotpEnabled = false,
                    Timezone = "Asia/Ho_Chi_Minh",
                    RoleId = 2,
                    Status = UserStatus.ACTIVE,
                    IsVerified = true,
                    Balance = 0.00m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
