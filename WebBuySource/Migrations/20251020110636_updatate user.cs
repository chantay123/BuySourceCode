using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBuySource.Migrations
{
    /// <inheritdoc />
    public partial class updatateuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "RefreshTokens",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId1",
                table: "RefreshTokens",
                column: "UserId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UserId1",
                table: "RefreshTokens",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UserId1",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserId1",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "RefreshTokens");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "DeletedAt", "DeletedById", "Description", "IsSystem", "Name", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { 13, new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Regular system user", false, "User", new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 14, new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Administrator with delegated permissions", false, "Admin", new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "Balance", "CreatedAt", "CreatedById", "DeletedAt", "DeletedById", "Email", "FacebookId", "FailedLoginAttempts", "Fullname", "Gender", "GoogleId", "IsVerified", "LastLoginAt", "LastLoginIp", "LockedUntil", "Password", "PasswordChangedAt", "PhoneNumber", "RoleId", "Status", "SuspendedUntil", "SuspensionReason", "Timezone", "TotpEnabled", "TotpSecret", "UpdatedAt", "UpdatedById", "Username" },
                values: new object[,]
                {
                    { 12, null, 0.00m, new DateTime(2025, 10, 19, 9, 50, 24, 0, DateTimeKind.Utc).AddTicks(9941), null, null, null, "duongquocnam224400@gmail.com", null, 0, "Administrator", "Male", null, true, null, null, null, "$2a$11$IqR4vscoYT.1jgvsRXGd6eXNypeMGMjE1wxSr9Nx/v/ct/VeUv7KO", null, "0123456789", 2, "ACTIVE", null, null, "Asia/Ho_Chi_Minh", false, null, new DateTime(2025, 10, 19, 9, 50, 24, 0, DateTimeKind.Utc).AddTicks(9943), null, "admin" },
                    { 13, null, 0.00m, new DateTime(2025, 10, 19, 9, 50, 24, 0, DateTimeKind.Utc).AddTicks(9958), null, null, null, "phanchantay.ltp21@gmail.com", null, 0, "System Administrator", "Male", null, true, null, null, null, "$2a$11$zctxBI0YfV4CD..dBkLxa.6/oh2sZ84iEgeSWHMvtR6dxVVFzGzju", null, "0987654321", 2, "ACTIVE", null, null, "Asia/Ho_Chi_Minh", false, null, new DateTime(2025, 10, 19, 9, 50, 24, 0, DateTimeKind.Utc).AddTicks(9959), null, "superadmin" }
                });
        }
    }
}
