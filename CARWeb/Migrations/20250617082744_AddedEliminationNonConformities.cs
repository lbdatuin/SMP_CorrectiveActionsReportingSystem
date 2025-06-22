using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CARWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddedEliminationNonConformities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EliminationNonConformities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSimilarSituation = table.Column<bool>(type: "bit", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    IsSimilarSituationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSimilarSituationFiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsWhyWhy = table.Column<bool>(type: "bit", nullable: false),
                    IsFishBone = table.Column<bool>(type: "bit", nullable: false),
                    IsFaultTree = table.Column<bool>(type: "bit", nullable: false),
                    IsOthers = table.Column<bool>(type: "bit", nullable: false),
                    MethodFiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOthersDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RootCaseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnalyzedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnalyzedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkerRepresentative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CARHeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EliminationNonConformities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EliminationNonConformities_CARHeaders_CARHeaderId",
                        column: x => x.CARHeaderId,
                        principalTable: "CARHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EliminationNonConformities_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EliminationNonConformities_CARHeaderId",
                table: "EliminationNonConformities",
                column: "CARHeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EliminationNonConformities_DepartmentId",
                table: "EliminationNonConformities",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EliminationNonConformities");
        }
    }
}
