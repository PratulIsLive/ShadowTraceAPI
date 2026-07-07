using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShadowTraceAPI.Migrations
{
    /// <inheritdoc />
    public partial class EnhanceEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Users_PerformedById",
                table: "ActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseAssignments_Users_InvestigatorId",
                table: "CaseAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Evidence_Users_UploadedById",
                table: "Evidence");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RiskScore",
                table: "Suspects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedAt",
                table: "InvestigationCases",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "InvestigationCases",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CrimeCategory",
                table: "InvestigationCases",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedClosureDate",
                table: "InvestigationCases",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsColdCase",
                table: "InvestigationCases",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CurrentHolder",
                table: "Evidence",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EvidenceNumber",
                table: "Evidence",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Evidence",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StorageLocation",
                table: "Evidence",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentAssignment",
                table: "CaseAssignments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EntityName",
                table: "ActivityLogs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Module",
                table: "ActivityLogs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suspects_PhoneNumber",
                table: "Suspects",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationCases_CaseNumber",
                table: "InvestigationCases",
                column: "CaseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationCases_CreatedById",
                table: "InvestigationCases",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Evidence_EvidenceNumber",
                table: "Evidence",
                column: "EvidenceNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_Users_PerformedById",
                table: "ActivityLogs",
                column: "PerformedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CaseAssignments_Users_InvestigatorId",
                table: "CaseAssignments",
                column: "InvestigatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evidence_Users_UploadedById",
                table: "Evidence",
                column: "UploadedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationCases_Users_CreatedById",
                table: "InvestigationCases",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Users_PerformedById",
                table: "ActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseAssignments_Users_InvestigatorId",
                table: "CaseAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Evidence_Users_UploadedById",
                table: "Evidence");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationCases_Users_CreatedById",
                table: "InvestigationCases");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Suspects_PhoneNumber",
                table: "Suspects");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationCases_CaseNumber",
                table: "InvestigationCases");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationCases_CreatedById",
                table: "InvestigationCases");

            migrationBuilder.DropIndex(
                name: "IX_Evidence_EvidenceNumber",
                table: "Evidence");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastLoginAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RiskScore",
                table: "Suspects");

            migrationBuilder.DropColumn(
                name: "AssignedAt",
                table: "InvestigationCases");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "InvestigationCases");

            migrationBuilder.DropColumn(
                name: "CrimeCategory",
                table: "InvestigationCases");

            migrationBuilder.DropColumn(
                name: "ExpectedClosureDate",
                table: "InvestigationCases");

            migrationBuilder.DropColumn(
                name: "IsColdCase",
                table: "InvestigationCases");

            migrationBuilder.DropColumn(
                name: "CurrentHolder",
                table: "Evidence");

            migrationBuilder.DropColumn(
                name: "EvidenceNumber",
                table: "Evidence");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Evidence");

            migrationBuilder.DropColumn(
                name: "StorageLocation",
                table: "Evidence");

            migrationBuilder.DropColumn(
                name: "IsCurrentAssignment",
                table: "CaseAssignments");

            migrationBuilder.DropColumn(
                name: "EntityName",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "Module",
                table: "ActivityLogs");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_Users_PerformedById",
                table: "ActivityLogs",
                column: "PerformedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CaseAssignments_Users_InvestigatorId",
                table: "CaseAssignments",
                column: "InvestigatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evidence_Users_UploadedById",
                table: "Evidence",
                column: "UploadedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
