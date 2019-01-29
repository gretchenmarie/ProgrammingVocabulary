using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgrammingVocabulary.Migrations
{
    public partial class Quizvocabulary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "UserVocabulary",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    QuizId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Word = table.Column<string>(nullable: false),
                    Definition = table.Column<string>(nullable: false),
                    Link = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.QuizId);
                    table.ForeignKey(
                        name: "FK_Quiz_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quiz_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVocabulary_QuizId",
                table: "UserVocabulary",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_LanguageId",
                table: "Quiz",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_UserId",
                table: "Quiz",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVocabulary_Quiz_QuizId",
                table: "UserVocabulary",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVocabulary_Quiz_QuizId",
                table: "UserVocabulary");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropIndex(
                name: "IX_UserVocabulary_QuizId",
                table: "UserVocabulary");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "UserVocabulary");
        }
    }
}
