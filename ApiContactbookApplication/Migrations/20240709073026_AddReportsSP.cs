using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiContactbookApplication.Migrations
{
    public partial class AddReportsSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
               CREATE OR ALTER PROCEDURE GetAllContactsByBirthdayMonth
	                @Month INT
	
                AS
                BEGIN
			              SELECT c.ContactId,c.Name, c.Email,  c.PhoneNumber ,c.Company ,c.FileName, c.[File],c.BirthDate , c.Gender, c.Favourite ,co.CountryName,s.StateName
				                FROM Contactbooks c JOIN Countries co ON c.CountryId = co.CountryId	
					                JOIN States s ON s.StateId = c.StateId
						                WHERE Month(c.BirthDate) = @Month
	
                END
            ");

            migrationBuilder.Sql(@"
                  CREATE OR ALTER PROCEDURE GetAllContactsByStates
	                @StateId INT
	
                AS
                BEGIN
			                SELECT c.ContactId,c.Name, c.Email,  c.PhoneNumber ,c.Company ,c.FileName, c.[File],c.BirthDate , c.Gender, c.Favourite 
				                FROM Contactbooks c 
						                WHERE c.StateId = @StateId
	
                END
            ");

            migrationBuilder.Sql(@"
                  CREATE OR ALTER PROCEDURE GetAllContactsCountByCountry
	                    @CountryId INT
	
                    AS
                    BEGIN
                        SELECT Count(c.ContactId) FROM Contactbooks c WHERE c.CountryId = @CountryId
	
                    END
            ");

            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE GetAllContactsCountByGender
	                @Gender VarChar(5)
	
                AS
                BEGIN
                 SELECT Count(c.ContactId) FROM Contactbooks c WHERE c.Gender = @Gender
	
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllContactsByBirthdayMonth;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllContactsByStates;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllContactsCountByCountry;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllContactsCountByGender;");
        }
    }
}
