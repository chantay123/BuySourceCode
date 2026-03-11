using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBuySource.Migrations
{
    /// <inheritdoc />
    public partial class addtablecodelike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodeTagCodeId",
                table: "Tags",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CodeTagTagId",
                table: "Tags",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CodeTagCodeId",
                table: "Codes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CodeTagTagId",
                table: "Codes",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CodeId",
                table: "CodeLikes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CodeLikes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "CodeLikes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CodeTagCodeId_CodeTagTagId",
                table: "Tags",
                columns: new[] { "CodeTagCodeId", "CodeTagTagId" });

            migrationBuilder.CreateIndex(
                name: "IX_Codes_CodeTagCodeId_CodeTagTagId",
                table: "Codes",
                columns: new[] { "CodeTagCodeId", "CodeTagTagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Codes_CodeTags_CodeTagCodeId_CodeTagTagId",
                table: "Codes",
                columns: new[] { "CodeTagCodeId", "CodeTagTagId" },
                principalTable: "CodeTags",
                principalColumns: new[] { "CodeId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_CodeTags_CodeTagCodeId_CodeTagTagId",
                table: "Tags",
                columns: new[] { "CodeTagCodeId", "CodeTagTagId" },
                principalTable: "CodeTags",
                principalColumns: new[] { "CodeId", "TagId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codes_CodeTags_CodeTagCodeId_CodeTagTagId",
                table: "Codes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_CodeTags_CodeTagCodeId_CodeTagTagId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CodeTagCodeId_CodeTagTagId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Codes_CodeTagCodeId_CodeTagTagId",
                table: "Codes");

            migrationBuilder.DropColumn(
                name: "CodeTagCodeId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CodeTagTagId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CodeTagCodeId",
                table: "Codes");

            migrationBuilder.DropColumn(
                name: "CodeTagTagId",
                table: "Codes");

            migrationBuilder.DropColumn(
                name: "id",
                table: "CodeLikes");

            migrationBuilder.AlterColumn<int>(
                name: "CodeId",
                table: "CodeLikes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CodeLikes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 0);
        }
    }
}
