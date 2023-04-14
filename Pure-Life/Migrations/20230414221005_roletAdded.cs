using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pure_Life.Migrations
{
    public partial class roletAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoletId",
                table: "Stafi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoliId",
                table: "Stafi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rolet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rolet", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stafi_RoletId",
                table: "Stafi",
                column: "RoletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stafi_Rolet_RoletId",
                table: "Stafi",
                column: "RoletId",
                principalTable: "Rolet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stafi_Rolet_RoletId",
                table: "Stafi");

            migrationBuilder.DropTable(
                name: "Rolet");

            migrationBuilder.DropIndex(
                name: "IX_Stafi_RoletId",
                table: "Stafi");

            migrationBuilder.DropColumn(
                name: "RoletId",
                table: "Stafi");

            migrationBuilder.DropColumn(
                name: "RoliId",
                table: "Stafi");
        }
    }
}
