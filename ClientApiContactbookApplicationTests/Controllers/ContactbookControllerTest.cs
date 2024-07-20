using ClientApiContactbookApplication.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ClientApiContactbookApplication.ViewModels;
using ClientApiContactbookApplication.Controllers;
using Newtonsoft.Json;
using System.Net;

namespace ClientApiContactbookApplicationTests.Controllers
{
    public class ContactbookControllerTest
    {
        //ShowAllContact

        [Fact]
        public void ShowAllContact_ReturnsContacts()
        {
            //Arrange
            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
                .Returns(response);

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllContact() as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void ShowAllContact_ReturnsNoRecordFound()
        {
            //Arrange
            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = "No record found !."
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
                .Returns(response);

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllContact() as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void ShowAllContact_ReturnsNoContacts()
        {
            //Arrange
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = false,
                Message=""
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
                .Returns(response);

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllContact() as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        //RedirectGroupContact
        [Fact]
        public void RedirectGroupContact_ReturnsContacts()
        {
            //Arrange
            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
                .Returns(response);

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.RedirectGroupContact("a") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void RedirectGroupContact_ReturnsNoRecordFound()
        {
            //Arrange
            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = "No record found !."
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
                .Returns(response);

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.RedirectGroupContact("a") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void RedirectGroupContact_ReturnsNoContacts()
        {
            //Arrange
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = false,
                Message = ""
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
                .Returns(response);

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.RedirectGroupContact("a") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }


        //showallcontactwithpagination
        [Fact]
        public void showallcontactwithpagination_ReturnsContacts_WhenSerachIsNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllContactWithPagination(null, 1, 2, "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void showallcontactwithpagination_ReturnsContacts_WhenSerachIsNotNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllContactWithPagination("fir", 1, 2, "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void showallcontactwithpagination_ReturnsNoContacts_WhenSerachIsNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>();
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = false,
                Data = contacts,
                Message = ""

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllContactWithPagination(null, 1, 2, "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void showallcontactwithpagination_ReturnsNoContacts_WhenSerachIsNotNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllContactWithPagination("fir", 1, 2, "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void showallcontactwithpagination_ReturnsNoNoRecordFound_WhenSerachIsNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>();
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = false,
                Data = contacts,
                Message = "No record found !."

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllContactWithPagination(null, 1, 2, "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void showallcontactwithpagination_ReturnsNoNoRecordFound_WhenSerachIsNotNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = "No record found !."

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllContactWithPagination("fir", 1, 2, "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void showallcontactwithpagination_ReturnsZeroContacts_WhenSerachIsNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 0 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllContactWithPagination(null, 1, 2, "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);

            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void showallcontactwithpagination_ReturnsZeroContacts_WhenSerachIsNotNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 0 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllContactWithPagination("fir", 1, 2, "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);

            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void showallcontactwithpagination_ReturnsContacts_WhenSerachIsNull_PageIsGreaterThanTotalCount()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 11 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllContactWithPagination(null, 6, 6, "asc") as RedirectToActionResult;
            //Assert
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void showallcontactwithpagination_ReturnsContacts_WhenSerachIsNotNull_PageIsGreaterThanTotalCount()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 11 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllContactWithPagination( "fir", 6, 6, "asc") as RedirectToActionResult;
            //Assert
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        //ShowAllFavouriteContactWithPagination
        [Fact]
        public void ShowAllFavouriteContactWithPagination_ReturnsContacts()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllFavouriteContactWithPagination( 1, 2, "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void ShowAllFavouriteContactWithPagination_ReturnsNoContacts()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>();
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = false,
                Data = contacts,
                Message = ""

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllFavouriteContactWithPagination( 1, 2, "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void ShowAllFavouriteContactWithPagination_ReturnsNoNoRecordFound()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>();
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = false,
                Data = contacts,
                Message = "No record found !."

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllFavouriteContactWithPagination(1, 2, "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void ShowAllFavouriteContactWithPagination_ReturnsZeroContacts()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 0 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllFavouriteContactWithPagination( 1, 2, "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);

            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void ShowAllFavouriteContactWithPagination_ReturnsContacts_PageIsGreaterThanTotalCount()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 11 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowAllFavouriteContactWithPagination( 6, 6, "asc") as RedirectToActionResult;
            //Assert
            Assert.Equal("ShowAllFavouriteContactWithPagination", actual.ActionName);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        //ShowSpecificContactWithPagination

        [Fact]
        public void ShowSpecificContactWithPagination_ReturnsContacts_WhenSerachIsNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowSpecificContactWithPagination(null, 1, 2,"a", "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void ShowSpecificContactWithPagination_ReturnsContacts_WhenSerachIsNotNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowSpecificContactWithPagination("fir", 1, 2, "a", "asc") as ViewResult;

            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void ShowSpecificContactWithPagination_ReturnsNoContacts_WhenSerachIsNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>();
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = false,
                Data = contacts,
                Message = ""

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowSpecificContactWithPagination(null, 1, 2, "a", "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void ShowSpecificContactWithPagination_ReturnsNoContacts_WhenSerachIsNotNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowSpecificContactWithPagination("fir", 1, 2, "a", "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void ShowSpecificContactWithPagination_ReturnsNoNoRecordFound_WhenSerachIsNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>();
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = false,
                Data = contacts,
                Message = "No record found !."

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowSpecificContactWithPagination(null, 1, 2, "a", "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void ShowSpecificContactWithPagination_ReturnsNoNoRecordFound_WhenSerachIsNotNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = "No record found !."

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowSpecificContactWithPagination("fir", 1, 2, "a", "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void ShowSpecificContactWithPagination_ReturnsZeroContacts_WhenSerachIsNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 0 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowSpecificContactWithPagination(null, 1, 2, "a", "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);

            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void ShowSpecificContactWithPagination_ReturnsZeroContacts_WhenSerachIsNotNull()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 0 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowSpecificContactWithPagination("fir", 1, 2, "a", "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);

            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void ShowSpecificContactWithPagination_ReturnsContacts_WhenSerachIsNull_PageIsGreaterThanTotalCount()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 11 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowSpecificContactWithPagination(null, 6, 6, "a", "asc") as RedirectToActionResult;

            //Assert
            Assert.Equal("ShowSpecificContactWithPagination", actual.ActionName);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void ShowSpecificContactWithPagination_ReturnsContacts_WhenSerachIsNotNull_PageIsGreaterThanTotalCount()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 11 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowSpecificContactWithPagination("fir", 6, 6, "a", "asc") as RedirectToActionResult;

            //Assert
            Assert.Equal("ShowSpecificContactWithPagination", actual.ActionName);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        //ShowSpecificFavouriteContactWithPagination
        [Fact]
        public void ShowSpecificFavouriteContactWithPagination_ReturnsContacts()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowSpecificFavouriteContactWithPagination(1, 2,"a", "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void ShowSpecificFavouriteContactWithPagination_ReturnsNoContacts()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>();
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = false,
                Data = contacts,
                Message = ""

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowSpecificFavouriteContactWithPagination(1, 2, "a", "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void ShowSpecificFavouriteContactWithPagination_ReturnsNoNoRecordFound()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>();
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = false,
                Data = contacts,
                Message = "No record found !."

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 3 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowSpecificFavouriteContactWithPagination(1, 2, "a", "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Exactly(2));
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void ShowSpecificFavouriteContactWithPagination_ReturnsZeroContacts()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var response = new ServiceResponse<IEnumerable<ContactbookViewModel>>
            {
                Success = true,
                Data = contacts,
                Message = ""

            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
                .Returns(response);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 0 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowSpecificFavouriteContactWithPagination(1, 2, "a", "asc") as ViewResult;
            //Assert
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);

            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        [Fact]
        public void ShowSpecificFavouriteContactWithPagination_ReturnsContacts_PageIsGreaterThanTotalCount()
        {
            //Arrange

            var contacts = new List<ContactbookViewModel>
            {
                new ContactbookViewModel{ ContactId=1, Name="Name 1"},
                new ContactbookViewModel{ ContactId=2, Name="Name 2"},
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, mockHttpContext.Object.Request, null, 60))
               .Returns(new ServiceResponse<int> { Success = true, Data = 11 });

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                },
            };

            //Act
            var actual = target.ShowSpecificFavouriteContactWithPagination(6, 6, "a", "asc") as RedirectToActionResult;

            //Assert
            Assert.Equal("ShowSpecificFavouriteContactWithPagination", actual.ActionName);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<int>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }



        //create contact get

        [Fact]
        public void Create_ReturnsViewResult_WithCountryList_WhenCountryExists()
        {
            //Arrange
            var expectedCountry = new List<CountryContactbookViewModel>
            {
                new CountryContactbookViewModel {CountryId = 1,CountryName = "Country 1"},
                new CountryContactbookViewModel {CountryId = 2,CountryName = "Country 2"},

            };
            var expectedState = new List<StateContactbookViewModel>
            {
                new StateContactbookViewModel {StateId=1,CountryId = 1,StateName = "State 1"},
                new StateContactbookViewModel {StateId=2,CountryId = 2,StateName = "State 2"},

            };
            var expectedResponse = new ServiceResponse<IEnumerable<CountryContactbookViewModel>>
            {
                Success = true,
                Data = expectedCountry,
            };
            var expectedStateResponse = new ServiceResponse<IEnumerable<StateContactbookViewModel>>
            {
                Success = true,
                Data = expectedState,
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            var mockHttpContext = new Mock<HttpContext>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
                .Returns(expectedResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
              .Returns(expectedStateResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };
            //Act
            var actual = target.CreateContact() as ViewResult;

            //Assert
            Assert.NotNull(actual);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);

        }
        [Fact]
        public void Create_ReturnsViewResult_WithEmptyCountryList_WhenCountryDoesNotExists()
        {
            //Arrange
            var expectedResponse = new ServiceResponse<IEnumerable<CountryContactbookViewModel>>
            {
                Success = false,
            };
            var expectedStateResponse = new ServiceResponse<IEnumerable<StateContactbookViewModel>>
            {
                Success = false,
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            var mockHttpContext = new Mock<HttpContext>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
                .Returns(expectedResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
                .Returns(expectedStateResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };
            //Act
            var actual = target.CreateContact() as ViewResult;

            //Assert
            Assert.NotNull(actual);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);

        }
        
        //createcontact post
        [Fact]
        public void Create_RedirectToActionResult_WhenContactSavedSuccessfully()
        {
            //Arrange
            var expectedCountry = new List<CountryContactbookViewModel>
            {
                new CountryContactbookViewModel {CountryId = 1,CountryName = "Country 1"},
                new CountryContactbookViewModel {CountryId = 2,CountryName = "Country 2"},

            };
            var expectedState = new List<StateContactbookViewModel>
            {
                new StateContactbookViewModel {StateId=1,CountryId = 1,StateName = "State 1"},
                new StateContactbookViewModel {StateId=2,CountryId = 2,StateName = "State 2"},

            };

            var viewModel = new AddContactbookViewModel 
            { Name = "Name 1", StateId = 1, CountryId = 1, StateContactbook = expectedState, CountryContactbook = expectedCountry,FileName="xyz.png",File=new byte[0]
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var successMessage = "Contact Saved Successfully";
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Message = successMessage,
            };
            var expectedCountryResponse = new ServiceResponse<IEnumerable<CountryContactbookViewModel>>
            {
                Success = true,
                Data = expectedCountry,
            };
            var expectedStateResponse = new ServiceResponse<IEnumerable<StateContactbookViewModel>>
            {
                Success = true,
                Data = expectedState,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.PostHttpResponseMessage(It.IsAny<string>(), viewModel, It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
               .Returns(expectedCountryResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
              .Returns(expectedStateResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.CreateContact(viewModel) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal("Contact Saved Successfully", target.TempData["successMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PostHttpResponseMessage(It.IsAny<string>(), viewModel, It.IsAny<HttpRequest>()), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
      
        [Fact]
        public void Create_ReturnsViewResultWithErrorMessage_WhenResponseIsNotSuccess()
        {
            //Arrange
            var expectedCountry = new List<CountryContactbookViewModel>
            {
                new CountryContactbookViewModel {CountryId = 1,CountryName = "Country 1"},
                new CountryContactbookViewModel {CountryId = 2,CountryName = "Country 2"},

            };
            var expectedState = new List<StateContactbookViewModel>
            {
                new StateContactbookViewModel {StateId=1,CountryId = 1,StateName = "State 1"},
                new StateContactbookViewModel {StateId=2,CountryId = 2,StateName = "State 2"},

            };
            var viewModel = new AddContactbookViewModel
            {
                Name = "Name 1",
                StateId = 1,
                CountryId = 1,
                StateContactbook = expectedState,
                CountryContactbook = expectedCountry,
                FileName = "xyz.png",
                File = new byte[0]
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var errorMessage = "Error Occured";
            var expectedErrorResponse = new ServiceResponse<string>
            {
                Message = errorMessage,
            };
            var expectedCountryResponse = new ServiceResponse<IEnumerable<CountryContactbookViewModel>>
            {
                Success = true,
                Data = expectedCountry,
            };
            var expectedStateResponse = new ServiceResponse<IEnumerable<StateContactbookViewModel>>
            {
                Success = true,
                Data = expectedState,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedErrorResponse))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.PostHttpResponseMessage(It.IsAny<string>(), viewModel, It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
              .Returns(expectedCountryResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
              .Returns(expectedStateResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.CreateContact(viewModel) as ViewResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);

            Assert.Equal(errorMessage, target.TempData["errorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PostHttpResponseMessage(It.IsAny<string>(), viewModel, It.IsAny<HttpRequest>()), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void Create_ReturnsRedirectToActionResult_WhenResponseIsNotSuccess()
        {
            //Arrange
            var expectedCountry = new List<CountryContactbookViewModel>
            {
                new CountryContactbookViewModel {CountryId = 1,CountryName = "Country 1"},
                new CountryContactbookViewModel {CountryId = 2,CountryName = "Country 2"},

            };
            var expectedState = new List<StateContactbookViewModel>
            {
                new StateContactbookViewModel {StateId=1,CountryId = 1,StateName = "State 1"},
                new StateContactbookViewModel {StateId=2,CountryId = 2,StateName = "State 2"},

            };
            var viewModel = new AddContactbookViewModel
            {
                Name = "Name 1",
                StateId = 1,
                CountryId = 1,
                StateContactbook = expectedState,
                CountryContactbook = expectedCountry,
                FileName = "xyz.png",
                File = new byte[0]
            };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var errorMessage = "Something went wrong. Please try after sometime.";
            var expectedCountryResponse = new ServiceResponse<IEnumerable<CountryContactbookViewModel>>
            {
                Success = true,
                Data = expectedCountry,
            };
            var expectedStateResponse = new ServiceResponse<IEnumerable<StateContactbookViewModel>>
            {
                Success = true,
                Data = expectedState,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(null))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.PostHttpResponseMessage(It.IsAny<string>(), viewModel, It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
               .Returns(expectedCountryResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
              .Returns(expectedStateResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.CreateContact(viewModel) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PostHttpResponseMessage(It.IsAny<string>(), viewModel, It.IsAny<HttpRequest>()), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void Create_ReturnsViewResult_WithContactList_WhenModelStateIsInvalid()
        {
            //Arrange
            var expectedCountry = new List<CountryContactbookViewModel>
            {
                new CountryContactbookViewModel {CountryId = 1,CountryName = "Country 1"},
                new CountryContactbookViewModel {CountryId = 2,CountryName = "Country 2"},

            };
            var expectedState = new List<StateContactbookViewModel>
            {
                new StateContactbookViewModel {StateId=1,CountryId = 1,StateName = "State 1"},
                new StateContactbookViewModel {StateId=2,CountryId = 2,StateName = "State 2"},

            };
            var expectedResponse = new ServiceResponse<IEnumerable<CountryContactbookViewModel>>
            {
                Success = true,
                Data = expectedCountry,
            };
            var expectedResponseState = new ServiceResponse<IEnumerable<StateContactbookViewModel>>
            {
                Success = true,
                Data = expectedState,
            };
            var viewModel = new AddContactbookViewModel {
                Name = "Name" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
                .Returns(expectedResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
               .Returns(expectedResponseState);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };
            target.ModelState.AddModelError("LastName", "Last name is required.");

            //Act
            var actual = target.CreateContact(viewModel) as ViewResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(viewModel, actual.Model);
            Assert.False(target.ModelState.IsValid);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void Create_ReturnsViewResult_WithEmptyCountryandStateList_WhenModelStateIsInvalid()
        {
            //Arrange

            var expectedResponse = new ServiceResponse<IEnumerable<CountryContactbookViewModel>>
            {
                Success = false,
            };
            var expectedResponseState = new ServiceResponse<IEnumerable<StateContactbookViewModel>>
            {
                Success = false,
            };
            var viewModel = new AddContactbookViewModel {Name = "Name" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
                .Returns(expectedResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
              .Returns(expectedResponseState);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };
            target.ModelState.AddModelError("LastName", "Last name is required.");

            //Act
            var actual = target.CreateContact(viewModel) as ViewResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(viewModel, actual.Model);
            Assert.False(target.ModelState.IsValid);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }

        //Details
        [Fact]
        public void Details_ReturnsViewResult_WhenDetailsObtainedSuccessfully()
        {
            //Arrange
            var contactId = 1;
            var expectedContacts = new ContactbookViewModel { ContactId = 1, Name = "Name 1" };

            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");

            var expectedServiceResponse = new ServiceResponse<ContactbookViewModel>
            {
                Success = true,
                Data = expectedContacts,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Details(contactId) as ViewResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }
        [Fact]
        public void Details_ReturnsRedirectToActionResult_WhenSuccessIsFalse()
        {
            //Arrange
            var contactId = 1;
            var expectedContacts = new ContactbookViewModel { ContactId = 1, Name = "Name 1" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var errorMessage = "Error Occured";
            var expectedServiceResponse = new ServiceResponse<ContactbookViewModel>
            {
                Success = false,
                Data = expectedContacts,
                Message = errorMessage,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Details(contactId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }
        [Fact]
        public void Details_ReturnsRedirectToActionResult_WhenDataIsNull()
        {
            //Arrange
            var contactId = 1;
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var errorMessage = "Error Occured";
            var expectedServiceResponse = new ServiceResponse<ContactbookViewModel>
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
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Details(contactId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }
        [Fact]
        public void Details_ReturnsRedirectToActionResult_WhenStatusCodeIsBadRequest_ErrorResponseIsNotNull()
        {
            //Arrange
            var contactId = 1;
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var errorMessage = "Error Occured";
            var expectedServiceResponse = new ServiceResponse<ContactbookViewModel>
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
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Details(contactId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }
        [Fact]
        public void Details_ReturnsRedirectToActionResult_WhenStatusCodeIsBadRequest_ErrorResponseIsNull()
        {
            //Arrange
            var contactId = 1;
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(null))
            };
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Details(contactId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal("Something went wrong.Please try after sometime.", target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }

        //Edit Get
        [Fact]
        public void Edit_ReturnsViewResult_WhenContactDetailsObtainedSuccessfully_WithCountryAndStateList()
        {
            //Arrange
            var expectedCountry = new List<CountryContactbookViewModel>
            {
                new CountryContactbookViewModel {CountryId = 1,CountryName = "Country 1"},
                new CountryContactbookViewModel {CountryId = 2,CountryName = "Country 2"},

            };
            var expectedState = new List<StateContactbookViewModel>
            {
                new StateContactbookViewModel {StateId=1,CountryId = 1,StateName = "State 1"},
                new StateContactbookViewModel {StateId=2,CountryId = 2,StateName = "State 2"},

            };

            var contactId = 1;
            var expectedProducts = new UpdateContactbookViewModel { ContactId = 1, Name = "Name 1" };

            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");

            var expectedResponseForCountryContactbook = new ServiceResponse<IEnumerable<CountryContactbookViewModel>>
            {
                Success = true,
                Data = expectedCountry,
            };
            var expectedResponseForStateContactbook = new ServiceResponse<IEnumerable<StateContactbookViewModel>>
            {
                Success = true,
                Data = expectedState,
            };
            var expectedServiceResponse = new ServiceResponse<UpdateContactbookViewModel>
            {
                Success = true,
                Data = expectedProducts,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<UpdateContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
                .Returns(expectedResponseForCountryContactbook);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
              .Returns(expectedResponseForStateContactbook);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Edit(contactId) as ViewResult;
            var model = actual.Model as UpdateContactbookViewModel;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.NotNull(model);
            Assert.NotNull(model.CountryContactbook);
            Assert.NotNull(model.StateContactbook);
            Assert.Equal(expectedCountry.Count, model.CountryContactbook.Count());
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<UpdateContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void Edit_ReturnsViewResult_WhenContactDetailsObtainedSuccessfully_WithCountryContactbookEmptyList()
        {
            //Arrange
            var contactId = 1;
            var expectedContacts = new UpdateContactbookViewModel { ContactId = 1, Name = "Name 1" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");

            var expectedResponseForCountryContactbook = new ServiceResponse<IEnumerable<CountryContactbookViewModel>>
            {
                Success = false,
            };
            var expectedResponseForState = new ServiceResponse<IEnumerable<StateContactbookViewModel>>
            {
                Success = false,
            };
            var expectedServiceResponse = new ServiceResponse<UpdateContactbookViewModel>
            {
                Success = true,
                Data = expectedContacts,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<UpdateContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
                .Returns(expectedResponseForCountryContactbook);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
              .Returns(expectedResponseForState);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Edit(contactId) as ViewResult;
            var model = actual.Model as UpdateContactbookViewModel;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.NotNull(model);
            Assert.Empty(model.CountryContactbook);
            Assert.Empty(model.StateContactbook);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<UpdateContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void Edit_ReturnsRedirectToActionResult_WhenSuccessIsFalse()
        {
            //Arrange
            var contactId = 1;
            var expectedProducts = new UpdateContactbookViewModel { ContactId = 1, Name = "Name 1" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var errorMessage = "Error Occured";
            var expectedServiceResponse = new ServiceResponse<UpdateContactbookViewModel>
            {
                Success = false,
                Data = expectedProducts,
                Message = errorMessage,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<UpdateContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Edit(contactId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<UpdateContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }
        [Fact]
        public void Edit_ReturnsRedirectToActionResult_WhenDataIsNull()
        {
            //Arrange

            var contactId = 1;
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var errorMessage = "Error Occured";
            var expectedServiceResponse = new ServiceResponse<UpdateContactbookViewModel>
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
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<UpdateContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Edit(contactId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<UpdateContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }
        [Fact]
        public void Edit_ReturnsRedirectToActionResult_WhenStatusCodeIsBadRequest_ErrorResponseIsNotNull()
        {
            //Arrange
            var contactId = 1;
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var errorMessage = "Error Occured";
            var expectedServiceResponse = new ServiceResponse<UpdateContactbookViewModel>
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
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<UpdateContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Edit(contactId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<UpdateContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }
        [Fact]
        public void Edit_ReturnsRedirectToActionResult_WhenStatusCodeIsBadRequest_ErrorResponseIsNull()
        {
            //Arrange
            var contactId = 1;
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(null))
            };
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<UpdateContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Edit(contactId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal("Something went wrong.Please try after sometime.", target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<UpdateContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }

        //Edit Post
        [Fact]
        public void Edit_RedirectToActionResult_WhenContactsUpdatedSuccessfully()
        {
            //Arrange
            var expectedCountry = new List<CountryContactbookViewModel>
            {
                new CountryContactbookViewModel {CountryId = 1,CountryName = "Country 1"},
                new CountryContactbookViewModel {CountryId = 2,CountryName = "Country 2"},

            };
            var expectedState = new List<StateContactbookViewModel>
            {
                new StateContactbookViewModel {StateId=1,CountryId = 1,StateName = "State 1"},
                new StateContactbookViewModel {StateId=2,CountryId = 2,StateName = "State 2"},

            };
            var viewModel = new UpdateContactbookViewModel { ContactId = 1, Name = "Name 1", StateId = 1, CountryId = 1, StateContactbook = expectedState, CountryContactbook = expectedCountry };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var successMessage = "Contact Updated Successfully";
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Message = successMessage,
            };
            var expectedCountryResponse = new ServiceResponse<IEnumerable<CountryContactbookViewModel>>
            {
                Success = true,
                Data = expectedCountry,
            };
            var expectedStateResponse = new ServiceResponse<IEnumerable<StateContactbookViewModel>>
            {
                Success = true,
                Data = expectedState,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.PutHttpResponseMessage(It.IsAny<string>(), viewModel, It.IsAny<HttpRequest>())).Returns(expectedResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
             .Returns(expectedCountryResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
              .Returns(expectedStateResponse);
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Edit(viewModel) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal(successMessage, target.TempData["SuccessMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PutHttpResponseMessage(It.IsAny<string>(), viewModel, It.IsAny<HttpRequest>()), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
      
        [Fact]
        public void Edit_ReturnsViewResultWithErrorMessage_WhenResponseIsNotSuccess()
        {
            //Arrange
            var expectedCountry = new List<CountryContactbookViewModel>
            {
                new CountryContactbookViewModel {CountryId = 1,CountryName = "Country 1"},
                new CountryContactbookViewModel {CountryId = 2,CountryName = "Country 2"},

            };
            var expectedState = new List<StateContactbookViewModel>
            {
                new StateContactbookViewModel {StateId=1,CountryId = 1,StateName = "State 1"},
                new StateContactbookViewModel {StateId=2,CountryId = 2,StateName = "State 2"},

            };
            var viewModel = new UpdateContactbookViewModel { ContactId = 1, Name = "Name 1", StateId = 1, CountryId = 1, StateContactbook = expectedState, CountryContactbook = expectedCountry };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var errorMessage = "Error Occured";
            var expectedErrorResponse = new ServiceResponse<string>
            {
                Message = errorMessage,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedErrorResponse))
            };
            var expectedCountryResponse = new ServiceResponse<IEnumerable<CountryContactbookViewModel>>
            {
                Success = true,
                Data = expectedCountry,
            };
            var expectedStateResponse = new ServiceResponse<IEnumerable<StateContactbookViewModel>>
            {
                Success = true,
                Data = expectedState,
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.PutHttpResponseMessage(It.IsAny<string>(), viewModel, It.IsAny<HttpRequest>())).Returns(expectedResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
             .Returns(expectedCountryResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
              .Returns(expectedStateResponse);
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Edit(viewModel) as ViewResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PutHttpResponseMessage(It.IsAny<string>(), viewModel, It.IsAny<HttpRequest>()), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void Edit_ReturnsRedirectToActionResult_WhenResponseIsNotSuccess()
        {
            //Arrange
            var expectedCountry = new List<CountryContactbookViewModel>
            {
                new CountryContactbookViewModel {CountryId = 1,CountryName = "Country 1"},
                new CountryContactbookViewModel {CountryId = 2,CountryName = "Country 2"},

            };
            var expectedState = new List<StateContactbookViewModel>
            {
                new StateContactbookViewModel {StateId=1,CountryId = 1,StateName = "State 1"},
                new StateContactbookViewModel {StateId=2,CountryId = 2,StateName = "State 2"},

            };
            var viewModel = new UpdateContactbookViewModel { ContactId = 1, Name = "Name 1", StateId = 1, CountryId = 1, StateContactbook = expectedState, CountryContactbook = expectedCountry };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var errorMessage = "Something went wrong.Please try after sometime.";
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(null))
            };
            var expectedCountryResponse = new ServiceResponse<IEnumerable<CountryContactbookViewModel>>
            {
                Success = true,
                Data = expectedCountry,
            };
            var expectedStateResponse = new ServiceResponse<IEnumerable<StateContactbookViewModel>>
            {
                Success = true,
                Data = expectedState,
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.PutHttpResponseMessage(It.IsAny<string>(), viewModel, It.IsAny<HttpRequest>())).Returns(expectedResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
             .Returns(expectedCountryResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
              .Returns(expectedStateResponse);
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Edit(viewModel) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.PutHttpResponseMessage(It.IsAny<string>(), viewModel, It.IsAny<HttpRequest>()), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void Edit_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            //Arrange
            var expectedCountry = new List<CountryContactbookViewModel>
            {
                new CountryContactbookViewModel {CountryId = 1,CountryName = "Country 1"},
                new CountryContactbookViewModel {CountryId = 2,CountryName = "Country 2"},

            };
            var expectedState = new List<StateContactbookViewModel>
            {
                new StateContactbookViewModel {StateId=1,CountryId = 1,StateName = "State 1"},
                new StateContactbookViewModel {StateId=2,CountryId = 2,StateName = "State 2"},

            };
            var expectedCountryResponse = new ServiceResponse<IEnumerable<CountryContactbookViewModel>>
            {
                Success = true,
                Data = expectedCountry,
            };
            var expectedStateResponse = new ServiceResponse<IEnumerable<StateContactbookViewModel>>
            {
                Success = true,
                Data = expectedState,
            };
            var viewModel = new UpdateContactbookViewModel { ContactId = 1, Name = "Name 1", StateId = 1, CountryId = 1, StateContactbook = expectedState, CountryContactbook = expectedCountry };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();

            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
             .Returns(expectedCountryResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
              .Returns(expectedStateResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };
            target.ModelState.AddModelError("ProductDescription", "Product description is required.");

            //Act
            var actual = target.Edit(viewModel) as ViewResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(viewModel, actual.Model);
            Assert.False(target.ModelState.IsValid);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }
        [Fact]
        public void Edit_ReturnsViewResult_WhenModelStateIsInvalid_WhenStateAndCountryIsNull()
        {
            //Arrange
            var expectedCountry = new List<CountryContactbookViewModel> { };
            var expectedState = new List<StateContactbookViewModel> { };
            var expectedCountryResponse = new ServiceResponse<IEnumerable<CountryContactbookViewModel>>
            {
                Success = false,
            };
            var expectedStateResponse = new ServiceResponse<IEnumerable<StateContactbookViewModel>>
            {
                Success = false,
            };
            var viewModel = new UpdateContactbookViewModel { ContactId = 1, Name = "Name 1", StateId = 1, CountryId = 1, StateContactbook = expectedState, CountryContactbook = expectedCountry };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var mockHttpContext = new Mock<HttpContext>();

            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
             .Returns(expectedCountryResponse);
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60))
              .Returns(expectedStateResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };
            target.ModelState.AddModelError("ProductDescription", "Product description is required.");

            //Act
            var actual = target.Edit(viewModel) as ViewResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(viewModel, actual.Model);
            Assert.False(target.ModelState.IsValid);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>(It.IsAny<string>(), HttpMethod.Get, It.IsAny<HttpRequest>(), null, 60), Times.Once);
        }


        //Delete
        [Fact]
        public void Delete_ReturnsViewResult_WhenDetailsObtainedSuccessfully()
        {
            //Arrange
            var contactId = 1;
            var expectedContacts = new ContactbookViewModel { ContactId = 1, Name = "Name 1" };

            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");

            var expectedServiceResponse = new ServiceResponse<ContactbookViewModel>
            {
                Success = true,
                Data = expectedContacts,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Delete(contactId) as ViewResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }
        [Fact]
        public void Delete_ReturnsRedirectToActionResult_WhenSuccessIsFalse()
        {
            //Arrange
            var contactId = 1;
            var expectedContacts = new ContactbookViewModel { ContactId = 1, Name = "Name 1" };
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var errorMessage = "Error Occured";
            var expectedServiceResponse = new ServiceResponse<ContactbookViewModel>
            {
                Success = false,
                Data = expectedContacts,
                Message = errorMessage,
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedServiceResponse))
            };
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Delete(contactId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }
        [Fact]
        public void Delete_ReturnsRedirectToActionResult_WhenDataIsNull()
        {
            //Arrange
            var contactId = 1;
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var errorMessage = "Error Occured";
            var expectedServiceResponse = new ServiceResponse<ContactbookViewModel>
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
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Delete(contactId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }
        [Fact]
        public void Delete_ReturnsRedirectToActionResult_WhenStatusCodeIsBadRequest_ErrorResponseIsNotNull()
        {
            //Arrange
            var contactId = 1;
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var errorMessage = "Error Occured";
            var expectedServiceResponse = new ServiceResponse<ContactbookViewModel>
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
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Delete(contactId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal(errorMessage, target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }
        [Fact]
        public void Delete_ReturnsRedirectToActionResult_WhenStatusCodeIsBadRequest_ErrorResponseIsNull()
        {
            //Arrange
            var contactId = 1;
            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("FakeEndPoint");
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(null))
            };
            var mockTempDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>())).Returns(expectedResponse);
            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            //Act
            var actual = target.Delete(contactId) as RedirectToActionResult;

            //Assert
            Assert.NotNull(actual);
            Assert.True(target.ModelState.IsValid);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal("Something went wrong. Please try after sometime.", target.TempData["ErrorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.GetHttpResponseMessage<ContactbookViewModel>(It.IsAny<string>(), It.IsAny<HttpRequest>()), Times.Once);
        }



        //DeleteConfirmed
        [Fact]
        public void DeleteConfirm_ReturnsRedirectToAction_WhenDeletedSuccessfully()
        {
            // Arrange
            var id = 1;

            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            //var mockImage = new Mock<IImageUpload>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Message = "Success",
                Success = true
            };

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<string>>(It.IsAny<string>(), HttpMethod.Delete, It.IsAny<HttpRequest>(), null, 60)).Returns(expectedServiceResponse);
            var mockDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockDataProvider.Object);

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object
                },

            };
            // Act
            var actual = target.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal(expectedServiceResponse.Message, target.TempData["successMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<string>>(It.IsAny<string>(), HttpMethod.Delete, It.IsAny<HttpRequest>(), null, 60), Times.Once);

        }
        [Fact]
        public void DeleteConfirm_ReturnsRedirectToAction_WhenDeletionFailed()
        {
            // Arrange
            var id = 1;

            var mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(c => c["Endpoint:CivicaContactApi"]).Returns("fakeEndPoint");
            var expectedServiceResponse = new ServiceResponse<string>
            {
                Message = "Error",
                Success = false
            };

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpClientService.Setup(c => c.ExecuteApiRequest<ServiceResponse<string>>(It.IsAny<string>(), HttpMethod.Delete, It.IsAny<HttpRequest>(), null, 60)).Returns(expectedServiceResponse);
            var mockDataProvider = new Mock<ITempDataProvider>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), mockDataProvider.Object);

            var target = new ContactbookController(mockHttpClientService.Object, mockConfiguration.Object)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object
                },

            };
            // Act
            var actual = target.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal("ShowAllContactWithPagination", actual.ActionName);
            Assert.Equal(expectedServiceResponse.Message, target.TempData["errorMessage"]);
            mockConfiguration.Verify(c => c["Endpoint:CivicaContactApi"], Times.Once);
            mockHttpClientService.Verify(c => c.ExecuteApiRequest<ServiceResponse<string>>(It.IsAny<string>(), HttpMethod.Delete, It.IsAny<HttpRequest>(), null, 60), Times.Once);

        }

    }
}
