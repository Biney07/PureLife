using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pure_Life.Migrations
{
    public partial class Kujdestarite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kujdestarite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kati = table.Column<int>(type: "int", nullable: false),
                    Reparti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertedFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StafiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kujdestarite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kujdestarite_Stafi_StafiId",
                        column: x => x.StafiId,
                        principalTable: "Stafi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kujdestarite_StafiId",
                table: "Kujdestarite",
                column: "StafiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kujdestarite");
        }
    }
}
