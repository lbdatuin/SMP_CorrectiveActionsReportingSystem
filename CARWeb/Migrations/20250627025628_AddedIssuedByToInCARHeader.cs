using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CARWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddedIssuedByToInCARHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IssuedTo",
                table: "CARHeaders",
                newName: "CARIssuedTo");

            migrationBuilder.RenameColumn(
                name: "IssuedBy",
                table: "CARHeaders",
                newName: "CARIssuedBy");

            migrationBuilder.CreateTable(
                name: "IssuedBy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CARHeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuedBy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssuedBy_CARHeaders_CARHeaderId",
                        column: x => x.CARHeaderId,
                        principalTable: "CARHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssuedTo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CARHeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuedTo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssuedTo_CARHeaders_CARHeaderId",
                        column: x => x.CARHeaderId,
                        principalTable: "CARHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssuedByItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DSectionId = table.Column<int>(type: "int", nullable: false),
                    IssuedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuedByItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssuedByItems_DSections_DSectionId",
                        column: x => x.DSectionId,
                        principalTable: "DSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssuedByItems_IssuedBy_IssuedById",
                        column: x => x.IssuedById,
                        principalTable: "IssuedBy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssuedToItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DSectionId = table.Column<int>(type: "int", nullable: false),
                    IssuedToId = table.Column<int>(type: "int", nullable: false),
                    IssuedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuedToItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssuedToItems_DSections_DSectionId",
                        column: x => x.DSectionId,
                        principalTable: "DSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssuedToItems_IssuedBy_IssuedById",
                        column: x => x.IssuedById,
                        principalTable: "IssuedBy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssuedToItems_IssuedTo_IssuedToId",
                        column: x => x.IssuedToId,
                        principalTable: "IssuedTo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssuedBy_CARHeaderId",
                table: "IssuedBy",
                column: "CARHeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssuedByItems_DSectionId",
                table: "IssuedByItems",
                column: "DSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_IssuedByItems_IssuedById",
                table: "IssuedByItems",
                column: "IssuedById");

            migrationBuilder.CreateIndex(
                name: "IX_IssuedTo_CARHeaderId",
                table: "IssuedTo",
                column: "CARHeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssuedToItems_DSectionId",
                table: "IssuedToItems",
                column: "DSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_IssuedToItems_IssuedById",
                table: "IssuedToItems",
                column: "IssuedById");

            migrationBuilder.CreateIndex(
                name: "IX_IssuedToItems_IssuedToId",
                table: "IssuedToItems",
                column: "IssuedToId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssuedByItems");

            migrationBuilder.DropTable(
                name: "IssuedToItems");

            migrationBuilder.DropTable(
                name: "IssuedBy");

            migrationBuilder.DropTable(
                name: "IssuedTo");

            migrationBuilder.RenameColumn(
                name: "CARIssuedTo",
                table: "CARHeaders",
                newName: "IssuedTo");

            migrationBuilder.RenameColumn(
                name: "CARIssuedBy",
                table: "CARHeaders",
                newName: "IssuedBy");
        }
    }
}
