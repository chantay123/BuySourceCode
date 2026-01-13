using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBuySource.Migrations
{
    /// <inheritdoc />
    public partial class creeaterolladdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "DeletedAt", "DeletedById", "Description", "IsSystem", "Name", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { 13, new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Regular system user", false, "User", new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 14, new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Administrator with delegated permissions", false, "Admin", new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Password", "RoleId", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 19, 9, 49, 14, 420, DateTimeKind.Utc).AddTicks(4306), "$2a$11$U1OvSAbnE5yWDDoM49IL8eJ05OQa6mpweSg6fuBYd9Qgz//67dxiW", 2, new DateTime(2025, 10, 19, 9, 49, 14, 420, DateTimeKind.Utc).AddTicks(4307) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "Balance", "CreatedAt", "CreatedById", "DeletedAt", "DeletedById", "Email", "FacebookId", "FailedLoginAttempts", "Fullname", "Gender", "GoogleId", "IsVerified", "LastLoginAt", "LastLoginIp", "LockedUntil", "Password", "PasswordChangedAt", "PhoneNumber", "RoleId", "Status", "SuspendedUntil", "SuspensionReason", "Timezone", "TotpEnabled", "TotpSecret", "UpdatedAt", "UpdatedById", "Username" },
                values: new object[] { 12, null, 0.00m, new DateTime(2025, 10, 19, 9, 49, 14, 420, DateTimeKind.Utc).AddTicks(4293), null, null, null, "duongquocnam224400@gmail.com", null, 0, "Administrator", "Male", null, true, null, null, null, "$2a$11$vBs6hv5n6kJu2j/iejVh0OXW/n5bFj8.dZo7qVFNFdgXz9kd5k9bu", null, "0123456789", 2, "ACTIVE", null, null, "Asia/Ho_Chi_Minh", false, null, new DateTime(2025, 10, 19, 9, 49, 14, 420, DateTimeKind.Utc).AddTicks(4294), null, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "DeletedAt", "DeletedById", "Description", "IsSystem", "Name", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { 10, new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Regular system user", false, "User", new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 11, new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Administrator with delegated permissions", false, "Admin", new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Password", "RoleId", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 19, 9, 47, 19, 904, DateTimeKind.Utc).AddTicks(7261), "$2a$11$eEhJZyrhj18tXyYnN93i6OzKcM756NRRWTkOVzkiMi4rYdwB1bCya", 11, new DateTime(2025, 10, 19, 9, 47, 19, 904, DateTimeKind.Utc).AddTicks(7262) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "Balance", "CreatedAt", "CreatedById", "DeletedAt", "DeletedById", "Email", "FacebookId", "FailedLoginAttempts", "Fullname", "Gender", "GoogleId", "IsVerified", "LastLoginAt", "LastLoginIp", "LockedUntil", "Password", "PasswordChangedAt", "PhoneNumber", "RoleId", "Status", "SuspendedUntil", "SuspensionReason", "Timezone", "TotpEnabled", "TotpSecret", "UpdatedAt", "UpdatedById", "Username" },
                values: new object[] { 10, null, 0.00m, new DateTime(2025, 10, 19, 9, 47, 19, 904, DateTimeKind.Utc).AddTicks(7247), null, null, null, "duongquocnam224400@gmail.com", null, 0, "Administrator", "Male", null, true, null, null, null, "$2a$11$A7llpL1aw9MwBDliP7k8D.AQEBW5D3u1qUcQsnKE8BYsfJqZB8fcW", null, "0123456789", 11, "ACTIVE", null, null, "Asia/Ho_Chi_Minh", false, null, new DateTime(2025, 10, 19, 9, 47, 19, 904, DateTimeKind.Utc).AddTicks(7248), null, "admin" });
        }
    }
}
