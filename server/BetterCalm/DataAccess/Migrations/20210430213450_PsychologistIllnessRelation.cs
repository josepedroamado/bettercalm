using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class PsychologistIllnessRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Illnesses_Psychologists_PsychologistId",
                table: "Illnesses");

            migrationBuilder.DropIndex(
                name: "IX_Illnesses_PsychologistId",
                table: "Illnesses");

            migrationBuilder.DropColumn(
                name: "PsychologistId",
                table: "Illnesses");

            migrationBuilder.CreateTable(
                name: "IllnessPsychologist",
                columns: table => new
                {
                    IllnessesId = table.Column<int>(type: "int", nullable: false),
                    PsychologistsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IllnessPsychologist", x => new { x.IllnessesId, x.PsychologistsId });
                    table.ForeignKey(
                        name: "FK_IllnessPsychologist_Illnesses_IllnessesId",
                        column: x => x.IllnessesId,
                        principalTable: "Illnesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IllnessPsychologist_Psychologists_PsychologistsId",
                        column: x => x.PsychologistsId,
                        principalTable: "Psychologists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IllnessPsychologist_PsychologistsId",
                table: "IllnessPsychologist",
                column: "PsychologistsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IllnessPsychologist");

            migrationBuilder.AddColumn<int>(
                name: "PsychologistId",
                table: "Illnesses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Illnesses_PsychologistId",
                table: "Illnesses",
                column: "PsychologistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Illnesses_Psychologists_PsychologistId",
                table: "Illnesses",
                column: "PsychologistId",
                principalTable: "Psychologists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
