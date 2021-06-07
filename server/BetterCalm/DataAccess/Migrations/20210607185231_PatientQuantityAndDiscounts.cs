using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class PatientQuantityAndDiscounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EMail",
                table: "Patients",
                newName: "Email");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentDiscountId",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppointmentQuantity",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalCost",
                table: "Appointments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "AppointmentDiscount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDiscount", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AppointmentDiscountId",
                table: "Patients",
                column: "AppointmentDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DiscountId",
                table: "Appointments",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentDiscount_DiscountId",
                table: "Appointments",
                column: "DiscountId",
                principalTable: "AppointmentDiscount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AppointmentDiscount_AppointmentDiscountId",
                table: "Patients",
                column: "AppointmentDiscountId",
                principalTable: "AppointmentDiscount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentDiscount_DiscountId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AppointmentDiscount_AppointmentDiscountId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "AppointmentDiscount");

            migrationBuilder.DropIndex(
                name: "IX_Patients_AppointmentDiscountId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DiscountId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppointmentDiscountId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "AppointmentQuantity",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Patients",
                newName: "EMail");
        }
    }
}
