using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AppointmentDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppointmentDuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDuration", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DurationId",
                table: "Appointments",
                column: "DurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentDuration_DurationId",
                table: "Appointments",
                column: "DurationId",
                principalTable: "AppointmentDuration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentDuration_DurationId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "AppointmentDuration");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DurationId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DurationId",
                table: "Appointments");
        }
    }
}
