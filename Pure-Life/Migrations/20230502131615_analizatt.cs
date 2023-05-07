using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pure_Life.Migrations
{
    public partial class analizatt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analizat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cmimi = table.Column<double>(type: "float", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analizat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Llojet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VleratReferente = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Llojet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalizatLlojet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnalizaId = table.Column<int>(type: "int", nullable: false),
                    LlojiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalizatLlojet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalizatLlojet_Analizat_AnalizaId",
                        column: x => x.AnalizaId,
                        principalTable: "Analizat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalizatLlojet_Llojet_LlojiId",
                        column: x => x.LlojiId,
                        principalTable: "Llojet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalizatLlojet_AnalizaId",
                table: "AnalizatLlojet",
                column: "AnalizaId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalizatLlojet_LlojiId",
                table: "AnalizatLlojet",
                column: "LlojiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalizatLlojet");

            migrationBuilder.DropTable(
                name: "Analizat");

            migrationBuilder.DropTable(
                name: "Llojet");
        }
    }
}
