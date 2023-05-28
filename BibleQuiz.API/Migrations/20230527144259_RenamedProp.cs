using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibleQuiz.API.Migrations
{
    public partial class RenamedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pasage",
                table: "VerseOfTheDay",
                newName: "Passage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Passage",
                table: "VerseOfTheDay",
                newName: "Pasage");
        }
    }
}
