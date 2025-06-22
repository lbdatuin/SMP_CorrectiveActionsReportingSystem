using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CARWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddedModelForStatuseffectiveness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CorrectiveActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonResponsible = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentHead = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewerDesignation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalCommunicationFiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsManagementOfChange = table.Column<bool>(type: "bit", nullable: false),
                    ManagementOfChangeFiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CARHeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectiveActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CorrectiveActions_CARHeaders_CARHeaderId",
                        column: x => x.CARHeaderId,
                        principalTable: "CARHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FollowUpStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Evidences = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusOfActions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CARHeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUpStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowUpStatus_CARHeaders_CARHeaderId",
                        column: x => x.CARHeaderId,
                        principalTable: "CARHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IMVerifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsQA = table.Column<bool>(type: "bit", nullable: false),
                    QAReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsQB = table.Column<bool>(type: "bit", nullable: false),
                    QBReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsQC = table.Column<bool>(type: "bit", nullable: false),
                    QCReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsQD = table.Column<bool>(type: "bit", nullable: false),
                    QDReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsQE = table.Column<bool>(type: "bit", nullable: false),
                    QEReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseOfAction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CARHeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMVerifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IMVerifications_CARHeaders_CARHeaderId",
                        column: x => x.CARHeaderId,
                        principalTable: "CARHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusOfEffectiveness",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsS1 = table.Column<bool>(type: "bit", nullable: false),
                    IsS2 = table.Column<bool>(type: "bit", nullable: false),
                    IsS3 = table.Column<bool>(type: "bit", nullable: false),
                    VerifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CARHeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusOfEffectiveness", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusOfEffectiveness_CARHeaders_CARHeaderId",
                        column: x => x.CARHeaderId,
                        principalTable: "CARHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorrectiveActionItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CAction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Responsible = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CorrectiveActionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectiveActionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CorrectiveActionItems_CorrectiveActions_CorrectiveActionId",
                        column: x => x.CorrectiveActionId,
                        principalTable: "CorrectiveActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActionItems_CorrectiveActionId",
                table: "CorrectiveActionItems",
                column: "CorrectiveActionId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActions_CARHeaderId",
                table: "CorrectiveActions",
                column: "CARHeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpStatus_CARHeaderId",
                table: "FollowUpStatus",
                column: "CARHeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IMVerifications_CARHeaderId",
                table: "IMVerifications",
                column: "CARHeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusOfEffectiveness_CARHeaderId",
                table: "StatusOfEffectiveness",
                column: "CARHeaderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrectiveActionItems");

            migrationBuilder.DropTable(
                name: "FollowUpStatus");

            migrationBuilder.DropTable(
                name: "IMVerifications");

            migrationBuilder.DropTable(
                name: "StatusOfEffectiveness");

            migrationBuilder.DropTable(
                name: "CorrectiveActions");
        }
    }
}
