using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBuySource.Migrations
{
    public partial class fix_table_like : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CodeLikes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CodeLikes");
        }
    }
}
