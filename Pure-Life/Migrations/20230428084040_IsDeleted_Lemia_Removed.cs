using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pure_Life.Migrations
{
    public partial class IsDeleted_Lemia_Removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Lemia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Lemia",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
