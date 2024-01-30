using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibleQuiz.Infrastructure.Data.Migrations
{
    public partial class _questionpassageprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TestQuestions",
                table: "TestQuestions");

            migrationBuilder.EnsureSchema(
                name: "com");

            migrationBuilder.RenameTable(
                name: "Verse",
                newName: "Verse",
                newSchema: "com");

            migrationBuilder.RenameTable(
                name: "BibleBook",
                newName: "BibleBook",
                newSchema: "com");

            migrationBuilder.RenameTable(
                name: "TestQuestions",
                newName: "TestQuestion",
                newSchema: "com");

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                schema: "com",
                table: "TestQuestion",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Passage",
                schema: "com",
                table: "TestQuestion",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestQuestion",
                schema: "com",
                table: "TestQuestion",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TestQuestion",
                schema: "com",
                table: "TestQuestion");

            migrationBuilder.DropColumn(
                name: "Passage",
                schema: "com",
                table: "TestQuestion");

            migrationBuilder.RenameTable(
                name: "Verse",
                schema: "com",
                newName: "Verse");

            migrationBuilder.RenameTable(
                name: "BibleBook",
                schema: "com",
                newName: "BibleBook");

            migrationBuilder.RenameTable(
                name: "TestQuestion",
                schema: "com",
                newName: "TestQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "Source",
                table: "TestQuestions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestQuestions",
                table: "TestQuestions",
                column: "Id");
        }
    }
}
