using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CARWeb.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedFollowUpStatusModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VerifiedBy",
                table: "FollowUpStatus",
                newName: "F3VerifiedBy");

            migrationBuilder.RenameColumn(
                name: "StatusOfActions",
                table: "FollowUpStatus",
                newName: "F3StatusOfActions");

            migrationBuilder.RenameColumn(
                name: "Evidences",
                table: "FollowUpStatus",
                newName: "F3Evidences");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "FollowUpStatus",
                newName: "F3Date");

            migrationBuilder.AddColumn<DateTime>(
                name: "F1Date",
                table: "FollowUpStatus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "F1Evidences",
                table: "FollowUpStatus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "F1StatusOfActions",
                table: "FollowUpStatus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "F1VerifiedBy",
                table: "FollowUpStatus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "F2Date",
                table: "FollowUpStatus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "F2Evidences",
                table: "FollowUpStatus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "F2StatusOfActions",
                table: "FollowUpStatus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "F2VerifiedBy",
                table: "FollowUpStatus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "F1Date",
                table: "FollowUpStatus");

            migrationBuilder.DropColumn(
                name: "F1Evidences",
                table: "FollowUpStatus");

            migrationBuilder.DropColumn(
                name: "F1StatusOfActions",
                table: "FollowUpStatus");

            migrationBuilder.DropColumn(
                name: "F1VerifiedBy",
                table: "FollowUpStatus");

            migrationBuilder.DropColumn(
                name: "F2Date",
                table: "FollowUpStatus");

            migrationBuilder.DropColumn(
                name: "F2Evidences",
                table: "FollowUpStatus");

            migrationBuilder.DropColumn(
                name: "F2StatusOfActions",
                table: "FollowUpStatus");

            migrationBuilder.DropColumn(
                name: "F2VerifiedBy",
                table: "FollowUpStatus");

            migrationBuilder.RenameColumn(
                name: "F3VerifiedBy",
                table: "FollowUpStatus",
                newName: "VerifiedBy");

            migrationBuilder.RenameColumn(
                name: "F3StatusOfActions",
                table: "FollowUpStatus",
                newName: "StatusOfActions");

            migrationBuilder.RenameColumn(
                name: "F3Evidences",
                table: "FollowUpStatus",
                newName: "Evidences");

            migrationBuilder.RenameColumn(
                name: "F3Date",
                table: "FollowUpStatus",
                newName: "Date");
        }
    }
}
