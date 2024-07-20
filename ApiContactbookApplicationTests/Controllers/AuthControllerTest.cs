using ApiContactbookApplication.Controllers;
using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Models;
using ApiContactbookApplication.Services.Contract;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiContactbookApplicationTests.Controllers
{
    public class AuthControllerTest
    {
        //register
        [Theory]
        [InlineData("User already exists.")]
        [InlineData("Something went wrong, please try after sometime.")]
        [InlineData("Mininum password length should be 8")]
        [InlineData("Password should be apphanumeric")]
        [InlineData("Password should contain special characters")]
        public void Register_ReturnsBadRequest_WhenRegistrationFails(string message)
        {
            // Arrange
            var registerDto = new RegisterDto();
            var mockAuthService = new Mock<IAuthService>();
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Success = false,
                Message = message

            };
            mockAuthService.Setup(service => service.RegisterUserService(registerDto))
                           .Returns(expectedServiceResponse);

            var target = new AuthController(mockAuthService.Object);

            // Act
            var actual = target.RegisterUser(registerDto) as ObjectResult;

            // Assert
            Assert.NotNull(actual);
            Assert.NotNull((ServiceResponse<string>)actual.Value);
            Assert.Equal(message, ((ServiceResponse<string>)actual.Value).Message);
            Assert.False(((ServiceResponse<string>)actual.Value).Success);
            Assert.Equal((int)HttpStatusCode.BadRequest, actual.StatusCode);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actual);
            Assert.IsType<ServiceResponse<string>>(badRequestResult.Value);
            Assert.False(((ServiceResponse<string>)badRequestResult.Value).Success);
            mockAuthService.Verify(service => service.RegisterUserService(registerDto), Times.Once);
        }
        [Fact]
        public void Register_ReturnsOk_WhenRegistrationSuccess()
        {
            // Arrange
            var registerDto = new RegisterDto();
            var mockAuthService = new Mock<IAuthService>();
            var message = "User Added Successfully";
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Success = true,
                Message = message

            };
            mockAuthService.Setup(service => service.RegisterUserService(registerDto))
                           .Returns(expectedServiceResponse);

            var target = new AuthController(mockAuthService.Object);

            // Act
            var actual = target.RegisterUser(registerDto) as OkObjectResult;

            // Assert
            Assert.NotNull(actual);
            Assert.NotNull((ServiceResponse<string>)actual.Value);
            Assert.Equal(message, ((ServiceResponse<string>)actual.Value).Message);
            Assert.True(((ServiceResponse<string>)actual.Value).Success);
            Assert.Equal((int)HttpStatusCode.OK, actual.StatusCode);
            var okResult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsType<ServiceResponse<string>>(okResult.Value);
            Assert.True(((ServiceResponse<string>)okResult.Value).Success);
            mockAuthService.Verify(service => service.RegisterUserService(registerDto), Times.Once);
        }

        //userdetailbyid
        [Fact]

        public void GetUserDetailById_ReturnsOk()
        {

            var userId = 1;
            var user = new User
            {

                UserId = userId,
                FirstName = "Contact 1"
            };

            var response = new ServiceResponse<UserDetailDto>
            {
                Success = true,
                Data = new UserDetailDto
                {
                    UserId = userId,
                    FirstName = user.FirstName
                }
            };

            var mockAuthService = new Mock<IAuthService>();
            var target = new AuthController(mockAuthService.Object);
            mockAuthService.Setup(c => c.GetUserDetailById(userId)).Returns(response);

            //Act
            var actual = target.GetUserDetailById(userId) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockAuthService.Verify(c => c.GetUserDetailById(userId), Times.Once);
        }

        [Fact]

        public void GetUserDetailById_ReturnsNotFound()
        {

            var userId = 1;
            var user = new User
            {

                UserId = userId,
                FirstName = "Contact 1"
            };

            var response = new ServiceResponse<UserDetailDto>
            {
                Success = false,
                Data = new UserDetailDto
                {
                    UserId = userId,
                    FirstName = user.FirstName
                }
            };

            var mockAuthService = new Mock<IAuthService>();
            var target = new AuthController(mockAuthService.Object);
            mockAuthService.Setup(c => c.GetUserDetailById(userId)).Returns(response);

            //Act
            var actual = target.GetUserDetailById(userId) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockAuthService.Verify(c => c.GetUserDetailById(userId), Times.Once);
        }


        //EditUserDetail
        [Fact]
        public void EditUserDetail_ReturnsOk_WhenContactIsUpdatesSuccessfully()
        {
            var updateUserDetailsDto = new UserDetailDto
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "test@example.com",
                ContactNumber = "123-456-7890",
                LoginId = "loginid",
                File = new byte[0],
                FileName = "abc.png"
            };
            var response = new ServiceResponse<string>
            {
                Success = true,
            };
            var mockAuthService = new Mock<IAuthService>();
            var target = new AuthController(mockAuthService.Object);
            mockAuthService.Setup(c => c.ModifyUser(It.IsAny<User>())).Returns(response);

            //Act

            var actual = target.Edit(updateUserDetailsDto) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockAuthService.Verify(c => c.ModifyUser(It.IsAny<User>()),Times.Once);

        }

        [Fact]
        public void EditUserDetail_ReturnsBadRequest_WhenContactIsNotUpdated()
        {
            var updateUserDetailsDto = new UserDetailDto
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "test@example.com",
                ContactNumber = "123-456-7890",
                LoginId = "loginid",
                File = new byte[0],
                FileName = "abc.png"
            };
            var response = new ServiceResponse<string>
            {
                Success = false,
            };
            var mockAuthService = new Mock<IAuthService>();
            var target = new AuthController(mockAuthService.Object);
            mockAuthService.Setup(c => c.ModifyUser(It.IsAny<User>())).Returns(response);

            //Act

            var actual = target.Edit(updateUserDetailsDto) as BadRequestObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(400, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockAuthService.Verify(c => c.ModifyUser(It.IsAny<User>()),Times.Once);


        }

        //login
        [Theory]
        [InlineData("Invalid username or password!")]
        [InlineData("Something went wrong, please try after sometime.")]
        public void Login_ReturnsBadRequest_WhenLoginFails(string message)
        {
            // Arrange
            var loginDto = new LoginDto();
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Success = false,
                Message = message

            };
            var mockAuthService = new Mock<IAuthService>();
            mockAuthService.Setup(service => service.LoginUserService(loginDto))
                           .Returns(expectedServiceResponse);

            var target = new AuthController(mockAuthService.Object);

            // Act
            var actual = target.LoginUser(loginDto) as ObjectResult;

            // Assert
            Assert.NotNull(actual);
            Assert.NotNull((ServiceResponse<string>)actual.Value);
            Assert.Equal(message, ((ServiceResponse<string>)actual.Value).Message);
            Assert.False(((ServiceResponse<string>)actual.Value).Success);
            Assert.Equal((int)HttpStatusCode.BadRequest, actual.StatusCode);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actual);
            Assert.IsType<ServiceResponse<string>>(badRequestResult.Value);
            Assert.False(((ServiceResponse<string>)badRequestResult.Value).Success);
            mockAuthService.Verify(service => service.LoginUserService(loginDto), Times.Once);
        }
        [Fact]
        public void Login_ReturnsOk_WhenLoginSucceeds()
        {
            // Arrange
            var loginDto = new LoginDto { Username = "username", Password = "password" };
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Success = true,
                Message = string.Empty

            };
            var mockAuthService = new Mock<IAuthService>();
            mockAuthService.Setup(service => service.LoginUserService(loginDto))
                           .Returns(expectedServiceResponse);

            var target = new AuthController(mockAuthService.Object);

            // Act
            var actual = target.LoginUser(loginDto) as ObjectResult;

            // Assert
            Assert.NotNull(actual);
            Assert.NotNull((ServiceResponse<string>)actual.Value);
            Assert.Equal(string.Empty, ((ServiceResponse<string>)actual.Value).Message);
            Assert.True(((ServiceResponse<string>)actual.Value).Success);
            var okResult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsType<ServiceResponse<string>>(okResult.Value);
            Assert.True(((ServiceResponse<string>)okResult.Value).Success);
            mockAuthService.Verify(service => service.LoginUserService(loginDto), Times.Once);
        }

        [Theory]
        [InlineData("Mininum password length should be 8")]
        [InlineData("Password should be apphanumeric")]
        [InlineData("Password should contain special characters")]
        [InlineData("Invalid username!")]
        [InlineData("Password and confirmation password do not match!")]
        [InlineData("Something went wrong, please try again later.")]
        public void PasswordService_ReturnsBadRequest_WhenPasswordServiceFails(string message)
        {
            // Arrange
            var forgetDto = new PasswordDto();
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Success = false,
                Message = message

            };
            var mockAuthService = new Mock<IAuthService>();
            mockAuthService.Setup(service => service.PasswordService(forgetDto))
                           .Returns(expectedServiceResponse);

            var target = new AuthController(mockAuthService.Object);

            // Act
            var actual = target.PasswordService(forgetDto) as ObjectResult;

            // Assert
            Assert.NotNull(actual);
            Assert.NotNull((ServiceResponse<string>)actual.Value);
            Assert.Equal(message, ((ServiceResponse<string>)actual.Value).Message);
            Assert.False(((ServiceResponse<string>)actual.Value).Success);
            Assert.Equal((int)HttpStatusCode.BadRequest, actual.StatusCode);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actual);
            Assert.IsType<ServiceResponse<string>>(badRequestResult.Value);
            Assert.False(((ServiceResponse<string>)badRequestResult.Value).Success);
            mockAuthService.Verify(service => service.PasswordService(forgetDto), Times.Once);
        }

        [Fact]
        public void PasswordService_ReturnsOk_WhenPasswordServiceSucceeds()
        {
            // Arrange
            var fixture = new Fixture();
            var forgetDto = new PasswordDto()
            {
                UserName = "username",
                Password = "Password@1234",
                ConfirmPassword = "Password@1234"
            };
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Success = true,
                Message = ""

            };
            var mockAuthService = new Mock<IAuthService>();
            mockAuthService.Setup(service => service.PasswordService(forgetDto))
                           .Returns(expectedServiceResponse);

            var target = new AuthController(mockAuthService.Object);

            // Act
            var actual = target.PasswordService(forgetDto) as OkObjectResult;

            // Assert
            Assert.NotNull(actual);
            Assert.NotNull((ServiceResponse<string>)actual.Value);
            Assert.Equal(string.Empty, ((ServiceResponse<string>)actual.Value).Message);
            Assert.True(((ServiceResponse<string>)actual.Value).Success);
            var okResult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsType<ServiceResponse<string>>(okResult.Value);
            Assert.True(((ServiceResponse<string>)okResult.Value).Success);
            mockAuthService.Verify(service => service.PasswordService(forgetDto), Times.Once);
        }
    }
}
