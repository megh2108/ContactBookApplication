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
    public class StateControllerTest
    {
        [Fact]
        public void GetAllStates_ReturnsOkWithStates_WhenStateExists()
        {
            //Arrange
            var states = new List<State>
             {
            new State{StateId=1,StateName="State 1", CountryId= 1},
            new State{StateId=2,StateName="State 2", CountryId= 2},
            };

            var response = new ServiceResponse<IEnumerable<StateDto>>
            {
                Success = true,
            };

            var mockStateService = new Mock<IStateService>();
            var target = new StateController(mockStateService.Object);
            mockStateService.Setup(c => c.GetAllStates()).Returns(response);

            //Act
            var actual = target.GetAllStates() as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockStateService.Verify(c => c.GetAllStates(), Times.Once);
        }

        [Fact]
        public void GetAllStates_ReturnsNotFound_WhenNoStateExists()
        {
            //Arrange
            var response = new ServiceResponse<IEnumerable<StateDto>>
            {
                Success = false,
                Data = new List<StateDto>(),

            };

            var mockStateService = new Mock<IStateService>();
            var target = new StateController(mockStateService.Object);
            mockStateService.Setup(c => c.GetAllStates()).Returns(response);

            //Act
            var actual = target.GetAllStates() as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockStateService.Verify(c => c.GetAllStates(), Times.Once);
        }
        [Fact]
        public void GetStateByCountryId_ReturnsOkWithStates_WhenStateExists()
        {
            //Arrange
            var countryId = 1;
            var response = new ServiceResponse<IEnumerable<StateDto>>
            {
                Success = true,
            };

            var mockStateService = new Mock<IStateService>();
            var target = new StateController(mockStateService.Object);
            mockStateService.Setup(c => c.GetStateByCountryId(countryId)).Returns(response);

            //Act
            var actual = target.GetStateByCountryId(countryId) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockStateService.Verify(c => c.GetStateByCountryId(countryId), Times.Once);
        }
        [Fact]
        public void GetStateByCountryId_ReturnsNotFound_WhenStateNotExists()
        {
            //Arrange
            var states = new State();
            var response = new ServiceResponse<IEnumerable<StateDto>>
            {
                Success = false,
            };

            var mockStateService = new Mock<IStateService>();
            var target = new StateController(mockStateService.Object);
            mockStateService.Setup(c => c.GetStateByCountryId(states.CountryId)).Returns(response);

            //Act
            var actual = target.GetStateByCountryId(states.CountryId) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockStateService.Verify(c => c.GetStateByCountryId(states.CountryId), Times.Once);
        }
    }
}
