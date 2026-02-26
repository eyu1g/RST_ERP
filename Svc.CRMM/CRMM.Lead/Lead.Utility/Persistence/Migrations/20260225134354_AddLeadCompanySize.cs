using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lead.Utility.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLeadCompanySize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanySize",
                table: "Leads",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanySize",
                table: "Leads");
        }
    }
}
