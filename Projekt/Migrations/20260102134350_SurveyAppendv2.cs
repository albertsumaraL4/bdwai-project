using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class SurveyAppendv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyItems");

            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "QuestionItems",
                newName: "Content");

            migrationBuilder.AddColumn<int>(
                name: "SurveyId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "QuestionItems",
                newName: "Answer");

            migrationBuilder.CreateTable(
                name: "SurveyItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyItems_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyItems_SurveyId",
                table: "SurveyItems",
                column: "SurveyId");
        }
    }
}
