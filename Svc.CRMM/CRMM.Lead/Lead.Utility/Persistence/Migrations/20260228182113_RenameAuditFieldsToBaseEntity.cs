using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lead.Utility.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameAuditFieldsToBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Leads",
                newName: "DateAdd");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Leads",
                newName: "DateMod");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "LeadGroups",
                newName: "DateAdd");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "LeadGroups",
                newName: "DateMod");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "LeadGroupConditions",
                newName: "DateAdd");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "LeadGroupConditions",
                newName: "DateMod");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdd",
                table: "LeadStatuses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateMod",
                table: "LeadStatuses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LeadStatuses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "LeadStatuses",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdd",
                table: "LeadStages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateMod",
                table: "LeadStages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LeadStages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "LeadStages",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdd",
                table: "LeadSources",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateMod",
                table: "LeadSources",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LeadSources",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "LeadSources",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdd",
                table: "LeadScoreHistories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateMod",
                table: "LeadScoreHistories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LeadScoreHistories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "LeadScoreHistories",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<string>(
                name: "Timeline",
                table: "Leads",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Leads",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LeadNo",
                table: "Leads",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Leads",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "Leads",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Industry",
                table: "Leads",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Leads",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Leads",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanySize",
                table: "Leads",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "Leads",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Leads",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Leads",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdd",
                table: "LeadMergeLogs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateMod",
                table: "LeadMergeLogs",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LeadMergeLogs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "LeadMergeLogs",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LeadGroups",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "LeadGroups",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LeadGroupConditions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "LeadGroupConditions",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdd",
                table: "LeadDuplicateCandidates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateMod",
                table: "LeadDuplicateCandidates",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LeadDuplicateCandidates",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "LeadDuplicateCandidates",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdd",
                table: "LeadConversionLogs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateMod",
                table: "LeadConversionLogs",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LeadConversionLogs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "LeadConversionLogs",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdd",
                table: "LeadAssignmentRules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateMod",
                table: "LeadAssignmentRules",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LeadAssignmentRules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "LeadAssignmentRules",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdd",
                table: "LeadActivities",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateMod",
                table: "LeadActivities",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LeadActivities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "LeadActivities",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAdd",
                table: "LeadStatuses");

            migrationBuilder.DropColumn(
                name: "DateMod",
                table: "LeadStatuses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LeadStatuses");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "LeadStatuses");

            migrationBuilder.DropColumn(
                name: "DateAdd",
                table: "LeadStages");

            migrationBuilder.DropColumn(
                name: "DateMod",
                table: "LeadStages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LeadStages");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "LeadStages");

            migrationBuilder.DropColumn(
                name: "DateAdd",
                table: "LeadSources");

            migrationBuilder.DropColumn(
                name: "DateMod",
                table: "LeadSources");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LeadSources");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "LeadSources");

            migrationBuilder.DropColumn(
                name: "DateAdd",
                table: "LeadScoreHistories");

            migrationBuilder.DropColumn(
                name: "DateMod",
                table: "LeadScoreHistories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LeadScoreHistories");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "LeadScoreHistories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "DateAdd",
                table: "LeadMergeLogs");

            migrationBuilder.DropColumn(
                name: "DateMod",
                table: "LeadMergeLogs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LeadMergeLogs");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "LeadMergeLogs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LeadGroups");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "LeadGroups");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LeadGroupConditions");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "LeadGroupConditions");

            migrationBuilder.DropColumn(
                name: "DateAdd",
                table: "LeadDuplicateCandidates");

            migrationBuilder.DropColumn(
                name: "DateMod",
                table: "LeadDuplicateCandidates");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LeadDuplicateCandidates");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "LeadDuplicateCandidates");

            migrationBuilder.DropColumn(
                name: "DateAdd",
                table: "LeadConversionLogs");

            migrationBuilder.DropColumn(
                name: "DateMod",
                table: "LeadConversionLogs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LeadConversionLogs");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "LeadConversionLogs");

            migrationBuilder.DropColumn(
                name: "DateAdd",
                table: "LeadAssignmentRules");

            migrationBuilder.DropColumn(
                name: "DateMod",
                table: "LeadAssignmentRules");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LeadAssignmentRules");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "LeadAssignmentRules");

            migrationBuilder.DropColumn(
                name: "DateAdd",
                table: "LeadActivities");

            migrationBuilder.DropColumn(
                name: "DateMod",
                table: "LeadActivities");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LeadActivities");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "LeadActivities");

            migrationBuilder.RenameColumn(
                name: "DateAdd",
                table: "Leads",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "DateMod",
                table: "Leads",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DateAdd",
                table: "LeadGroups",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "DateMod",
                table: "LeadGroups",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DateAdd",
                table: "LeadGroupConditions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "DateMod",
                table: "LeadGroupConditions",
                newName: "UpdatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Timeline",
                table: "Leads",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Leads",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LeadNo",
                table: "Leads",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Leads",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "Leads",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Industry",
                table: "Leads",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Leads",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Leads",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CompanySize",
                table: "Leads",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "Leads",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

        }
    }
}
