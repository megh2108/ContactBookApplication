using ApiContactbookApplication.Data;
using ApiContactbookApplication.Data.Implementation;
using ApiContactbookApplication.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiContactbookApplicationTests.Repsitories
{
    public class ContactbookRepositoryTest
    {

        //getallcontacts
        [Fact]
        public void GetAll_ReturnsContacts_WhenContactsExist()
        {
            // Arrange
            var contactsList = new List<Contactbook>
        {
            new Contactbook
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                Company = "Company",
                File = null,
                FileName = "file2.txt",
                BirthDate = null,
                Gender = "Male",
                Favourite = true,
                CountryId = 1,
                StateId = 1,
                Country = new Country
                {
                    CountryId = 1,
                    CountryName = "USA"
                },
                State = new State
                {
                    StateId = 1,
                    StateName = "California"
                }
            },
            new Contactbook
            {
                ContactId = 2,
                Name = "Jane",
                Email = "jane@example.com",
                PhoneNumber = "0987654321",
                Company = "Company",
                File = null,
                FileName = "file2.txt",
                BirthDate = null,
                Gender = "Female",
                Favourite = false,
                CountryId = 2,
                StateId = 2,
                Country = new Country
                {
                    CountryId = 2,
                    CountryName = "Canada"
                },
                State = new State
                {
                    StateId = 2,
                    StateName = "Ontario"
                }
            }
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(m => m.Provider).Returns(contactsList.Provider);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(m => m.Expression).Returns(contactsList.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(m => m.ElementType).Returns(contactsList.ElementType);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(m => m.GetEnumerator()).Returns(contactsList.GetEnumerator());

            var mockDbContext = new Mock<IAppDbContext>();
            mockDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);

            var target = new ContactbookRepository(mockDbContext.Object);

            // Act
            var actual = target.GetAll();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(contactsList.Count(), actual.Count());
            mockDbContext.Verify(c => c.Contactbooks, Times.Once);
        }

        //GetAllFavourite

        [Fact]
        public void GetAllFavourite_ReturnsContacts_WhenContactsExist()
        {
            // Arrange
            var contactsList = new List<Contactbook>
        {
            new Contactbook
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                Company = "Company",
                File = null,
                FileName = "file2.txt",
                BirthDate = null,
                Gender = "Male",
                Favourite = true,
                CountryId = 1,
                StateId = 1,
                Country = new Country
                {
                    CountryId = 1,
                    CountryName = "USA"
                },
                State = new State
                {
                    StateId = 1,
                    StateName = "California"
                }
            },
            new Contactbook
            {
                ContactId = 2,
                Name = "Jane",
                Email = "jane@example.com",
                PhoneNumber = "0987654321",
                Company = "Company",
                File = null,
                FileName = "file2.txt",
                BirthDate = null,
                Gender = "Female",
                Favourite = false,
                CountryId = 2,
                StateId = 2,
                Country = new Country
                {
                    CountryId = 2,
                    CountryName = "Canada"
                },
                State = new State
                {
                    StateId = 2,
                    StateName = "Ontario"
                }
            }
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(m => m.Provider).Returns(contactsList.Provider);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(m => m.Expression).Returns(contactsList.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(m => m.ElementType).Returns(contactsList.ElementType);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(m => m.GetEnumerator()).Returns(contactsList.GetEnumerator());

            var mockDbContext = new Mock<IAppDbContext>();
            mockDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);

            var target = new ContactbookRepository(mockDbContext.Object);

            // Act
            var actual = target.GetAllFavourite();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(1, actual.Count());
            mockDbContext.Verify(c => c.Contactbooks, Times.Once);
        }

        //GetSpecificContact

        [Fact]
        public void GetSpecificContact_ReturnsContacts_WhenContactsExist()
        {
            // Arrange

            string letter = "d";
            var contactsList = new List<Contactbook>
        {
            new Contactbook
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                Company = "Company",
                File = null,
                FileName = "file2.txt",
                BirthDate = null,
                Gender = "Male",
                Favourite = true,
                CountryId = 1,
                StateId = 1,
                Country = new Country
                {
                    CountryId = 1,
                    CountryName = "USA"
                },
                State = new State
                {
                    StateId = 1,
                    StateName = "California"
                }
            },
            new Contactbook
            {
                ContactId = 2,
                Name = "Jane",
                Email = "jane@example.com",
                PhoneNumber = "0987654321",
                Company = "Company",
                File = null,
                FileName = "file2.txt",
                BirthDate = null,
                Gender = "Female",
                Favourite = false,
                CountryId = 2,
                StateId = 2,
                Country = new Country
                {
                    CountryId = 2,
                    CountryName = "Canada"
                },
                State = new State
                {
                    StateId = 2,
                    StateName = "Ontario"
                }
            }
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(m => m.Provider).Returns(contactsList.Provider);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(m => m.Expression).Returns(contactsList.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(m => m.ElementType).Returns(contactsList.ElementType);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(m => m.GetEnumerator()).Returns(contactsList.GetEnumerator());

            var mockDbContext = new Mock<IAppDbContext>();
            mockDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);

            var target = new ContactbookRepository(mockDbContext.Object);

            // Act
            var actual = target.GetSpecificContact(letter);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(0, actual.Count());
            mockDbContext.Verify(c => c.Contactbooks, Times.Once);
        }


        //total contacts

        [Fact]
        public void TotalContacts_ReturnsCount_WhenContactsExistWhenSearchIsNotNull()
        {
            string searchQuery = "Con";
            var contacts = new List<Contactbook> {
                new Contactbook {ContactId = 1,Name="Contact 1"},
                new Contactbook {ContactId = 2,Name="Contact 2"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);

            //Act
            var actual = target.TotalContacts( searchQuery);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(2, actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockAppDbContext.Verify(c => c.Contactbooks, Times.Once);

        }

        [Fact]
        public void TotalContacts_ReturnsCount_WhenContactsExistWhenSearchIsNull()
        {
            string searchQuery = null;
            var contacts = new List<Contactbook> {
                new Contactbook {ContactId = 1,Name="Contact 1"},
                new Contactbook {ContactId = 2,Name="Contact 2"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);

            //Act
            var actual = target.TotalContacts(searchQuery);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(2, actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockAppDbContext.Verify(c => c.Contactbooks, Times.Once);

        }
        //getpaginatedcontacts

        [Fact]
        public void GetPaginatedContacts_ReturnsCorrectContacts_WhenContactsExists_SearchIsNull()
        {
            string searchQuery = null;
            string sortOrder = "asc";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                  new Contactbook{ContactId=1, Name="Contact 1"},
                  new Contactbook{ContactId=2, Name="Contact 2"},
                  new Contactbook{ContactId=3, Name="Contact 3"},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            //Act
            var actual = target.GetPaginatedContacts(page, pageSize, sortOrder, searchQuery);
            //Assert
            Assert.NotNull(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
        }


        [Fact]
        public void GetPaginatedContacts_ReturnsCorrectContacts_WhenContactsExists_SearchIsNotNull()
        {
            string searchQuery = "search";
            string sortOrder = "asc";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                  new Contactbook{ContactId=1, Name="Contact 1"},
                  new Contactbook{ContactId=2, Name="Contact 2"},
                  new Contactbook{ContactId=3, Name="Contact 3"},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            //Act
            var actual = target.GetPaginatedContacts(page, pageSize, sortOrder, searchQuery);
            //Assert
            Assert.NotNull(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
        }

        //[Fact]
        //public void GetPaginatedContacts_ReturnsEmptyList_WhenContactsExists_SearchIsNull()
        //{
        //    string searchQuery = null;
        //    string sortOrder = "asc";
        //    int page = 1;
        //    int pageSize = 2;
        //    var contacts = new List<Contactbook>().AsQueryable();
        //    var mockDbSet = new Mock<DbSet<Contactbook>>();
        //    mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
        //    mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
        //    var mockAppDbContext = new Mock<IAppDbContext>();
        //    mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
        //    var target = new ContactbookRepository(mockAppDbContext.Object);
        //    //Act
        //    var actual = target.GetPaginatedContacts(page, pageSize, sortOrder, searchQuery);
        //    //Assert
        //    Assert.NotNull(actual);
        //    mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
        //    mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
        //}


        //[Fact]
        //public void GetPaginatedContacts_ReturnsEmptyList_WhenContactsExists_SearchIsNotNull()
        //{
        //    string searchQuery = "search";
        //    string sortOrder = "asc";
        //    int page = 1;
        //    int pageSize = 2;
        //    var contacts = new List<Contactbook>().AsQueryable();
        //    var mockDbSet = new Mock<DbSet<Contactbook>>();
        //    mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
        //    mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
        //    var mockAppDbContext = new Mock<IAppDbContext>();
        //    mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
        //    var target = new ContactbookRepository(mockAppDbContext.Object);
        //    //Act
        //    var actual = target.GetPaginatedContacts(page, pageSize, sortOrder, searchQuery);
        //    //Assert
        //    Assert.NotNull(actual);
        //    mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
        //    mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
        //}


        [Fact]
        public void GetPaginatedContacts_ReturnsCorrectContactsInDescending_WhenContactsExists_SearchIsNull()
        {
            string searchQuery = null;
            string sortOrder = "desc";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                  new Contactbook{ContactId=1, Name="Contact 1"},
                  new Contactbook{ContactId=2, Name="Contact 2"},
                  new Contactbook{ContactId=3, Name="Contact 3"},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            //Act
            var actual = target.GetPaginatedContacts(page, pageSize, sortOrder, searchQuery);
            //Assert
            Assert.NotNull(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
        }


        [Fact]
        public void GetPaginatedContacts_ReturnsCorrectContactsInDescending_WhenContactsExists_SearchIsNotNull()
        {
            string searchQuery = "search";
            string sortOrder = "desc";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                  new Contactbook{ContactId=1, Name="Contact 1"},
                  new Contactbook{ContactId=2, Name="Contact 2"},
                  new Contactbook{ContactId=3, Name="Contact 3"},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            //Act
            var actual = target.GetPaginatedContacts(page, pageSize, sortOrder, searchQuery);
            //Assert
            Assert.NotNull(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
        }

        [Fact]
        public void GetPaginatedContacts_ThrowsArgumentException_WhenInvalidSortingOrder_SearchIsNull()
        {
            string searchQuery = null;
            string sortOrder = "invalid";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                  new Contactbook{ContactId=1, Name="Contact 1"},
                  new Contactbook{ContactId=2, Name="Contact 2"},
                  new Contactbook{ContactId=3, Name="Contact 3"},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            // Act and Assert
            Assert.Throws<ArgumentException>(() => target.GetPaginatedContacts(page, pageSize, sortOrder, searchQuery));
        }


        [Fact]
        public void GetPaginatedContacts_ThrowsArgumentException_WhenInvalidSortingOrder_SearchIsNotNull()
        {
            string searchQuery = "search";
            string sortOrder = "invalid";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                  new Contactbook{ContactId=1, Name="Contact 1"},
                  new Contactbook{ContactId=2, Name="Contact 2"},
                  new Contactbook{ContactId=3, Name="Contact 3"},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            // Act and Assert
            Assert.Throws<ArgumentException>(() => target.GetPaginatedContacts(page, pageSize, sortOrder, searchQuery));
        }

        //total favourite contact
        [Fact]
        public void TotalFavouriteContacts_ReturnsCount_WhenFavouriteContactsExist()
        {
            var contacts = new List<Contactbook> {
                new Contactbook {ContactId = 1,Name="Contact 1",Favourite = false},
                new Contactbook {ContactId = 2,Name="Contact 2",Favourite = true}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);

            //Act
            var actual = target.TotalFavouriteContacts();

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(1, actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockAppDbContext.Verify(c => c.Contactbooks, Times.Once);

        }

        //GetPaginatedFavouriteContacts

        [Fact]
        public void GetPaginatedFavouriteContacts_ReturnsCorrectContacts_WhenContactsExists()
        {
            string sortOrder = "asc";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                  new Contactbook{ContactId=1, Name="Contact 1",Favourite=false},
                  new Contactbook{ContactId=2, Name="Contact 2",Favourite=true},
                  new Contactbook{ContactId=3, Name="Contact 3",Favourite=true},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            //Act
            var actual = target.GetPaginatedFavouriteContacts(page, pageSize, sortOrder);
            //Assert
            Assert.NotNull(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
        }

        [Fact]
        public void GetPaginatedFavouriteContacts_ReturnsCorrectContactsInDescending_WhenContactsExists()
        {
            string sortOrder = "desc";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                 new Contactbook{ContactId=1, Name="Contact 1",Favourite=false},
                  new Contactbook{ContactId=2, Name="Contact 2",Favourite=true},
                  new Contactbook{ContactId=3, Name="Contact 3",Favourite=true},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            //Act
            var actual = target.GetPaginatedFavouriteContacts(page, pageSize, sortOrder);
            //Assert
            Assert.NotNull(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
        }

        [Fact]
        public void GetPaginatedFavouriteContacts_ThrowsArgumentException_WhenInvalidSortingOrder()
        {
            string sortOrder = "invalid";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                 new Contactbook{ContactId=1, Name="Contact 1",Favourite=false},
                  new Contactbook{ContactId=2, Name="Contact 2",Favourite=true},
                  new Contactbook{ContactId=3, Name="Contact 3",Favourite=true},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            // Act and Assert
            Assert.Throws<ArgumentException>(() => target.GetPaginatedFavouriteContacts(page, pageSize, sortOrder));
        }

        //TotalSpecificContacts
        [Fact]
        public void TotalSpecificContacts_ReturnsCount_WhenContactsExistWhenSearchIsNotNull()
        {
            string searchQuery = "Con";
            string letter = "D";
            var contacts = new List<Contactbook> {
                new Contactbook {ContactId = 1,Name="Contact 1"},
                new Contactbook {ContactId = 2,Name="Contact 2"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);

            //Act
            var actual = target.TotalSpecificContacts(letter,searchQuery);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(0, actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockAppDbContext.Verify(c => c.Contactbooks, Times.Once);

        }

        [Fact]
        public void TotalSpecificContacts_ReturnsCount_WhenContactsExistWhenSearchIsNull()
        {
            string searchQuery = null;
            string letter = "D";
            var contacts = new List<Contactbook> {
                new Contactbook {ContactId = 1,Name="Contact 1"},
                new Contactbook {ContactId = 2,Name="Contact 2"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);

            //Act
            var actual = target.TotalSpecificContacts(letter,searchQuery);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(0, actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockAppDbContext.Verify(c => c.Contactbooks, Times.Once);

        }

        //GetPaginatedContactsWithLetter
        [Fact]
        public void GetPaginatedContactsWithLetter_ReturnsCorrectContacts_WhenContactsExists_SearchIsNull()
        {
            string searchQuery = null;
            string letter = "c";
            string sortOrder = "asc";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                  new Contactbook{ContactId=1, Name="Contact 1"},
                  new Contactbook{ContactId=2, Name="Contact 2"},
                  new Contactbook{ContactId=3, Name="Contact 3"},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            //Act
            var actual = target.GetPaginatedContactsWithLetter(page, pageSize, letter,sortOrder, searchQuery);
            //Assert
            Assert.NotNull(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
        }


        [Fact]
        public void GetPaginatedContactsWithLetter_ReturnsCorrectContacts_WhenContactsExists_SearchIsNotNull()
        {
            string searchQuery = "search";
            string sortOrder = "asc";
            string letter = "c";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                  new Contactbook{ContactId=1, Name="Contact 1"},
                  new Contactbook{ContactId=2, Name="Contact 2"},
                  new Contactbook{ContactId=3, Name="Contact 3"},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            //Act
            var actual = target.GetPaginatedContactsWithLetter(page, pageSize, letter,sortOrder, searchQuery);
            //Assert
            Assert.NotNull(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
        }

        [Fact]
        public void GetPaginatedContactsWithLetter_ReturnsCorrectContactsInDescending_WhenContactsExists_SearchIsNull()
        {
            string searchQuery = null;
            string sortOrder = "desc";
            string letter = "c";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                  new Contactbook{ContactId=1, Name="Contact 1"},
                  new Contactbook{ContactId=2, Name="Contact 2"},
                  new Contactbook{ContactId=3, Name="Contact 3"},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            //Act
            var actual = target.GetPaginatedContactsWithLetter(page, pageSize, letter,sortOrder, searchQuery);
            //Assert
            Assert.NotNull(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
        }


        [Fact]
        public void GetPaginatedContactsWithLetter_ReturnsCorrectContactsInDescending_WhenContactsExists_SearchIsNotNull()
        {
            string searchQuery = "search";
            string sortOrder = "desc";
            string letter = "c";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                  new Contactbook{ContactId=1, Name="Contact 1"},
                  new Contactbook{ContactId=2, Name="Contact 2"},
                  new Contactbook{ContactId=3, Name="Contact 3"},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            //Act
            var actual = target.GetPaginatedContactsWithLetter(page, pageSize, letter,sortOrder, searchQuery);
            //Assert
            Assert.NotNull(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
        }

        [Fact]
        public void GetPaginatedContactsWithLetter_ThrowsArgumentException_WhenInvalidSortingOrder_SearchIsNull()
        {
            string searchQuery = null;
            string sortOrder = "invalid";
            string letter = "c";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                  new Contactbook{ContactId=1, Name="Contact 1"},
                  new Contactbook{ContactId=2, Name="Contact 2"},
                  new Contactbook{ContactId=3, Name="Contact 3"},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            // Act and Assert
            Assert.Throws<ArgumentException>(() => target.GetPaginatedContactsWithLetter(page, pageSize, letter,sortOrder, searchQuery));
        }


        [Fact]
        public void GetPaginatedContactsWithLetter_ThrowsArgumentException_WhenInvalidSortingOrder_SearchIsNotNull()
        {
            string searchQuery = "search";
            string sortOrder = "invalid";
            string letter = "c";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                  new Contactbook{ContactId=1, Name="Contact 1"},
                  new Contactbook{ContactId=2, Name="Contact 2"},
                  new Contactbook{ContactId=3, Name="Contact 3"},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            // Act and Assert
            Assert.Throws<ArgumentException>(() => target.GetPaginatedContactsWithLetter(page, pageSize, letter,sortOrder, searchQuery));
        }

        //TotalSpecificFavouriteContacts
        [Fact]
        public void TotalSpecificFavouriteContacts_ReturnsCount_WhenFavouriteContactsExist()
        {
            string letter = "C";
            var contacts = new List<Contactbook> {
                new Contactbook {ContactId = 1,Name="Contact 1",Favourite = false},
                new Contactbook {ContactId = 2,Name="Contact 2",Favourite = true}
            }.AsQueryable();


            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);

            //Act
            var actual = target.TotalSpecificFavouriteContacts(letter);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(1, actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockAppDbContext.Verify(c => c.Contactbooks, Times.Once);

        }

        //GetPaginatedFavouriteContactsWithLetter

        [Fact]
        public void GetPaginatedFavouriteContactsWithLetter_ReturnsCorrectContacts_WhenContactsExists()
        {
            string sortOrder = "asc";
            string letter = "C";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                  new Contactbook{ContactId=1, Name="Contact 1",Favourite=false},
                  new Contactbook{ContactId=2, Name="Contact 2",Favourite=true},
                  new Contactbook{ContactId=3, Name="Contact 3",Favourite=true},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            //Act
            var actual = target.GetPaginatedFavouriteContactsWithLetter(page, pageSize,letter, sortOrder);
            //Assert
            Assert.NotNull(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
        }

        [Fact]
        public void GetPaginatedFavouriteContactsWithLetter_ReturnsCorrectContactsInDescending_WhenContactsExists()
        {
            string sortOrder = "desc";
            string letter = "C";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                 new Contactbook{ContactId=1, Name="Contact 1",Favourite=false},
                  new Contactbook{ContactId=2, Name="Contact 2",Favourite=true},
                  new Contactbook{ContactId=3, Name="Contact 3",Favourite=true},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            //Act
            var actual = target.GetPaginatedFavouriteContactsWithLetter(page, pageSize,letter, sortOrder);
            //Assert
            Assert.NotNull(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
        }

        [Fact]
        public void GetPaginatedFavouriteContactsWithLetter_ThrowsArgumentException_WhenInvalidSortingOrder()
        {
            string sortOrder = "invalid";
            string letter = "C";
            int page = 1;
            int pageSize = 2;
            var contacts = new List<Contactbook>
              {
                 new Contactbook{ContactId=1, Name="Contact 1",Favourite=false},
                  new Contactbook{ContactId=2, Name="Contact 2",Favourite=true},
                  new Contactbook{ContactId=3, Name="Contact 3",Favourite=true},

              }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            // Act and Assert
            Assert.Throws<ArgumentException>(() => target.GetPaginatedFavouriteContactsWithLetter(page, pageSize,letter, sortOrder));
        }

        //getcontact by id
        [Fact]
        public void GetContact_WhenContactIsNull()
        {
            //Arrange
            var id = 1;
            var contacts = new List<Contactbook>().AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            var mockAbContext = new Mock<IAppDbContext>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockAbContext.SetupGet(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAbContext.Object);
            //Act
            var actual = target.GetContacts(id);
            //Assert
            Assert.Null(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockAbContext.VerifyGet(c => c.Contactbooks, Times.Once);

        }
        [Fact]
        public void GetContact_WhenContactIsNotNull()
        {
            //Arrange
            var id = 1;
            var contacts = new List<Contactbook>()
            {
              new Contactbook { ContactId = 1, Name = "Contact 1" },
                new Contactbook { ContactId = 2, Name = "Contact 2" },
            }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            var mockAbContext = new Mock<IAppDbContext>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            mockAbContext.SetupGet(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAbContext.Object);
            //Act
            var actual = target.GetContacts(id);
            //Assert
            Assert.NotNull(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Exactly(3));
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockAbContext.VerifyGet(c => c.Contactbooks, Times.Once);

        }
    
        //insert contact
        [Fact]
        public void InsertContact_ReturnsTrue()
        {
            //Arrange
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.SetupGet(c => c.Contactbooks).Returns(mockDbSet.Object);
            mockAppDbContext.Setup(c => c.SaveChanges()).Returns(1);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            var contact = new Contactbook
            {
                ContactId = 1,
                Name = "C1"
            };


            //Act
            var actual = target.InsertContact(contact);

            //Assert
            Assert.True(actual);
            mockDbSet.Verify(c => c.Add(contact), Times.Once);
            mockAppDbContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void InsertContact_ReturnsFalse()
        {
            //Arrange
            Contactbook contact = null;
            var mockAbContext = new Mock<IAppDbContext>();
            var target = new ContactbookRepository(mockAbContext.Object);

            //Act
            var actual = target.InsertContact(contact);
            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void UpdateContact_ReturnsTrue()
        {
            //Arrange
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            var mockAppDbContext = new Mock<IAppDbContext>();
            mockAppDbContext.SetupGet(c => c.Contactbooks).Returns(mockDbSet.Object);
            mockAppDbContext.Setup(c => c.SaveChanges()).Returns(1);
            var target = new ContactbookRepository(mockAppDbContext.Object);
            var contact = new Contactbook
            {
                ContactId = 1,
                Name = "C1"
            };


            //Act
            var actual = target.UpdateContact(contact);

            //Assert
            Assert.True(actual);
            mockDbSet.Verify(c => c.Update(contact), Times.Once);
            mockAppDbContext.Verify(c => c.SaveChanges(), Times.Once);
        }
        [Fact]
        public void UpdateContact_ReturnsFalse()
        {
            //Arrange
            Contactbook contact = null;
            var mockAbContext = new Mock<IAppDbContext>();
            var target = new ContactbookRepository(mockAbContext.Object);

            //Act
            var actual = target.UpdateContact(contact);
            //Assert
            Assert.False(actual);
        }


        [Fact]
        public void DeleteContact_ReturnsTrue()
        {
            // Arrange
            var contactId = 1;
            var contact = new Contactbook { ContactId = contactId };
            var mockContext = new Mock<IAppDbContext>();
            mockContext.Setup(c => c.Contactbooks.Find(contactId)).Returns(contact);
            var target = new ContactbookRepository(mockContext.Object);
            // Act
            var result = target.DeleteContact(contactId);

            // Assert
            Assert.True(result);
            mockContext.Verify(c => c.Contactbooks.Remove(contact), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
            mockContext.Verify(c => c.Contactbooks.Find(contactId), Times.Once);

        }

        [Fact]
        public void DeleteContact_ReturnsFalse()
        {
            //Arrange
            var id = 1;
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            var mockAbContext = new Mock<IAppDbContext>();
            mockAbContext.SetupGet(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAbContext.Object);

            //Act
            var actual = target.DeleteContact(id);
            //Assert
            Assert.False(actual);
            mockAbContext.VerifyGet(c => c.Contactbooks, Times.Once);
        }

        [Fact]
        public void ContactExists_ReturnsTrue()
        {
            //Arrange
            var phone = "1234567890";
            var contacts = new List<Contactbook>
            {
                new Contactbook { ContactId = 1, Name = "Contact 1", PhoneNumber="1234567890"},
                new Contactbook { ContactId = 2, Name = "Contact 2", PhoneNumber="9876543216" },
            }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            var mockAbContext = new Mock<IAppDbContext>();
            mockAbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAbContext.Object);

            //Act
            var actual = target.ContactExists(phone);
            //Assert
            Assert.True(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockAbContext.Verify(c => c.Contactbooks, Times.Once);
        }

        [Fact]
        public void ContactExists_ReturnsFalse()
        {
            //Arrange
            var phone = "1234567890";
            var contacts = new List<Contactbook>().AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            var mockAbContext = new Mock<IAppDbContext>();
            mockAbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAbContext.Object);

            //Act
            var actual = target.ContactExists(phone);
            //Assert
            Assert.False(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockAbContext.Verify(c => c.Contactbooks, Times.Once);
        }

        [Fact]
        public void ContactExistsIdName_ReturnsFalse()
        {
            //Arrange
            var phone = "1234567890";
            var id = 1;
            var contacts = new List<Contactbook>().AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            var mockAbContext = new Mock<IAppDbContext>();
            mockAbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAbContext.Object);

            //Act
            var actual = target.ContactExists(id, phone);
            //Assert
            Assert.False(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockAbContext.Verify(c => c.Contactbooks, Times.Once);
        }

        [Fact]
        public void ContactExistsIdName_ReturnsTrue()
        {
            //Arrange
            var phone = "1234567890";
            var id = 3;
            var contacts = new List<Contactbook>
            {
                new Contactbook { ContactId = 1, Name = "Contact 1", PhoneNumber="1234567890" },
                new Contactbook { ContactId = 2, Name = "Contact 2" , PhoneNumber="9876543219"},
            }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Contactbook>>();
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Provider).Returns(contacts.Provider);
            mockDbSet.As<IQueryable<Contactbook>>().Setup(c => c.Expression).Returns(contacts.Expression);
            var mockAbContext = new Mock<IAppDbContext>();
            mockAbContext.Setup(c => c.Contactbooks).Returns(mockDbSet.Object);
            var target = new ContactbookRepository(mockAbContext.Object);

            //Act
            var actual = target.ContactExists(id, phone);
            //Assert
            Assert.True(actual);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Provider, Times.Once);
            mockDbSet.As<IQueryable<Contactbook>>().Verify(c => c.Expression, Times.Once);
            mockAbContext.Verify(c => c.Contactbooks, Times.Once);
        }

    }
}
