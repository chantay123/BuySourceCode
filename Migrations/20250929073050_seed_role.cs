using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBuySource.Migrations
{
    /// <inheritdoc />
    public partial class seed_role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "DeletedAt", "DeletedById", "Description", "IsSystem", "Name", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { 8, new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Regular system user", false, "User", new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 9, new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Administrator with delegated permissions", false, "Admin", new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "DeletedAt", "DeletedById", "Description", "IsSystem", "Name", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Regular system user", false, "User", new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { 2, new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, "Administrator with delegated permissions", false, "Admin", new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });
        }
    }
}
