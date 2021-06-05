using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class PsychologistRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RateId",
                table: "Psychologists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PsychologistRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HourlyRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychologistRate", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Psychologists_RateId",
                table: "Psychologists",
                column: "RateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Psychologists_PsychologistRate_RateId",
                table: "Psychologists",
                column: "RateId",
                principalTable: "PsychologistRate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Psychologists_PsychologistRate_RateId",
                table: "Psychologists");

            migrationBuilder.DropTable(
                name: "PsychologistRate");

            migrationBuilder.DropIndex(
                name: "IX_Psychologists_RateId",
                table: "Psychologists");

            migrationBuilder.DropColumn(
                name: "RateId",
                table: "Psychologists");
        }
    }
}
