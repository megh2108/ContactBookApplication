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
using System.Text;
using System.Threading.Tasks;

namespace ApiContactbookApplicationTests.Controllers
{
    public class ContactbookContollerTest
    {

        //GetAllContacts
        [Fact]
        public void GetAllContacts_ReturnsOkWithContacts()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string letter = "d";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = true,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetAllContacts()).Returns(response);

            //Act
            var actual = target.GetAllContacts() as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetAllContacts(), Times.Once);
        }

        [Fact]
        public void GetAllContacts_ReturnsNotFound()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string letter = "d";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = false,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetAllContacts()).Returns(response);

            //Act
            var actual = target.GetAllContacts() as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetAllContacts(), Times.Once);
        }

        //GetAllFavouriteContacts
        [Fact]
        public void GetAllFavouriteContacts_ReturnsOkWithContacts()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890",
                    Favourite = true},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string letter = "d";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = true,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetAllFavouriteContacts()).Returns(response);

            //Act
            var actual = target.GetAllFavouriteContacts() as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetAllFavouriteContacts(), Times.Once);
        }

        [Fact]
        public void GetAllFavouriteContacts_ReturnsNotFound()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890",
                    Favourite = true},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string letter = "d";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = false,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetAllFavouriteContacts()).Returns(response);

            //Act
            var actual = target.GetAllFavouriteContacts() as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetAllFavouriteContacts(), Times.Once);
        }

        //GetAllSpecificContacts

        [Fact]
        public void GetAllSpecificContact_ReturnsOkWithContacts()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string letter = "d";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = true,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetAllSpecificContact(letter)).Returns(response);

            //Act
            var actual = target.GetAllSpecificContact(letter) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetAllSpecificContact(letter), Times.Once);
        }

        [Fact]
        public void GetAllSpecificContact_ReturnsNotFound()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string letter = "d";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = false,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetAllSpecificContact(letter)).Returns(response);

            //Act
            var actual = target.GetAllSpecificContact(letter) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetAllSpecificContact(letter), Times.Once);
        }

        //GetAllContactsCount
        [Fact]
        public void GetContactsCount_ReturnsOkWithContacts_WhenSearchIsNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            var response = new ServiceResponse<int>
            {
                Success = true,
                Data = contacts.Count
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.TotalContacts(null)).Returns(response);

            //Act
            var actual = target.GetContactsCount(null) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            Assert.Equal(2, response.Data);
            mockContactService.Verify(c => c.TotalContacts(null), Times.Once);
        }

        [Fact]
        public void GetContactsCount_ReturnsOkWithContacts_WhenSearchIsNotNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            string search = "dev";

            var response = new ServiceResponse<int>
            {
                Success = true,
                Data = contacts.Count
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.TotalContacts(search)).Returns(response);

            //Act
            var actual = target.GetContactsCount(search) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            Assert.Equal(2, response.Data);
            mockContactService.Verify(c => c.TotalContacts(search), Times.Once);
        }

        [Fact]
        public void GetContactsCount_ReturnsNotFound_WhenSearchIsNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto> { };

            var response = new ServiceResponse<int>
            {
                Success = false,
                Data = contacts.Count
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.TotalContacts(null)).Returns(response);

            //Act
            var actual = target.GetContactsCount(null) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            Assert.Equal(0, response.Data);
            mockContactService.Verify(c => c.TotalContacts(null), Times.Once);
        }

        [Fact]
        public void GetContactsCount_ReturnsNotFound_WhenSearchIsNotNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto> { };

            string search = "dev";

            var response = new ServiceResponse<int>
            {
                Success = false,
                Data = contacts.Count
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.TotalContacts(search)).Returns(response);

            //Act
            var actual = target.GetContactsCount(search) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            Assert.Equal(0, response.Data);
            mockContactService.Verify(c => c.TotalContacts(search), Times.Once);
        }

        //GetAllContactsByPagination
        [Fact]
        public void GetAllContactsByPagination_ReturnsOkWithContacts_SearchIsNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = true,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetPaginatedContacts(page, pageSize, sortOrder, null)).Returns(response);

            //Act
            var actual = target.GetAllContactsByPagination(null, page, pageSize, sortOrder) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetPaginatedContacts(page, pageSize, sortOrder, null), Times.Once);
        }

        [Fact]
        public void GetAllContactsByPagination_ReturnsOkWithContacts_SearchIsNotNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string search = "dev";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = true,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetPaginatedContacts(page, pageSize, sortOrder, search)).Returns(response);

            //Act
            var actual = target.GetAllContactsByPagination(search, page, pageSize, sortOrder) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetPaginatedContacts(page, pageSize, sortOrder, search), Times.Once);
        }

        [Fact]
        public void GetAllContactsByPagination_ReturnsNotFound_SearchIsNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = false,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetPaginatedContacts(page, pageSize, sortOrder, null)).Returns(response);

            //Act
            var actual = target.GetAllContactsByPagination(null, page, pageSize, sortOrder) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetPaginatedContacts(page, pageSize, sortOrder, null), Times.Once);
        }

        [Fact]
        public void GetAllContactsByPagination_ReturnsNotFound_SearchIsNotNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string search = "dev";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = false,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetPaginatedContacts(page, pageSize, sortOrder, search)).Returns(response);

            //Act
            var actual = target.GetAllContactsByPagination(search, page, pageSize, sortOrder) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetPaginatedContacts(page, pageSize, sortOrder, search), Times.Once);
        }

        //GetSpecificContactsCount

        [Fact]
        public void GetSpecificContactsCount_ReturnsOkWithContacts_WhenSearchIsNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            string letter = "d";
            var response = new ServiceResponse<int>
            {
                Success = true,
                Data = contacts.Count
            };


            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.TotalSpecificContacts(letter, null)).Returns(response);

            //Act
            var actual = target.GetSpecificContactsCount(letter, null) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            Assert.Equal(2, response.Data);
            mockContactService.Verify(c => c.TotalSpecificContacts(letter, null), Times.Once);
        }

        [Fact]
        public void GetSpecificContactsCount_ReturnsOkWithContacts_WhenSearchIsNotNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            string search = "dev";
            string letter = "d";


            var response = new ServiceResponse<int>
            {
                Success = true,
                Data = contacts.Count
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.TotalSpecificContacts(letter, search)).Returns(response);

            //Act
            var actual = target.GetSpecificContactsCount(letter, search) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            Assert.Equal(2, response.Data);
            mockContactService.Verify(c => c.TotalSpecificContacts(letter, search), Times.Once);
        }

        [Fact]
        public void GetSpecificContactsCount_ReturnsNotFound_WhenSearchIsNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto> { };

            string letter = "d";

            var response = new ServiceResponse<int>
            {
                Success = false,
                Data = contacts.Count
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.TotalSpecificContacts(letter, null)).Returns(response);

            //Act
            var actual = target.GetSpecificContactsCount(letter, null) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            Assert.Equal(0, response.Data);
            mockContactService.Verify(c => c.TotalSpecificContacts(letter, null), Times.Once);
        }

        [Fact]
        public void GetSpecificContactsCount_ReturnsNotFound_WhenSearchIsNotNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto> { };

            string search = "dev";
            string letter = "d";


            var response = new ServiceResponse<int>
            {
                Success = false,
                Data = contacts.Count
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.TotalSpecificContacts(letter, search)).Returns(response);

            //Act
            var actual = target.GetSpecificContactsCount(letter, search) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            Assert.Equal(0, response.Data);
            mockContactService.Verify(c => c.TotalSpecificContacts(letter, search), Times.Once);
        }

        // GetSpecificContactsByPaginationWithLetter

        [Fact]
        public void GetSpecificContactsByPaginationWithLetter_ReturnsOkWithSpecificLetteContacts_SearchIsNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string letter = "d";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = true,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, null)).Returns(response);

            //Act
            var actual = target.GetSpecificContactsByPaginationWithLetter(null, page, pageSize, letter, sortOrder) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, null), Times.Once);
        }

        [Fact]
        public void GetSpecificContactsByPaginationWithLetter_ReturnsOkWithSpecificLetteContacts_SearchIsNotNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string search = "dev";
            string letter = "d";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = true,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, search)).Returns(response);

            //Act
            var actual = target.GetSpecificContactsByPaginationWithLetter(search, page, pageSize, letter, sortOrder) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, search), Times.Once);
        }

        [Fact]
        public void GetSpecificContactsByPaginationWithLetter_ReturnsNotFoundSpecificLette_SearchIsNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
    {
        new ContactbookDto{
            ContactId=1,
            Name="Contact 1",
            PhoneNumber = "1234567890"},
        new ContactbookDto{
            ContactId=2,
            Name="Contact 2",
            PhoneNumber = "3234567890"},
    };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string letter = "d";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = false,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, null)).Returns(response);

            //Act
            var actual = target.GetSpecificContactsByPaginationWithLetter(null, page, pageSize, letter, sortOrder) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, null), Times.Once);
        }


        [Fact]
        public void GetSpecificContactsByPaginationWithLetter_ReturnsNotFoundSpecificLette_SearchIsNotNull()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string search = "dev";
            string letter = "d";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = false,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, search)).Returns(response);

            //Act
            var actual = target.GetSpecificContactsByPaginationWithLetter(search, page, pageSize, letter, sortOrder) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, search), Times.Once);
        }

        //GetFavouriteContactsCount

        [Fact]
        public void GetFavouriteContactsCount_ReturnsOkWithContacts()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            var response = new ServiceResponse<int>
            {
                Success = true,
                Data = contacts.Count
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.TotalFavouriteContacts()).Returns(response);

            //Act
            var actual = target.GetFavouriteContactsCount() as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            Assert.Equal(2, response.Data);
            mockContactService.Verify(c => c.TotalFavouriteContacts(), Times.Once);
        }

        [Fact]
        public void GetFavouriteContactsCount_ReturnsNotFound()
        {
            //Arrange
            var contacts = new List<ContactbookDto> { };

            var response = new ServiceResponse<int>
            {
                Success = false,
                Data = contacts.Count
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.TotalFavouriteContacts()).Returns(response);

            //Act
            var actual = target.GetFavouriteContactsCount() as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            Assert.Equal(0, response.Data);
            mockContactService.Verify(c => c.TotalFavouriteContacts(), Times.Once);
        }


        // GetAllFavouriteContactsByPagination
        [Fact]
        public void GetAllFavouriteContactsByPagination_ReturnsOkWithContacts()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = true,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetPaginatedFavouriteContacts(page, pageSize, sortOrder)).Returns(response);

            //Act
            var actual = target.GetAllFavouriteContactsByPagination(page, pageSize, sortOrder) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetPaginatedFavouriteContacts(page, pageSize, sortOrder), Times.Once);
        }

        [Fact]
        public void GetAllFavouriteContactsByPagination_ReturnsNotFound()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = false,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetPaginatedFavouriteContacts(page, pageSize, sortOrder)).Returns(response);

            //Act
            var actual = target.GetAllFavouriteContactsByPagination(page, pageSize, sortOrder) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetPaginatedFavouriteContacts(page, pageSize, sortOrder), Times.Once);
        }

        //GetSpecificFavouriteContactsCount


        [Fact]
        public void GetSpecificFavouriteContactsCount_ReturnsOkWithContacts()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            string letter = "d";
            var response = new ServiceResponse<int>
            {
                Success = true,
                Data = contacts.Count
            };


            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.TotalFavouriteSpecificContacts(letter)).Returns(response);

            //Act
            var actual = target.GetSpecificFavouriteContactsCount(letter) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            Assert.Equal(2, response.Data);
            mockContactService.Verify(c => c.TotalFavouriteSpecificContacts(letter), Times.Once);
        }

        [Fact]
        public void GetSpecificFavouriteContactsCount_ReturnsNotFound()
        {
            //Arrange
            var contacts = new List<ContactbookDto> { };

            string letter = "d";

            var response = new ServiceResponse<int>
            {
                Success = false,
                Data = contacts.Count
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.TotalFavouriteSpecificContacts(letter)).Returns(response);

            //Act
            var actual = target.GetSpecificFavouriteContactsCount(letter) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            Assert.Equal(0, response.Data);
            mockContactService.Verify(c => c.TotalFavouriteSpecificContacts(letter), Times.Once);
        }

        //GetSpecificFavouriteContactsByPaginationWithLetter

        [Fact]
        public void GetSpecificFavouriteContactsByPaginationWithLetter_ReturnsOkWithContacts()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string letter = "d";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = true,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetPaginatedFavouriteContactsWithLetter(page, pageSize, letter, sortOrder)).Returns(response);

            //Act
            var actual = target.GetSpecificFavouriteContactsByPaginationWithLetter(page, pageSize, letter, sortOrder) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetPaginatedFavouriteContactsWithLetter(page, pageSize, letter, sortOrder), Times.Once);
        }

        [Fact]
        public void GetSpecificFavouriteContactsByPaginationWithLetter_ReturnsNotFound()
        {
            //Arrange
            var contacts = new List<ContactbookDto>
            {
               new ContactbookDto{
                   ContactId=1,
                   Name="Contact 1",
                   PhoneNumber = "1234567890"},
                new ContactbookDto{
                   ContactId=2,
                   Name="Contact 2",
                   PhoneNumber = "3234567890"},

             };

            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string letter = "d";
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = false,
                Data = contacts
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetPaginatedFavouriteContactsWithLetter(page, pageSize, letter, sortOrder)).Returns(response);

            //Act
            var actual = target.GetSpecificFavouriteContactsByPaginationWithLetter(page, pageSize, letter, sortOrder) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetPaginatedFavouriteContactsWithLetter(page, pageSize, letter, sortOrder), Times.Once);
        }
        //get contacts by id

        [Fact]

        public void GetContactById_ReturnsOk()
        {

            var contactId = 1;
            var contact = new Contactbook
            {

                ContactId = contactId,
                Name = "Contact 1"
            };

            var response = new ServiceResponse<ContactbookDto>
            {
                Success = true,
                Data = new ContactbookDto
                {
                    ContactId = contactId,
                    Name = contact.Name
                }
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetContactsById(contactId)).Returns(response);

            //Act
            var actual = target.GetContactsById(contactId) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetContactsById(contactId), Times.Once);
        }

        [Fact]

        public void GetContactById_ReturnsNotFound()
        {

            var contactId = 1;
            var contact = new Contactbook
            {

                ContactId = contactId,
                Name = "Contact 1"
            };

            var response = new ServiceResponse<ContactbookDto>
            {
                Success = false,
                Data = new ContactbookDto
                {
                    ContactId = contactId,
                    Name = contact.Name
                }
            };

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.GetContactsById(contactId)).Returns(response);

            //Act
            var actual = target.GetContactsById(contactId) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.GetContactsById(contactId), Times.Once);
        }

        [Fact]
        public void AddContact_ReturnsOk_WhenContactIsAddedSuccessfully()
        {
            var AddContactbookDto = new AddContactbookDto
            {
                Name = "John Doe",
                Email = "test@example.com",
                PhoneNumber = "123-456-7890",
                Company = "Example Company",
                Gender = "Male",
                CountryId = 1,
                StateId = 1,
                Favourite = false,
                BirthDate = DateTime.Now
            };
            var response = new ServiceResponse<string>
            {
                Success = true,
            };
            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.AddContact(It.IsAny<Contactbook>())).Returns(response);

            //Act

            var actual = target.AddContact(AddContactbookDto) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.AddContact(It.IsAny<Contactbook>()), Times.Once);

        }

        [Fact]
        public void AddContact_ReturnsBadRequest_WhenContactIsNotAdded()
        {
            var AddContactbookDto = new AddContactbookDto
            {
                Name = "John Doe",
                Email = "test@example.com",
                PhoneNumber = "123-456-7890",
                Company = "Example Company",
                Gender = "Male",
                CountryId = 1,
                StateId = 1,
                Favourite = false,
                BirthDate = DateTime.Now
            };
            var response = new ServiceResponse<string>
            {
                Success = false,
            };
            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.AddContact(It.IsAny<Contactbook>())).Returns(response);

            //Act

            var actual = target.AddContact(AddContactbookDto) as BadRequestObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(400, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.AddContact(It.IsAny<Contactbook>()), Times.Once);

        }

        [Fact]
        public void UpdateContact_ReturnsOk_WhenContactIsUpdatesSuccessfully()
        {
            var UpdateContactbookDto = new UpdateContactbookDto
            {
                ContactId = 1,
                Name = "John Doe",
                Email = "test@example.com",
                PhoneNumber = "123-456-7890",
                Company = "Example Company",
                Gender = "Male",
                CountryId = 1,
                StateId = 1,
                Favourite = false,
                BirthDate = DateTime.Now
            };
            var response = new ServiceResponse<string>
            {
                Success = true,
            };
            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.ModifyContact(It.IsAny<Contactbook>())).Returns(response);

            //Act

            var actual = target.UpdateContact(UpdateContactbookDto) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.ModifyContact(It.IsAny<Contactbook>()), Times.Once);

        }

        [Fact]
        public void UpdateContact_ReturnsBadRequest_WhenContactIsNotUpdated()
        {
            var UpdateContactbookDto = new UpdateContactbookDto
            {
                ContactId = 1,
                Name = "John Doe",
                Email = "test@example.com",
                PhoneNumber = "123-456-7890",
                Company = "Example Company",
                Gender = "Male",
                CountryId = 1,
                StateId = 1,
                Favourite = false,
                BirthDate = DateTime.Now
            };

            var response = new ServiceResponse<string>
            {
                Success = false,
            };
            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.ModifyContact(It.IsAny<Contactbook>())).Returns(response);

            //Act

            var actual = target.UpdateContact(UpdateContactbookDto) as BadRequestObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(400, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.ModifyContact(It.IsAny<Contactbook>()), Times.Once);

        }

        [Fact]
        public void RemoveContact_ReturnsOkResponse_WhenContactDeletedSuccessfully()
        {

            var contactId = 1;
            var response = new ServiceResponse<string>
            {
                Success = true,
            };
            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.RemoveContact(contactId)).Returns(response);

            //Act

            var actual = target.RemoveContact(contactId) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.RemoveContact(contactId), Times.Once);
        }

        [Fact]
        public void RemoveContact_ReturnsBadRequest_WhenContactNotDeleted()
        {

            var contactId = 1;
            var response = new ServiceResponse<string>
            {
                Success = false,
            };
            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);
            mockContactService.Setup(c => c.RemoveContact(contactId)).Returns(response);

            //Act

            var actual = target.RemoveContact(contactId) as BadRequestObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(400, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal(response, actual.Value);
            mockContactService.Verify(c => c.RemoveContact(contactId), Times.Once);
        }

        [Fact]
        public void RemoveContact_ReturnsBadRequest_WhenContactIsLessThanZero()
        {

            var contactId = 0;

            var mockContactService = new Mock<IContactbookService>();
            var target = new ContactbookController(mockContactService.Object);

            //Act

            var actual = target.RemoveContact(contactId) as BadRequestObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(400, actual.StatusCode);
            Assert.NotNull(actual.Value);
            Assert.Equal("Please enter proper data.", actual.Value);
        }

    }
}
