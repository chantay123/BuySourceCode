using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBuySource.Migrations
{
    public partial class add_table_like : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CodeLikes",
                columns: table => new
                {
                    UserId = table.Column<int>(
                        type: "integer",
                        nullable: false),

                    CodeId = table.Column<int>(
                        type: "integer",
                        nullable: false),

                    DateLastMant = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        name: "PK_CodeLikes",
                        x => new { x.UserId, x.CodeId });

                    table.ForeignKey(
                        name: "FK_CodeLikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_CodeLikes_Codes_CodeId",
                        column: x => x.CodeId,
                        principalTable: "Codes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeLikes_CodeId",
                table: "CodeLikes",
                column: "CodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeLikes");
        }
    }
}
