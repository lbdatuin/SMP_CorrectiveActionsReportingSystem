using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CARWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddedCARHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CARHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SysRefNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CARNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RevisionNo = table.Column<int>(type: "int", nullable: true),
                    RevisionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recurring = table.Column<bool>(type: "bit", nullable: false),
                    NonRecurring = table.Column<bool>(type: "bit", nullable: false),
                    IssuedTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Clauses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CARTypeId = table.Column<int>(type: "int", nullable: true),
                    TypeOfFinding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfAccident = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CARHeaders_CARTypes_CARTypeId",
                        column: x => x.CARTypeId,
                        principalTable: "CARTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DetailsOfIssues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetailsOfIssueDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetailsOfIssueFiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvidenceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvidenceFiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequirementsDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequirementsFiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CARHeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsOfIssues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailsOfIssues_CARHeaders_CARHeaderId",
                        column: x => x.CARHeaderId,
                        principalTable: "CARHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImmediateCorrections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionsToCorrectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionsToCorrectFiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionsToDealDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionsToDealFiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CARHeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmediateCorrections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImmediateCorrections_CARHeaders_CARHeaderId",
                        column: x => x.CARHeaderId,
                        principalTable: "CARHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NonConformityItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CARHeaderId = table.Column<int>(type: "int", nullable: false),
                    NonConformityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonConformityItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NonConformityItems_CARHeaders_CARHeaderId",
                        column: x => x.CARHeaderId,
                        principalTable: "CARHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NonConformityItems_NonConformities_NonConformityId",
                        column: x => x.NonConformityId,
                        principalTable: "NonConformities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StandardItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CARHeaderId = table.Column<int>(type: "int", nullable: false),
                    StandardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandardItems_CARHeaders_CARHeaderId",
                        column: x => x.CARHeaderId,
                        principalTable: "CARHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandardItems_Standards_StandardId",
                        column: x => x.StandardId,
                        principalTable: "Standards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CARHeaders_CARTypeId",
                table: "CARHeaders",
                column: "CARTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsOfIssues_CARHeaderId",
                table: "DetailsOfIssues",
                column: "CARHeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImmediateCorrections_CARHeaderId",
                table: "ImmediateCorrections",
                column: "CARHeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NonConformityItems_CARHeaderId",
                table: "NonConformityItems",
                column: "CARHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_NonConformityItems_NonConformityId",
                table: "NonConformityItems",
                column: "NonConformityId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardItems_CARHeaderId",
                table: "StandardItems",
                column: "CARHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardItems_StandardId",
                table: "StandardItems",
                column: "StandardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailsOfIssues");

            migrationBuilder.DropTable(
                name: "ImmediateCorrections");

            migrationBuilder.DropTable(
                name: "NonConformityItems");

            migrationBuilder.DropTable(
                name: "StandardItems");

            migrationBuilder.DropTable(
                name: "CARHeaders");
        }
    }
}
