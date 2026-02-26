using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lead.Utility.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLeadUiFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Budget",
                table: "Leads",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Industry",
                table: "Leads",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Leads",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Timeline",
                table: "Leads",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Industry",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Timeline",
                table: "Leads");
        }
    }
}
