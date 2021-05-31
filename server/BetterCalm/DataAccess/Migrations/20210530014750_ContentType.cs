using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ContentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AudioUrl",
                table: "Contents",
                newName: "ContentUrl");

            migrationBuilder.AddColumn<int>(
                name: "ContentTypeId",
                table: "Contents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ContentTypeId",
                table: "Contents",
                column: "ContentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_ContentTypes_ContentTypeId",
                table: "Contents",
                column: "ContentTypeId",
                principalTable: "ContentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_ContentTypes_ContentTypeId",
                table: "Contents");

            migrationBuilder.DropTable(
                name: "ContentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Contents_ContentTypeId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "ContentTypeId",
                table: "Contents");

            migrationBuilder.RenameColumn(
                name: "ContentUrl",
                table: "Contents",
                newName: "AudioUrl");
        }
    }
}
