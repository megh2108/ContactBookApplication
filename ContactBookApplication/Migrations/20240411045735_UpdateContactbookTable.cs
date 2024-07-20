using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactBookApplication.Migrations
{
    public partial class UpdateContactbookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Contacts");
        }
    }
}
