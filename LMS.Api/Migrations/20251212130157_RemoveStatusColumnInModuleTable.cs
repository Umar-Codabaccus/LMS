using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStatusColumnInModuleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Module");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Module",
                type: "nvarchar(100)",
                nullable: true);
        }
    }
}
