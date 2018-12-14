using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgrammingVocabulary.Migrations
{
    public partial class UserVocabulary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendship");

            migrationBuilder.CreateTable(
                name: "UserVocabulary",
                columns: table => new
                {
                    UserVocabularyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    VocabularyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVocabulary", x => x.UserVocabularyId);
                    table.ForeignKey(
                        name: "FK_UserVocabulary_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVocabulary_Vocabulary_VocabularyId",
                        column: x => x.VocabularyId,
                        principalTable: "Vocabulary",
                        principalColumn: "VocabularyId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVocabulary_UserId",
                table: "UserVocabulary",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVocabulary_VocabularyId",
                table: "UserVocabulary",
                column: "VocabularyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVocabulary");

            migrationBuilder.CreateTable(
                name: "Friendship",
                columns: table => new
                {
                    FriendshipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Friend1Id = table.Column<string>(nullable: false),
                    Friend2Id = table.Column<string>(nullable: false),
                    User1Id = table.Column<string>(nullable: false),
                    User2Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendship", x => x.FriendshipId);
                    table.ForeignKey(
                        name: "FK_Friendship_AspNetUsers_Friend1Id",
                        column: x => x.Friend1Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Friendship_AspNetUsers_Friend2Id",
                        column: x => x.Friend2Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_Friend1Id",
                table: "Friendship",
                column: "Friend1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_Friend2Id",
                table: "Friendship",
                column: "Friend2Id");
        }
    }
}
