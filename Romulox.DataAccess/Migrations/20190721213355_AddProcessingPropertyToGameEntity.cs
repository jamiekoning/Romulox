using Microsoft.EntityFrameworkCore.Migrations;

namespace Romulox.DataAccess.Migrations
{
    public partial class AddProcessingPropertyToGameEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Processed",
                table: "Games",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Processed",
                table: "Games");
        }
    }
}
