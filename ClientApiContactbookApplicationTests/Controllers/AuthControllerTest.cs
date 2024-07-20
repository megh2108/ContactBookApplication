using ClientApiContactbookApplication.Controllers;
using ClientApiContactbookApplication.Infrastructure;
using ClientApiContactbookApplication.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ClientApiContactbookApplicationTests.Controllers
{
    public class AuthControllerTest
    {
        [Fact]
        public void Register_ReturnsView()
        {
            // Arrange
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockHttpContext = new Mock<HttpContext>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };
            //Act
            var result = target.Register() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        //Register Post
        [Fact]
        public void Register_ModelIsInvalid()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel { FirstName = "firstname" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };
            target.ModelState.AddModelError("LastName", "last name is required");
            //Act
            var actual = target.Register(registerViewModel) as ViewResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(registerViewModel, actual.Model);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            Assert.False(target.ModelState.IsValid);
        }
        [Fact]
        public void Register_RedirectToRegisterSuccess_WhenUserSavedSuccessfully()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            { FirstName = "firstname", LastName = "lastname", Password = "Password@123", Email = "email@gmail.com", ConfirmPassword = "Password@123", ContactNumber = "1234567890", LoginId = "loginid" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var successMessage = "User saved successfully";
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Success = true,
                Message = successMessage
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.PostHttpResponseMessage(It.IsAny<string>(), registerViewModel, It.IsAny<HttpRequest>()))
               .Returns(expectedResponse);
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.Register(registerViewModel) as RedirectToActionResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal("RegisterSuccess", actual.ActionName);
            Assert.Equal(successMessage, target.TempData["SuccessMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PostHttpResponseMessage(It.IsAny<string>(), registerViewModel, It.IsAny<HttpRequest>()), Times.Once);
            Assert.True(target.ModelState.IsValid);
        }
        [Fact]
        public void Register_ReturnView_WhenBadRequest()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            { FirstName = "firstname", LastName = "lastname", Password = "Password@123", Email = "email@gmail.com", ConfirmPassword = "Password@123", ContactNumber = "1234567890", LoginId = "loginid" };

            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var errorMessage = "Error Occurs";
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Success = false,
                Message = errorMessage
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.PostHttpResponseMessage(It.IsAny<string>(), registerViewModel, It.IsAny<HttpRequest>()))
               .Returns(expectedResponse);
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.Register(registerViewModel) as ViewResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PostHttpResponseMessage(It.IsAny<string>(), registerViewModel, It.IsAny<HttpRequest>()), Times.Once);
            Assert.True(target.ModelState.IsValid);
        }
        [Fact]
        public void Register_ReturnView_WhenBadRequest_WhenSomethingWentWrong()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            { FirstName = "firstname", LastName = "lastname", Password = "Password@123", Email = "email@gmail.com", ConfirmPassword = "Password@123", ContactNumber = "1234567890", LoginId = "loginid" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");

            var expectedResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(JsonConvert.SerializeObject(null))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.PostHttpResponseMessage(It.IsAny<string>(), registerViewModel, It.IsAny<HttpRequest>()))
               .Returns(expectedResponse);
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.Register(registerViewModel) as ViewResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal("Something went wrong. Please try after sometime.", target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PostHttpResponseMessage(It.IsAny<string>(), registerViewModel, It.IsAny<HttpRequest>()), Times.Once);
            Assert.True(target.ModelState.IsValid);
        }
        //HttpGet RegisterSuccess
        [Fact]
        public void RegisterSuccess_ReturnsView()
        {
            // Arrange
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockHttpContext = new Mock<HttpContext>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };
            //Act
            var result = target.RegisterSuccess() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
        //HttpGet Login
        [Fact]
        public void Login_ReturnsView()
        {
            // Arrange
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockHttpContext = new Mock<HttpContext>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };
            //Act
            var result = target.Login() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
        //Login HttpPost
        [Fact]
        public void Login_ModelIsInvalid()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            { Password = "Password@123" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };
            target.ModelState.AddModelError("UserName", "Username is required");
            //Act
            var actual = target.Login(loginViewModel) as ViewResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(loginViewModel, actual.Model);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            Assert.False(target.ModelState.IsValid);
        }
        [Fact]
        public void Login_ReturnView_WhenBadRequest()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            { Password = "Password@123", Username = "loginid" };

            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var errorMessage = "Error Occurs";
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Success = false,
                Message = errorMessage
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.PostHttpResponseMessage(It.IsAny<string>(), loginViewModel, It.IsAny<HttpRequest>()))
               .Returns(expectedResponse);
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.Login(loginViewModel) as ViewResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PostHttpResponseMessage(It.IsAny<string>(), loginViewModel, It.IsAny<HttpRequest>()), Times.Once);
            Assert.True(target.ModelState.IsValid);
        }
        [Fact]
        public void Login_Success_RedirectToAction()
        {
            //Arrange
            var loginViewModel = new LoginViewModel { Username = "loginid", Password = "Password" };
            var mockToken = "mockToken";
            var mockUserId = "1";
            var mockUserDetails = new UserDetailViewModel
            {
                UserId = 1,
                FirstName = "Firstname",
                LastName = "last name",
                Email = "abc@gmail.com",
                ContactNumber = "9089876567",
                LoginId = "loginid",
                FileName = "abc.png",
                File = new byte[10]
            };

            var mockResponseCookie = new Mock<IResponseCookies>();

            mockResponseCookie.Setup(c => c.Append("jwtToken", mockToken, It.IsAny<CookieOptions>()));
            mockResponseCookie.Setup(c => c.Append("userId", mockUserId, It.IsAny<CookieOptions>()));

            var mockHttpContext = new Mock<HttpContext>();
            var mockHttpResponse = new Mock<HttpResponse>();
            mockHttpContext.SetupGet(c => c.Response).Returns(mockHttpResponse.Object);
            mockHttpResponse.SetupGet(c => c.Cookies).Returns(mockResponseCookie.Object);

            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");

            var expectedUserDetail = new UserDetailViewModel
            {
                UserId = 1,
                FirstName = "Firstname",
                LastName = "last name",
                Email = "abc@gmail.com",
                ContactNumber = "9089876567",
                LoginId = "loginid",
                FileName = "abc.png",
                File = new byte[10]
            };

            var expectedServiceResponse = new ServiceResponse<string>
            {
                Success = true,
                Data = mockToken,

            };
            var expectedUserServiceResponse = new ServiceResponse<UserDetailViewModel>
            {
                Success = true,
                Data = expectedUserDetail,

            };


            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var expectedUserResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedUserServiceResponse))
            };


            mockHttpClientService.Setup(c => c.PostHttpResponseMessage(It.IsAny<string>(), loginViewModel, It.IsAny<HttpRequest>()))
             .Returns(expectedResponse);

            var userId = Convert.ToInt32(mockUserId);

            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<UserDetailViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedUserResponse);

            var jwtToken = new JwtSecurityToken(claims: new[] { new Claim("UserId", mockUserId) });
            mockTokenHandler.Setup(t => t.ReadJwtToken(It.IsAny<string>())).Returns(jwtToken);


            //var mockAuthController = new Mock<AuthController>(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object);
            //mockAuthController.CallBase = true;
            //mockAuthController.Setup(c => c.UserDetailById(It.IsAny<int>())).Returns(mockUserDetails);

            var mockAuthController = new Mock<AuthController> { CallBase = true };
            mockAuthController.Setup(c => c.UserDetailById(1)).Returns(mockUserDetails);

            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.Login(loginViewModel) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal("Contactbook", actual.ControllerName);
            mockResponseCookie.Verify(c => c.Append("jwtToken", mockToken, It.IsAny<CookieOptions>()), Times.Once);
            mockResponseCookie.Verify(c => c.Append("userId", mockUserId, It.IsAny<CookieOptions>()), Times.Once);

            if (mockUserDetails != null && mockUserDetails.File != null)
            {
                var image = Convert.ToBase64String(mockUserDetails.File);
                int chunkSize = 3800;
                int totalChunks = (image.Length + chunkSize - 1) / chunkSize;

                for (int i = 0; i < totalChunks; i++)
                {
                    string chunk = image.Substring(i * chunkSize, Math.Min(chunkSize, image.Length - i * chunkSize));
                    mockResponseCookie.Verify(c => c.Append($"image_chunk_{i}", chunk, It.IsAny<CookieOptions>()), Times.Once);
                }
            }

            mockTokenHandler.Verify(t => t.ReadJwtToken(It.IsAny<string>()), Times.Once);
            mockHttpContext.VerifyGet(c => c.Response, Times.Exactly(3));
            mockHttpResponse.VerifyGet(c => c.Cookies, Times.Exactly(3));
            Assert.True(target.ModelState.IsValid);

            mockHttpClientService.Verify(c => c.PostHttpResponseMessage(It.IsAny<string>(), loginViewModel, It.IsAny<HttpRequest>()), Times.Once);

            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<UserDetailViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);

        }

        [Fact]
        public void Login_ReturnView_WhenBadRequest_WhenSomethingWentWrong()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            { Password = "Password@123", Username = "loginid" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");

            var expectedResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(JsonConvert.SerializeObject(null))
            };

            var mockHttpContext = new Mock<HttpContext>();

            mockHttpClientService.Setup(c => c.PostHttpResponseMessage(It.IsAny<string>(), loginViewModel, It.IsAny<HttpRequest>()))
               .Returns(expectedResponse);

            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.Login(loginViewModel) as ViewResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal("Something went wrong. Please try after sometime.", target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PostHttpResponseMessage(It.IsAny<string>(), loginViewModel, It.IsAny<HttpRequest>()), Times.Once);
            Assert.True(target.ModelState.IsValid);
        }

        [Fact]
        public void Logout_DeleteJwtToken()
        {
            //Arrange
            var mockResponseCookie = new Mock<IResponseCookies>();

            var mockRequestCookie = new Mock<IRequestCookieCollection>();
            var imagCookies = new Dictionary<string, string>
            {
                { "image_chunk_0", "chunk0" },
                { "image_chunk_1", "chunk1" }
            };
            mockRequestCookie.Setup(c => c.ContainsKey(It.IsAny<string>())).Returns((string key) => imagCookies.ContainsKey(key));
            mockResponseCookie.Setup(c => c.Delete(It.IsAny<string>())).Callback((string key) => imagCookies.Remove(key));

            mockResponseCookie.Setup(c => c.Delete("jwtToken"));
            mockResponseCookie.Setup(c => c.Delete("userId"));

            var mockHttpContext = new Mock<HttpContext>();
            var mockHttpResponse = new Mock<HttpResponse>();
            var mockHttpRequest = new Mock<HttpRequest>();
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();

            mockHttpRequest.SetupGet(r => r.Cookies).Returns(mockRequestCookie.Object);
            mockHttpContext.SetupGet(c => c.Request).Returns(mockHttpRequest.Object);
            mockHttpContext.SetupGet(c => c.Response).Returns(mockHttpResponse.Object);
            mockHttpResponse.SetupGet(c => c.Cookies).Returns(mockResponseCookie.Object);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };
            //Act
            var actual = target.Logout() as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal("Index", actual.ActionName);
            Assert.Equal("Home", actual.ControllerName);

            mockResponseCookie.Verify(c => c.Delete("jwtToken"), Times.Once);
            mockResponseCookie.Verify(c => c.Delete("userId"), Times.Once);
            mockResponseCookie.Verify(c => c.Delete("image_chunk_0"), Times.Once);
            mockResponseCookie.Verify(c => c.Delete("image_chunk_1"), Times.Once);

            mockHttpContext.VerifyGet(c => c.Response, Times.Exactly(4));
            mockHttpResponse.VerifyGet(c => c.Cookies, Times.Exactly(4));
            mockRequestCookie.Verify(c => c.ContainsKey(It.IsAny<string>()), Times.Exactly(3)); // For image_chunk_0 and image_chunk_1

        }


        //HttpGet Forgot password
        [Fact]
        public void ForgotPassword_ReturnsView()
        {
            // Arrange
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockHttpContext = new Mock<HttpContext>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };
            //Act
            var result = target.ForgotPassword() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        //Forgot password HttpPost
        [Fact]
        public void ForgotPassword_ModelIsInvalid()
        {
            // Arrange
            var viewModel = new PasswordViewModel
            {
                Password = "Password@123"
            };

            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };
            target.ModelState.AddModelError("UserName", "Username is required");
            //Act
            var actual = target.ForgotPassword(viewModel) as ViewResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(viewModel, actual.Model);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            Assert.False(target.ModelState.IsValid);
        }



        [Fact]
        public void ForgotPasswordConfirmation_ReturnView_WhenBadRequest()
        {
            // Arrange
            var viewModel = new PasswordViewModel
            { UserName = "loginid", Password = "Password@123", ConfirmPassword = "Password@123" };

            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var errorMessage = "Error Occurs";
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Success = false,
                Message = errorMessage
            };

            var expectedResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };

            var mockHttpContext = new Mock<HttpContext>();
            var mockHttpRequest = new Mock<HttpRequest>();

            mockHttpContext.Setup(m => m.Request).Returns(mockHttpRequest.Object);


            mockHttpClientService.Setup(c => c.PostHttpResponseMessage(It.IsAny<string>(), viewModel, mockHttpRequest.Object))
               .Returns(expectedResponse);

            var mockTempDataProvider = new Mock<ITempDataProvider>();

            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ForgotPassword(viewModel) as ViewResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PostHttpResponseMessage(It.IsAny<string>(), viewModel, mockHttpRequest.Object), Times.Once);
            Assert.True(target.ModelState.IsValid);
        }



        [Fact]
        public void ForgotPasswordConfirmation_WhenBadRequest_WhenSomethingWentWrong()
        {
            // Arrange
            var viewModel = new PasswordViewModel
            { UserName = "loginid", Password = "Password@123", ConfirmPassword = "Password@123" };

            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");

            var expectedResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(JsonConvert.SerializeObject(null))
            };

            var mockHttpContext = new Mock<HttpContext>();
            var mockHttpRequest = new Mock<HttpRequest>();

            mockHttpContext.Setup(m => m.Request).Returns(mockHttpRequest.Object);



            mockHttpClientService.Setup(c => c.PostHttpResponseMessage(It.IsAny<string>(), viewModel, mockHttpRequest.Object))
              .Returns(expectedResponse);

            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ForgotPassword(viewModel) as ViewResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal("Something went wrong. Please try again later.", target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PostHttpResponseMessage(It.IsAny<string>(), viewModel, mockHttpRequest.Object), Times.Once);
            Assert.True(target.ModelState.IsValid);
        }

        [Fact]
        public void ForgotPasswordConfirmation_Success_RedirectToAction()
        {
            // Arrange
            var viewModel = new PasswordViewModel
            { UserName = "loginid", Password = "Password@123", ConfirmPassword = "Password@123" };


            var mockHttpContext = new Mock<HttpContext>();
            var mockHttpRequest = new Mock<HttpRequest>();

            mockHttpContext.Setup(m => m.Request).Returns(mockHttpRequest.Object);

            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Success = true,

            };

            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };


            mockHttpClientService.Setup(c => c.PostHttpResponseMessage(It.IsAny<string>(), viewModel, mockHttpRequest.Object))
              .Returns(expectedResponse);

            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ForgotPassword(viewModel) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal("PasswordConfirmation", actual.ActionName);

            Assert.True(target.ModelState.IsValid);

        }



        //HttpGet ForgotPasswordConfirmation
        [Fact]
        public void ForgotPasswordConfirmation_ReturnsView()
        {
            // Arrange
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            var mockHttpContext = new Mock<HttpContext>();
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };
            //Act
            var result = target.PasswordConfirmation() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

       

        //EditUser HttpGet
        [Fact]
        public void EditUser_ReturnsViewResult_WhenUserDetailsObtainedSuccessfully()
        {
            //Arrange
            var userId = 1;
            var expectedUser = new UserDetailViewModel { UserId = 1, FirstName = "FirstName 1" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var expectedServiceResponse = new ServiceResponse<UserDetailViewModel>
            {
                Success = true,
                Data = expectedUser,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<UserDetailViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.EditUser(userId) as ViewResult;
            var model = actual.Model as UserDetailViewModel;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.NotNull(model);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<UserDetailViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);

        }
        [Fact]
        //if(else)
        public void EditUser_ReturnsRedirectToActionResult_WhenSuccessIsFalse()
        {
            //Arrange
            var userId = 1;
            var expectedUser = new UserDetailViewModel { UserId = 1, FirstName = "FirstName 1" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var errorMessage = "Error Occured";
            var expectedServiceResponse = new ServiceResponse<UserDetailViewModel>
            {
                Success = false,
                Data = expectedUser,
                Message = errorMessage,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<UserDetailViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.EditUser(userId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("Register", actual.ActionName);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<UserDetailViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }
        [Fact]
        //if(else) when data is null
        public void EditUser_ReturnsRedirectToActionResult_WhenDataIsNull()
        {
            //Arrange

            var userId = 1;
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var errorMessage = "Error Occured";
            var expectedServiceResponse = new ServiceResponse<UserDetailViewModel>
            {
                Success = true,
                Data = null,
                Message = errorMessage,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<UserDetailViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.EditUser(userId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("Register", actual.ActionName);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<UserDetailViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }
        [Fact]
        //else(if)
        public void EditUser_ReturnsRedirectToActionResult_WhenStatusCodeIsBadRequest_ErrorResponseIsNotNull()
        {
            //Arrange
            var userId = 1;
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var errorMessage = "Error Occured";
            var expectedServiceResponse = new ServiceResponse<UserDetailViewModel>
            {
                Success = false,
                Message = errorMessage,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<UserDetailViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.EditUser(userId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("Register", actual.ActionName);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<UserDetailViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }
        //else(else)
        [Fact]
        public void EditUser_ReturnsRedirectToActionResult_WhenStatusCodeIsBadRequest_ErrorResponseIsNull()
        {
            //Arrange
            var userId = 1;
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(null))
            };
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<UserDetailViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.EditUser(userId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("Register", actual.ActionName);
            Assert.Equal("Something went wrong.Please try after sometime.", target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<UserDetailViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }

        //HttpPost
        //else(if)
        [Fact]
        public void EditUser_ReturnsRedirectToActionResult_WhenStatusCodeBadRequest_ErrorResponseIsNotNull()
        {
            //Arrange

            var expectedUser = new UserDetailViewModel { UserId = 1, FirstName = "FirstName 1" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var errorMessage = "Error Occured";
            var expectedServiceResponse = new ServiceResponse<UserDetailViewModel>
            {
                Success = false,
                Message = errorMessage,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.PutHttpResponseMessage(It.IsAny<string>(), expectedUser, It.IsAny<HttpRequest>())).Returns(expectedResponse);

            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.EditUser(expectedUser) as ViewResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PutHttpResponseMessage(It.IsAny<string>(), expectedUser, It.IsAny<HttpRequest>()), Times.Once);
        }
        //else(else)
        [Fact]
        public void EditUser_ReturnsRedirectToActionResult_WhenStatusCodeBadRequest_ErrorResponseIsNull()
        {
            //Arrange
            var expectedUser = new UserDetailViewModel { UserId = 1, FirstName = "FirstName 1"};
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(null))
            };
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.PutHttpResponseMessage(It.IsAny<string>(), expectedUser, It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.EditUser(expectedUser) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal("Contactbook", actual.ControllerName);
            Assert.Equal("Something went wrong try after some time", target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PutHttpResponseMessage(It.IsAny<string>(), expectedUser, It.IsAny<HttpRequest>()), Times.Once);
        }
        [Fact]
        public void EditUser_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            //Arrange
            var expectedUser = new UserDetailViewModel { UserId = 1, FirstName = "FirstName 1" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            var mockHttpContext = new Mock<HttpContext>();
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };
            target.ModelState.AddModelError("Email", "Email description is required.");

            //Act
            var actual = target.EditUser(expectedUser) as ViewResult;

            //Assert
            Assert.NotNull(actual);
            Assert.False(target.ModelState.IsValid);
        }
     
        [Fact]
        public void Edit_RedirectToActionResult_WhenUserUpdatedSuccessfully()
        {
            //Arrange
            var expectedUser = new UserDetailViewModel
            {
                UserId = 1, 
                FirstName = "FirstName 1"
            
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            var expectedServiceResponse = new ServiceResponse<UserDetailViewModel>
            {
                Success = true,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            mockHttpClientService.Setup(c => c.PutHttpResponseMessage(It.IsAny<string>(), expectedUser, It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.EditUser(expectedUser) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal("Logout", actual.ActionName);
            Assert.True(target.ModelState.IsValid);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PutHttpResponseMessage(It.IsAny<string>(), expectedUser, It.IsAny<HttpRequest>()), Times.Once);
        }


        //HttpGet change password
        [Fact]
        public void ChangePassword_ReturnsView()
        {
            // Arrange
           
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockHttpContext = new Mock<HttpContext>();
            var mockIdentity = new Mock<IIdentity>();
            mockIdentity.Setup(x => x.Name).Returns("user"); // Mocking User.Identity.Name
            var mockPrincipal = new Mock<ClaimsPrincipal>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockPrincipal.Setup(x => x.Identity).Returns(mockIdentity.Object);
            mockHttpContext.Setup(x => x.User).Returns(mockPrincipal.Object);

            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };
            //Act
            var result = target.ChangePassword() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PasswordViewModel>(result.Model);
            var model = result.Model as PasswordViewModel;
            Assert.Equal("user", model.UserName);
        }


        //ChangePassword Post
        [Fact]
        public void ChangePassword_ModelIsInvalid()
        {
            // Arrange
            var passwordViewModel = new PasswordViewModel
            { Password = "Password@123" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };
            target.ModelState.AddModelError("UserName", "Username is required");
            //Act
            var actual = target.ChangePassword(passwordViewModel) as ViewResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(passwordViewModel, actual.Model);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            Assert.False(target.ModelState.IsValid);
        }
        [Fact]
        public void ChangePassword_RedirectToForgotPasswordConfirmation_WhenUserSuccessfullyUpdatePassword()
        {
            // Arrange
            var passwordViewModel = new PasswordViewModel
            { UserName = "username", Password = "Password@123", ConfirmPassword = "Password@123" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var successMessage = "Password reset successfully";
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Success = true,
                Message = successMessage
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.PostHttpResponseMessage(It.IsAny<string>(), passwordViewModel, It.IsAny<HttpRequest>()))
               .Returns(expectedResponse);
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ChangePassword(passwordViewModel) as RedirectToActionResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal("Logout", actual.ActionName);
            Assert.Equal(successMessage, target.TempData["SuccessMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PostHttpResponseMessage(It.IsAny<string>(), passwordViewModel, It.IsAny<HttpRequest>()), Times.Once);
            Assert.True(target.ModelState.IsValid);
        }
        [Fact]
        public void ChangePassword_RedirectToAction_WhenBadRequest()
        {
            // Arrange
            var passwordViewModel = new PasswordViewModel
            { UserName = "username", Password = "Password@123", ConfirmPassword = "Password@123" };

            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var errorMessage = "Error Occurs";
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Success = false,
                Message = errorMessage
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.PostHttpResponseMessage(It.IsAny<string>(), passwordViewModel, It.IsAny<HttpRequest>()))
               .Returns(expectedResponse);
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ChangePassword(passwordViewModel) as ViewResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PostHttpResponseMessage(It.IsAny<string>(), passwordViewModel, It.IsAny<HttpRequest>()), Times.Once);
            Assert.True(target.ModelState.IsValid);
        }
        [Fact]
        public void ChangePassword_RedirectToAction_WhenBadRequest_WhenSomethingWentWrong()
        {
            // Arrange
            var passwordViewModel = new PasswordViewModel
            { UserName = "username", Password = "Password@123", ConfirmPassword = "Password@123" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenHandler = new Mock<IJwtTokenHandler>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");

            var expectedResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(JsonConvert.SerializeObject(null))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.PostHttpResponseMessage(It.IsAny<string>(), passwordViewModel, It.IsAny<HttpRequest>()))
               .Returns(expectedResponse);
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var target = new AuthController(mockHttpClientService.Object, mockConfiguration.Object, mockTokenHandler.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ChangePassword(passwordViewModel) as ViewResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal("Something went wrong. Please try again later.", target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PostHttpResponseMessage(It.IsAny<string>(), passwordViewModel, It.IsAny<HttpRequest>()), Times.Once);
            Assert.True(target.ModelState.IsValid);
        }
    }
}
