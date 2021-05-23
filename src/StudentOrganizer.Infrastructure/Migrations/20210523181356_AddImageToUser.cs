using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentOrganizer.Infrastructure.Migrations
{
    public partial class AddImageToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageHttpPath",
                table: "Users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageHttpPath",
                table: "Users");
        }
    }
}
