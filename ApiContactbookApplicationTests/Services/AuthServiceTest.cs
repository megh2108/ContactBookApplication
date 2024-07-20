using ApiContactbookApplication.Data.Contract;
using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Models;
using ApiContactbookApplication.Services.Contract;
using ApiContactbookApplication.Services.Implementation;
using AutoFixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiContactbookApplicationTests.Services
{
    public class AuthServiceTest
    {
        [Fact]
        public void RegisterUserService_ReturnsSuccess_WhenValidRegistration()
        {
            // Arrange
            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockVerifyPasswordHash = new Mock<IVerifyPasswordHashService>();
            mockAuthRepository.Setup(repo => repo.UserExists(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            mockAuthRepository.Setup(repo => repo.RegisterUser(It.IsAny<User>())).Returns(true);


            var target = new AuthService(mockAuthRepository.Object, mockVerifyPasswordHash.Object);

            var registerDto = new RegisterDto
            {
                FirstName = "firstname",
                LastName = "lastname",
                Email = "email@example.com",
                LoginId = "loginid",
                ContactNumber = "1234567890",
                Password = "Password@123"
            };

            // Act
            var result = target.RegisterUserService(registerDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(string.Empty, result.Message);
            mockAuthRepository.Verify(c => c.UserExists(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mockAuthRepository.Verify(c => c.RegisterUser(It.IsAny<User>()), Times.Once);
        }
        [Fact]
        public void RegisterUserService_ReturnsFailure_WhenPasswordIsWeak()
        {
            // Arrange
            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockVerifyPasswordHash = new Mock<IVerifyPasswordHashService>();
            var target = new AuthService(mockAuthRepository.Object, mockVerifyPasswordHash.Object);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Mininum password length should be 8" + Environment.NewLine);
            stringBuilder.Append("Password should be alphanumeric" + Environment.NewLine);
            stringBuilder.Append("Password should contain special characters" + Environment.NewLine);
            var registerDto = new RegisterDto
            {
                FirstName = "firstname",
                LastName = "lastname",
                Email = "email@example.com",
                LoginId = "loginid",
                ContactNumber = "1234567890",
                Password = "pass"
            };

            // Act
            var result = target.RegisterUserService(registerDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(stringBuilder.ToString(), result.Message);
        }
        [Fact]
        public void RegisterUserService_ReturnsUserExistss()
        {
            // Arrange
            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockVerifyPasswordHash = new Mock<IVerifyPasswordHashService>();
            mockAuthRepository.Setup(repo => repo.UserExists(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var target = new AuthService(mockAuthRepository.Object, mockVerifyPasswordHash.Object);

            var registerDto = new RegisterDto
            {
                FirstName = "firstname",
                LastName = "lastname",
                Email = "email@example.com",
                LoginId = "loginid",
                ContactNumber = "1234567890",
                Password = "Password@123"
            };

            // Act
            var result = target.RegisterUserService(registerDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("User already exists", result.Message);
            mockAuthRepository.Verify(c => c.UserExists(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
        [Fact]
        public void RegisterUserService_ReturnsSomeThingWentWrong_WhenInValidRegistration()
        {
            // Arrange
            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockVerifyPasswordHash = new Mock<IVerifyPasswordHashService>();
            mockAuthRepository.Setup(repo => repo.UserExists(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            mockAuthRepository.Setup(repo => repo.RegisterUser(It.IsAny<User>())).Returns(false);


            var target = new AuthService(mockAuthRepository.Object, mockVerifyPasswordHash.Object);

            var registerDto = new RegisterDto
            {
                FirstName = "firstname",
                LastName = "lastname",
                Email = "email@example.com",
                LoginId = "loginid",
                ContactNumber = "1234567890",
                Password = "Password@123"
            };

            // Act
            var result = target.RegisterUserService(registerDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Something went wrong. Please try after sometime.", result.Message);
            mockAuthRepository.Verify(c => c.UserExists(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mockAuthRepository.Verify(c => c.RegisterUser(It.IsAny<User>()), Times.Once);
        }
        [Fact]
        public void LoginUserService_ReturnsSomethingWentWrong_WhenLoginDtoIsNull()
        {
            //Arrange
            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockConfiguration = new Mock<IVerifyPasswordHashService>();

            var target = new AuthService(mockAuthRepository.Object, mockConfiguration.Object);


            // Act
            var result = target.LoginUserService(null);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Something went wrong.Please try after sometime.", result.Message);

        }
        [Fact]
        public void LoginUserService_ReturnsInvalidUsernameOrPassword_WhenUserIsNull()
        {
            //Arrange
            var loginDto = new LoginDto
            {
                Username = "username"
            };
            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockConfiguration = new Mock<IVerifyPasswordHashService>();
            mockAuthRepository.Setup(repo => repo.ValidateUser(loginDto.Username)).Returns<User>(null);

            var target = new AuthService(mockAuthRepository.Object, mockConfiguration.Object);


            // Act
            var result = target.LoginUserService(loginDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Invalid username or password", result.Message);
            mockAuthRepository.Verify(repo => repo.ValidateUser(loginDto.Username), Times.Once);


        }
        [Fact]
        public void LoginUserService_ReturnsInvalidUsernameOrPassword_WhenPasswordIsWrong()
        {
            //Arrange
            var loginDto = new LoginDto
            {
                Username = "username",
                Password = "password"
            };
            var fixture = new Fixture();
            var user = fixture.Create<User>();
            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockConfiguration = new Mock<IVerifyPasswordHashService>();
            mockAuthRepository.Setup(repo => repo.ValidateUser(loginDto.Username)).Returns(user);
            mockConfiguration.Setup(repo => repo.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt)).Returns(false);

            var target = new AuthService(mockAuthRepository.Object, mockConfiguration.Object);


            // Act
            var result = target.LoginUserService(loginDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Invalid username or password", result.Message);
            mockAuthRepository.Verify(repo => repo.ValidateUser(loginDto.Username), Times.Once);
            mockConfiguration.Verify(repo => repo.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt), Times.Once);


        }

        [Fact]
        public void LoginUserService_ReturnsResponse_WhenLoginIsSuccessful()
        {
            //Arrange
            var loginDto = new LoginDto
            {
                Username = "username",
                Password = "password"
            };
            var fixture = new Fixture();
            var user = fixture.Create<User>();
            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockConfiguration = new Mock<IVerifyPasswordHashService>();
            mockAuthRepository.Setup(repo => repo.ValidateUser(loginDto.Username)).Returns(user);
            mockConfiguration.Setup(repo => repo.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt)).Returns(true);
            mockConfiguration.Setup(repo => repo.CreateToken(user)).Returns("");

            var target = new AuthService(mockAuthRepository.Object, mockConfiguration.Object);


            // Act
            var result = target.LoginUserService(loginDto);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            mockAuthRepository.Verify(repo => repo.ValidateUser(loginDto.Username), Times.Once);
            mockConfiguration.Verify(repo => repo.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt), Times.Once);
            mockConfiguration.Verify(repo => repo.CreateToken(user), Times.Once);


        }

        [Fact]
        public void PasswordService_ReturnsSomethingWentWrong_WhenForgetDtoIsNull()
        {
            //Arrange
            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockConfiguration = new Mock<IVerifyPasswordHashService>();

            var target = new AuthService(mockAuthRepository.Object, mockConfiguration.Object);


            // Act
            var result = target.PasswordService(null);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("Something went wrong, please try again later.", result.Message);

        }

        [Fact]
        public void PasswordService_ReturnsInvalidUsername_WhenUserIsNull()
        {
            //Arrange
            var forgetDto = new PasswordDto
            {
                UserName = "username"
            };
            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockConfiguration = new Mock<IVerifyPasswordHashService>();
            mockAuthRepository.Setup(repo => repo.ValidateUser(forgetDto.UserName)).Returns<User>(null);

            var target = new AuthService(mockAuthRepository.Object, mockConfiguration.Object);


            // Act
            var result = target.PasswordService(forgetDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("Invalid username!", result.Message);
            mockAuthRepository.Verify(repo => repo.ValidateUser(forgetDto.UserName), Times.Once);

        }

        [Fact]
        public void PasswordService_ReturnsFailure_WhenPasswordIsWeak()
        {
            // Arrange
            var fixture = new Fixture();
            var user = fixture.Create<User>();
            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockConfiguration = new Mock<IVerifyPasswordHashService>();
            var target = new AuthService(mockAuthRepository.Object, mockConfiguration.Object);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Minimum password length should be 8" + Environment.NewLine);
            stringBuilder.Append("Password should be alphanumeric" + Environment.NewLine);
            stringBuilder.Append("Password should contain special characters" + Environment.NewLine);
            var forgetDto = new PasswordDto
            {
                UserName = "username",
                Password = "pass",
                ConfirmPassword = "pass"
            };
            mockAuthRepository.Setup(repo => repo.ValidateUser(forgetDto.UserName)).Returns(user);


            // Act
            var result = target.PasswordService(forgetDto);

            // Assert
            Assert.False(result.Success);
            // Assert.Equal(stringBuilder.ToString(), result.Message);
            mockAuthRepository.Verify(repo => repo.ValidateUser(forgetDto.UserName), Times.Once);

        }
        [Fact]
        public void PasswordService_ReturnsFailure_WhenPasswordsDontMatch()
        {
            // Arrange
            var fixture = new Fixture();
            var user = fixture.Create<User>();
            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockConfiguration = new Mock<IVerifyPasswordHashService>();
            var target = new AuthService(mockAuthRepository.Object, mockConfiguration.Object);
            var forgetDto = new PasswordDto
            {
                UserName = "username",
                Password = "Password@1234",
                ConfirmPassword = "Password234"
            };
            mockAuthRepository.Setup(repo => repo.ValidateUser(forgetDto.UserName)).Returns(user);


            // Act
            var result = target.PasswordService(forgetDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("Password and confirmation password do not match!", result.Message);
            mockAuthRepository.Verify(repo => repo.ValidateUser(forgetDto.UserName), Times.Once);

        }
        [Fact]
        public void PasswordService_ReturnsSuccess_WhenPasswordResetSuccessfully()
        {
            // Arrange
            var fixture = new Fixture();
            var user = fixture.Create<User>();
            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockConfiguration = new Mock<IVerifyPasswordHashService>();
            var target = new AuthService(mockAuthRepository.Object, mockConfiguration.Object);
            var forgetDto = new PasswordDto
            {
                UserName = "username",
                Password = "Password@1234",
                ConfirmPassword = "Password@1234"
            };
            mockAuthRepository.Setup(repo => repo.ValidateUser(forgetDto.UserName)).Returns(user);
            mockAuthRepository.Setup(repo => repo.UpdateUser(user));

            // Act
            var result = target.PasswordService(forgetDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Password reset successfully!", result.Message);
            mockAuthRepository.Verify(repo => repo.ValidateUser(forgetDto.UserName), Times.Once);
            mockAuthRepository.Verify(repo => repo.UpdateUser(user), Times.Once);

        }

        //GetUserDetailById

        [Fact]
        public void GetContactsById_ReturnEmpty_WhenNoContactExist()
        {
            //Arrange
            var response = new ServiceResponse<IEnumerable<UserDetailDto>>
            {
                Success = false,

            };

            var mockAuthRepository= new Mock<IAuthRepository>();
            var mockConfiguration = new Mock<IVerifyPasswordHashService>();
            var target = new AuthService(mockAuthRepository.Object,mockConfiguration.Object);
            //Act
            var actual = target.GetUserDetailById(1);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal("No record found !.", actual.Message);
            Assert.False(actual.Success);
        }
        [Fact]
        public void GetContactsById_ReturnsContact_WhenContactsExist()
        {
            //Arrange
            var user = new User
            {
                UserId = 1,
                FirstName = "firstname",
                LastName = "lastname",
                LoginId = "loginid",
                Email = "email@gmail.com",
                ContactNumber = "4758498576",
              
            };

            var response = new ServiceResponse<IEnumerable<UserDetailDto>>
            {
                Success = true,

            };
            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockConfiguration = new Mock<IVerifyPasswordHashService>(); mockAuthRepository.Setup(c => c.GetUserDetailById(user.UserId)).Returns(user);
            var target = new AuthService(mockAuthRepository.Object, mockConfiguration.Object);
            //Act
            var actual = target.GetUserDetailById(user.UserId);

            //Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
            mockAuthRepository.Verify(c => c.GetUserDetailById(user.UserId), Times.Once);



        }

        //ModifyUser

       
        [Fact]
        public void ModifyUser_ReturnsAlreadyExists_WhenContactAlreadyExists()
        {
            var userId = 1;
            var user = new User()
            {
                UserId = userId,
                FirstName = "firstname",
                LastName = "lastname",
                LoginId = "loginid",
                Email = "email@gmail.com",
                ContactNumber = "4758498576",
            };


            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockConfiguration = new Mock<IVerifyPasswordHashService>();
            mockAuthRepository.Setup(r => r.UserExists(userId, user.LoginId,user.Email)).Returns(true);

            var authService = new AuthService(mockAuthRepository.Object, mockConfiguration.Object);

            // Act
            var actual = authService.ModifyUser(user);


            // Assert
            Assert.NotNull(actual);
            Assert.False(actual.Success);
            Assert.Equal("User Exists!", actual.Message);
            mockAuthRepository.Verify(r => r.UserExists(userId, user.LoginId, user.Email), Times.Once);
        }

        [Fact]
        public void ModifyUser_ReturnsSomethingWentWrong_WhenContactNotFound()
        {
            var userId = 1;
            var existingUser = new User()
            {
                UserId = userId,
                FirstName = "firstname",
                LastName = "lastname",
                LoginId = "loginid",
                Email = "email@gmail.com",
                ContactNumber = "4758498576",
            };


            
            var updatedUser = new User()
            {
                UserId = userId,
                FirstName = "firstname 1",
            };


            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockConfiguration = new Mock<IVerifyPasswordHashService>();           
            //mockRepository.Setup(r => r.ContactExists(contactId, updatedContact.ContactNumber)).Returns(false);
            mockAuthRepository.Setup(r => r.GetUserDetailById(updatedUser.UserId)).Returns<IEnumerable<User>>(null);

            var authService = new AuthService(mockAuthRepository.Object, mockConfiguration.Object);

            // Act
            var actual = authService.ModifyUser(existingUser);


            // Assert
            Assert.NotNull(actual);
            Assert.False(actual.Success);
            Assert.Equal("Something went wrong after sometime", actual.Message);
            //mockRepository.Verify(r => r.ContactExists(contactId, updatedContact.ContactNumber), Times.Once);
            mockAuthRepository.Verify(r => r.GetUserDetailById(updatedUser.UserId), Times.Once);
        }


        [Fact]
        public void ModifyUser_ReturnsUpdatedSuccessfully_WhenContactModifiedSuccessfully()
        {

            //Arrange
            var userId = 1;
            var existingUser = new User()
            {
                UserId = userId,
                FirstName = "firstname",
                LastName = "lastname",
                LoginId = "loginid",
                Email = "email@gmail.com",
                ContactNumber = "4758498576",
            };



            var updatedUser = new User()
            {
                UserId = userId,
                FirstName = "firstname 1",
            };


            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockConfiguration = new Mock<IVerifyPasswordHashService>();

            mockAuthRepository.Setup(c => c.UserExists(userId, existingUser.LoginId, existingUser.Email)).Returns(false);
            mockAuthRepository.Setup(c => c.GetUserDetailById(updatedUser.UserId)).Returns(existingUser);
            mockAuthRepository.Setup(c => c.UpdateUser(existingUser)).Returns(true);

            var target = new AuthService(mockAuthRepository.Object, mockConfiguration.Object);

            //Act

            var actual = target.ModifyUser(updatedUser);


            //Assert
            Assert.NotNull(actual);
            Assert.Equal("User Updated successfully", actual.Message);

            mockAuthRepository.Verify(c => c.GetUserDetailById(updatedUser.UserId),Times.Once);
            mockAuthRepository.Verify(c => c.UpdateUser(existingUser),Times.Once);

        }
        [Fact]
        public void ModifyUser_ReturnsError_WhenContactModifiedFails()
        {

            //Arrange
            var userId = 1;
            var existingUser = new User()
            {
                UserId = userId,
                FirstName = "firstname",
                LastName = "lastname",
                LoginId = "loginid",
                Email = "email@gmail.com",
                ContactNumber = "4758498576",
            };



            var updatedUser = new User()
            {
                UserId = userId,
                FirstName = "firstname 1",
            };


            var mockAuthRepository = new Mock<IAuthRepository>();
            var mockConfiguration = new Mock<IVerifyPasswordHashService>();

            mockAuthRepository.Setup(c => c.UserExists(userId, existingUser.LoginId, existingUser.Email)).Returns(false);
            mockAuthRepository.Setup(c => c.GetUserDetailById(updatedUser.UserId)).Returns(existingUser);
            mockAuthRepository.Setup(c => c.UpdateUser(existingUser)).Returns(false);

            var target = new AuthService(mockAuthRepository.Object, mockConfiguration.Object);

            //Act

            var actual = target.ModifyUser(updatedUser);


            //Assert
            Assert.NotNull(actual);
            Assert.Equal("Something went wrong after sometime", actual.Message);
            mockAuthRepository.Verify(c => c.GetUserDetailById(updatedUser.UserId), Times.Once);
            mockAuthRepository.Verify(c => c.UpdateUser(existingUser), Times.Once);

        }
    }
}
