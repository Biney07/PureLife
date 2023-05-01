using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pure_Life.Migrations
{
    public partial class LemiaNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stafi_Lemia_LemiaId",
                table: "Stafi");

            migrationBuilder.AlterColumn<int>(
                name: "LemiaId",
                table: "Stafi",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Stafi_Lemia_LemiaId",
                table: "Stafi",
                column: "LemiaId",
                principalTable: "Lemia",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stafi_Lemia_LemiaId",
                table: "Stafi");

            migrationBuilder.AlterColumn<int>(
                name: "LemiaId",
                table: "Stafi",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stafi_Lemia_LemiaId",
                table: "Stafi",
                column: "LemiaId",
                principalTable: "Lemia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
