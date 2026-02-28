using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lead.Utility.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLeadAdminMasters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "LeadStatuses",
                newName: "Priority");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "LeadSources",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "LeadSources",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AssignToName",
                table: "LeadAssignmentRules",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssignToUserId",
                table: "LeadAssignmentRules",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "LeadAssignmentRules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LeadIndustries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateMod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadIndustries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadRoutingRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AssignToUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    AssignToName = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateMod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadRoutingRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadScoringRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MaxPoints = table.Column<int>(type: "integer", nullable: false),
                    WeightPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateMod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadScoringRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadRoutingRuleConditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LeadRoutingRuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    Field = table.Column<string>(type: "text", nullable: false),
                    Operator = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateMod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadRoutingRuleConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadRoutingRuleConditions_LeadRoutingRules_LeadRoutingRuleId",
                        column: x => x.LeadRoutingRuleId,
                        principalTable: "LeadRoutingRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadRoutingRuleConditions_LeadRoutingRuleId",
                table: "LeadRoutingRuleConditions",
                column: "LeadRoutingRuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadIndustries");

            migrationBuilder.DropTable(
                name: "LeadRoutingRuleConditions");

            migrationBuilder.DropTable(
                name: "LeadScoringRules");

            migrationBuilder.DropTable(
                name: "LeadRoutingRules");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "LeadSources");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "LeadSources");

            migrationBuilder.DropColumn(
                name: "AssignToName",
                table: "LeadAssignmentRules");

            migrationBuilder.DropColumn(
                name: "AssignToUserId",
                table: "LeadAssignmentRules");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "LeadAssignmentRules");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "LeadStatuses",
                newName: "SortOrder");
        }
    }
}
