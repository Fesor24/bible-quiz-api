using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibleQuiz.API.Data.Migrations
{
    public partial class ModifiedPermissionToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPermission",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Permission",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permission",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "HasPermission",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
