using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibleQuiz.API.Data.Migrations
{
    public partial class AddedThousandQuizQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ThousandQuestions",
                table: "ThousandQuestions");

            migrationBuilder.RenameTable(
                name: "ThousandQuestions",
                newName: "ThousandQuizQuestion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThousandQuizQuestion",
                table: "ThousandQuizQuestion",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ThousandQuizQuestion",
                table: "ThousandQuizQuestion");

            migrationBuilder.RenameTable(
                name: "ThousandQuizQuestion",
                newName: "ThousandQuestions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThousandQuestions",
                table: "ThousandQuestions",
                column: "Id");
        }
    }
}
