using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiContactbookApplication.Migrations
{
    public partial class AlterGetAllContactsSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
           CREATE OR ALTER PROCEDURE GetAllContactsByPagination
				@Search VARCHAR(50) = NULL,
				@Page INT,
				@PageSize INT,
				@SortOrder VARCHAR(10) = 'asc'

			AS
			BEGIN
			 DECLARE @Skip INT = (@Page - 1) * @PageSize;

				 IF @SortOrder = 'asc'
					BEGIN
						SELECT c.ContactId,c.Name, c.Email,  c.PhoneNumber ,c.Company ,c.FileName,c.[File], c.BirthDate , c.Gender, c.Favourite , co.CountryName , s.StateName FROM Contactbooks c
							JOIN Countries co ON c.CountryId = co.CountryId
								JOIN States s ON c.StateId = s.StateId
									WHERE @Search IS NULL OR @Search IS NOT NULL AND c.Name LIKE '%'+@Search+'%'
										Order By c.Name
											OFFSET @Skip ROWS
												FETCH NEXT @PageSize ROWS ONLY;
					END
					ELSE
					BEGIN
						SELECT c.ContactId,c.Name, c.Email,  c.PhoneNumber ,c.Company ,c.FileName,c.[File], c.BirthDate , c.Gender, c.Favourite , co.CountryName , s.StateName FROM Contactbooks c
							JOIN Countries co ON c.CountryId = co.CountryId
								JOIN States s ON c.StateId = s.StateId
									WHERE @Search IS NULL OR @Search IS NOT NULL AND c.Name LIKE '%'+@Search+'%'
										Order By c.Name desc
											OFFSET @Skip ROWS
												FETCH NEXT @PageSize ROWS ONLY;
					END
							
	
			END


        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllContactsByPagination;");
        }
    }
}
