using ApiContactbookApplication.Controllers;
using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Models;
using ApiContactbookApplication.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiContactbookApplicationTests.Controllers
{
    public class CountryControllerTest
    {
        [Fact]
        public void GetAllCountries_ReturnsOkWithCountries_WhenCountryExists()
        {
            //Arrange
            var countries = new List<Country>
             {
            new Country{CountryId=1,CountryName="Country 1"},
            new Country{CountryId=2,CountryName="Country 2"},
            };

            var response = new ServiceResponse<IEnumerable<CountryDto>>
            {
                Success = true,
            };

            var mockCountryService = new Mock<ICountryService>();
            var target = new CountryController(mockCountryService.Object);
            mockCountryService.Setup(c => c.GetAllCountries()).Returns(response);

            //Act
            var actual = target.GetAllCountries() as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockCountryService.Verify(c => c.GetAllCountries(), Times.Once);
        }

        [Fact]
        public void GetAllCountries_ReturnsNotFound_WhenNoCountryExists()
        {
            //Arrange
            var response = new ServiceResponse<IEnumerable<CountryDto>>
            {
                Success = false,
                Data = new List<CountryDto>()

            };

            var mockCountryService = new Mock<ICountryService>();
            var target = new CountryController(mockCountryService.Object);
            mockCountryService.Setup(c => c.GetAllCountries()).Returns(response);

            //Act
            var actual = target.GetAllCountries() as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            mockCountryService.Verify(c => c.GetAllCountries(), Times.Once);
        }
    }
}
