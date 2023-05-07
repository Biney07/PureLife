using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pure_Life.Migrations
{
    public partial class lemiaadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModifiedFrom",
                table: "Sherbimet",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Sherbimet",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "LemiaId",
                table: "Sherbimet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sherbimet_LemiaId",
                table: "Sherbimet",
                column: "LemiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sherbimet_Lemia_LemiaId",
                table: "Sherbimet",
                column: "LemiaId",
                principalTable: "Lemia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sherbimet_Lemia_LemiaId",
                table: "Sherbimet");

            migrationBuilder.DropIndex(
                name: "IX_Sherbimet_LemiaId",
                table: "Sherbimet");

            migrationBuilder.DropColumn(
                name: "LemiaId",
                table: "Sherbimet");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedFrom",
                table: "Sherbimet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Sherbimet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
