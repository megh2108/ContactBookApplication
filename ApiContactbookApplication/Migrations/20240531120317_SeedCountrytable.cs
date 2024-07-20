using Microsoft.EntityFrameworkCore.Migrations;
using System.Runtime.InteropServices;

#nullable disable

namespace ApiContactbookApplication.Migrations
{
    public partial class SeedCountrytable : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)

        {

            migrationBuilder.InsertData(

            table: "Countries",

            columns: new[] { "CountryId", "CountryName" },

            values: new object[,]

            {

              {1,"India" },

              {2,"Australia" },

              {3,"UK" },

              {4,"USA" },

              {5,"Canada" },

            });

        }

        protected override void Down(MigrationBuilder migrationBuilder)

        {

            migrationBuilder.DeleteData(

            table: "Countries",

            keyColumn: "CountryId",

            keyValue: new object[] { 1, 2, 3, 4, 5 }

        );

        }
    }
}
