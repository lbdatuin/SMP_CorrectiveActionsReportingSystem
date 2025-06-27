using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CARWeb.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedRelOFIssuedByItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssuedToItems_IssuedBy_IssuedById",
                table: "IssuedToItems");

            migrationBuilder.DropIndex(
                name: "IX_IssuedToItems_IssuedById",
                table: "IssuedToItems");

            migrationBuilder.DropColumn(
                name: "IssuedById",
                table: "IssuedToItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IssuedById",
                table: "IssuedToItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssuedToItems_IssuedById",
                table: "IssuedToItems",
                column: "IssuedById");

            migrationBuilder.AddForeignKey(
                name: "FK_IssuedToItems_IssuedBy_IssuedById",
                table: "IssuedToItems",
                column: "IssuedById",
                principalTable: "IssuedBy",
                principalColumn: "Id");
        }
    }
}
