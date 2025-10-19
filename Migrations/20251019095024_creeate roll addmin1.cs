using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBuySource.Migrations
{
    /// <inheritdoc />
    public partial class creeaterolladdmin1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 19, 9, 50, 24, 0, DateTimeKind.Utc).AddTicks(9941), "$2a$11$IqR4vscoYT.1jgvsRXGd6eXNypeMGMjE1wxSr9Nx/v/ct/VeUv7KO", new DateTime(2025, 10, 19, 9, 50, 24, 0, DateTimeKind.Utc).AddTicks(9943) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "Balance", "CreatedAt", "CreatedById", "DeletedAt", "DeletedById", "Email", "FacebookId", "FailedLoginAttempts", "Fullname", "Gender", "GoogleId", "IsVerified", "LastLoginAt", "LastLoginIp", "LockedUntil", "Password", "PasswordChangedAt", "PhoneNumber", "RoleId", "Status", "SuspendedUntil", "SuspensionReason", "Timezone", "TotpEnabled", "TotpSecret", "UpdatedAt", "UpdatedById", "Username" },
                values: new object[] { 13, null, 0.00m, new DateTime(2025, 10, 19, 9, 50, 24, 0, DateTimeKind.Utc).AddTicks(9958), null, null, null, "phanchantay.ltp21@gmail.com", null, 0, "System Administrator", "Male", null, true, null, null, null, "$2a$11$zctxBI0YfV4CD..dBkLxa.6/oh2sZ84iEgeSWHMvtR6dxVVFzGzju", null, "0987654321", 2, "ACTIVE", null, null, "Asia/Ho_Chi_Minh", false, null, new DateTime(2025, 10, 19, 9, 50, 24, 0, DateTimeKind.Utc).AddTicks(9959), null, "superadmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 19, 9, 49, 14, 420, DateTimeKind.Utc).AddTicks(4293), "$2a$11$vBs6hv5n6kJu2j/iejVh0OXW/n5bFj8.dZo7qVFNFdgXz9kd5k9bu", new DateTime(2025, 10, 19, 9, 49, 14, 420, DateTimeKind.Utc).AddTicks(4294) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "Balance", "CreatedAt", "CreatedById", "DeletedAt", "DeletedById", "Email", "FacebookId", "FailedLoginAttempts", "Fullname", "Gender", "GoogleId", "IsVerified", "LastLoginAt", "LastLoginIp", "LockedUntil", "Password", "PasswordChangedAt", "PhoneNumber", "RoleId", "Status", "SuspendedUntil", "SuspensionReason", "Timezone", "TotpEnabled", "TotpSecret", "UpdatedAt", "UpdatedById", "Username" },
                values: new object[] { 11, null, 0.00m, new DateTime(2025, 10, 19, 9, 49, 14, 420, DateTimeKind.Utc).AddTicks(4306), null, null, null, "phanchantay.ltp21@gmail.com", null, 0, "System Administrator", "Male", null, true, null, null, null, "$2a$11$U1OvSAbnE5yWDDoM49IL8eJ05OQa6mpweSg6fuBYd9Qgz//67dxiW", null, "0987654321", 2, "ACTIVE", null, null, "Asia/Ho_Chi_Minh", false, null, new DateTime(2025, 10, 19, 9, 49, 14, 420, DateTimeKind.Utc).AddTicks(4307), null, "superadmin" });
        }
    }
}
