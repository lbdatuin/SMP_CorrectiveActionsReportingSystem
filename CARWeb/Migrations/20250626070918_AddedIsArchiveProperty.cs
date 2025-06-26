using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CARWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsArchiveProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchive",
                table: "CARHeaders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIneffective",
                table: "CARHeaders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchive",
                table: "CARHeaders");

            migrationBuilder.DropColumn(
                name: "IsIneffective",
                table: "CARHeaders");
        }
    }
}
