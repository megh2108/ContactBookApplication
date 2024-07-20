using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiContactbookApplication.Migrations
{
    public partial class AlterUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Users");
        }
    }
}
