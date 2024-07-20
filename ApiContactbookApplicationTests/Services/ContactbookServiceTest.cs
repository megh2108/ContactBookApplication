using ApiContactbookApplication.Data.Contract;
using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Models;
using ApiContactbookApplication.Services.Implementation;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiContactbookApplicationTests.Services
{
    public class ContactbookServiceTest
    {
        //GetAllContacts

        [Fact]
        public void GetCAllontacts_ReturnList_WhenNoContactExist()
        {
            //Arrange
            var mockRepository = new Mock<IContactbookRepository>();
            var target = new ContactbookService(mockRepository.Object);
            //Act
            var actual = target.GetAllContacts();

            //Assert
            Assert.NotNull(actual);
            Assert.Equal("No record found !.", actual.Message);
            Assert.False(actual.Success);
        }
        [Fact]
        public void GetAllContacts_ReturnsContactsList_WhenContactsExist()
        {
            //Arrange
            var contacts = new List<Contactbook>
            {
            new Contactbook
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                FileName = "file1.txt",
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
                PhoneNumber = "9876543210",
                FileName = "file2.txt",
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
        };

            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = true,
              
            };
            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(c => c.GetAll()).Returns(contacts);
            var target = new ContactbookService(mockRepository.Object);

            //Act
            var actual = target.GetAllContacts();

            //Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
            mockRepository.Verify(c => c.GetAll(), Times.Once);
            Assert.Equal(contacts.Count, actual.Data.Count()); // Ensure the counts are equal

            for (int i = 0; i < contacts.Count; i++)
            {
                Assert.Equal(contacts[i].ContactId, actual.Data.ElementAt(i).ContactId);
                Assert.Equal(contacts[i].Name, actual.Data.ElementAt(i).Name);

            }
        }

        //GetAllFavouriteContacts

        [Fact]
        public void GetAllFavouriteContacts_ReturnList_WhenNoContactExist()
        {
            //Arrange
            var mockRepository = new Mock<IContactbookRepository>();
            var target = new ContactbookService(mockRepository.Object);
            //Act
            var actual = target.GetAllFavouriteContacts();

            //Assert
            Assert.NotNull(actual);
            Assert.Equal("No record found !.", actual.Message);
            Assert.False(actual.Success);
        }
        [Fact]
        public void GetAllFavouriteContacts_ReturnsContactsList_WhenContactsExist()
        {
            //Arrange
            var contacts = new List<Contactbook>
            {
            new Contactbook
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                FileName = "file1.txt",
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
                PhoneNumber = "9876543210",
                FileName = "file2.txt",
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
        };

            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = true,

            };
            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(c => c.GetAllFavourite()).Returns(contacts);
            var target = new ContactbookService(mockRepository.Object);

            //Act
            var actual = target.GetAllFavouriteContacts();

            //Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
            mockRepository.Verify(c => c.GetAllFavourite(), Times.Once);
            Assert.Equal(contacts.Count, actual.Data.Count()); // Ensure the counts are equal

            for (int i = 0; i < contacts.Count; i++)
            {
                Assert.Equal(contacts[i].ContactId, actual.Data.ElementAt(i).ContactId);
                Assert.Equal(contacts[i].Name, actual.Data.ElementAt(i).Name);

            }
        }

        //GetAllSpecificContact

        [Fact]
        public void GetAllSpecificContact_ReturnList_WhenNoContactExist()
        {
            //Arrange
            string letter = "d";
            var mockRepository = new Mock<IContactbookRepository>();
            var target = new ContactbookService(mockRepository.Object);
            //Act
            var actual = target.GetAllSpecificContact(letter);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal("No record found !.", actual.Message);
            Assert.True(actual.Success);
        }
        [Fact]
        public void GetAllSpecificContact_ReturnsContactsList_WhenContactsExist()
        {
            //Arrange
            var contacts = new List<Contactbook>
            {
            new Contactbook
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                FileName = "file1.txt",
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
                PhoneNumber = "9876543210",
                FileName = "file2.txt",
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
        };

            string letter = "d";

            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = true,

            };
            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(c => c.GetSpecificContact(letter)).Returns(contacts);
            var target = new ContactbookService(mockRepository.Object);

            //Act
            var actual = target.GetAllSpecificContact(letter);

            //Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
            mockRepository.Verify(c => c.GetSpecificContact(letter), Times.Once);
            Assert.Equal(contacts.Count, actual.Data.Count()); // Ensure the counts are equal

            for (int i = 0; i < contacts.Count; i++)
            {
                Assert.Equal(contacts[i].ContactId, actual.Data.ElementAt(i).ContactId);
                Assert.Equal(contacts[i].Name, actual.Data.ElementAt(i).Name);

            }
        }

        //TotalContacts
        [Fact]
        public void TotalContacts_ReturnsContactCount_WhenSearchIsNull()
        {
            var contacts = new List<Contactbook>
        {
            new Contactbook
            {
                ContactId = 1,
                Name = "John"

            },
            new Contactbook
            {
                ContactId = 2,
                Name = "Jane"

            }
        };

            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.TotalContacts(null)).Returns(contacts.Count);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.TotalContacts(null);

            // Assert
            Assert.True(actual.Success);
            Assert.Equal(contacts.Count, actual.Data);
            mockRepository.Verify(r => r.TotalContacts(null), Times.Once);
        }

        [Fact]
        public void TotalContacts_ReturnsContactCount_WhenSearchIsNotNull()
        {
            string searchQuery = "dev";
            var contacts = new List<Contactbook>
        {
            new Contactbook
            {
                ContactId = 1,
                Name = "John"

            },
            new Contactbook
            {
                ContactId = 2,
                Name = "Jane"

            }
        };

            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.TotalContacts(searchQuery)).Returns(contacts.Count);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.TotalContacts(searchQuery);

            // Assert
            Assert.True(actual.Success);
            Assert.Equal(contacts.Count, actual.Data);
            mockRepository.Verify(r => r.TotalContacts(searchQuery), Times.Once);
        }

        //GetPaginatedContacts

        [Fact]
        public void GetPaginatedContacts_ReturnsNoRecord_WhenContactsNotExistAndSearchIsNull()
        {

            // Arrange
            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.GetPaginatedContacts(page, pageSize,sortOrder,null)).Returns<IEnumerable<Contactbook>>(null);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.GetPaginatedContacts(page, pageSize, sortOrder,null);

            // Assert
            Assert.True(actual.Success);
            Assert.Null(actual.Data);
            Assert.Equal("No record found !.", actual.Message);
            mockRepository.Verify(r => r.GetPaginatedContacts(page, pageSize,sortOrder,null), Times.Once);
        }


        [Fact]
        public void GetPaginatedContacts_ReturnsNoRecord_WhenContactsNotExistAndSearchIsNoyNull()
        {

            // Arrange
            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string searchQuery = "dev";
            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.GetPaginatedContacts(page, pageSize, sortOrder, searchQuery)).Returns<IEnumerable<Contactbook>>(null);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.GetPaginatedContacts(page, pageSize, sortOrder, searchQuery);

            // Assert
            Assert.True(actual.Success);
            Assert.Null(actual.Data);
            Assert.Equal("No record found !.", actual.Message);
            mockRepository.Verify(r => r.GetPaginatedContacts(page, pageSize, sortOrder, searchQuery), Times.Once);
        }

        [Fact]
        public void GetPaginatedContacts_ReturnsContacts_WhenContactsExistAndSearchIsNull()
        {

            // Arrange
            string sortOrder = "asc";
            int page = 1;
            int pageSize = 2;

            var contacts = new List<Contactbook>
            {
            new Contactbook
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                FileName = "file1.txt",
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
                PhoneNumber = "9876543210",
                FileName = "file2.txt",
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
        };



            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.GetPaginatedContacts(page, pageSize,sortOrder,null)).Returns(contacts);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.GetPaginatedContacts(page, pageSize, sortOrder,null);

            // Assert
            Assert.True(actual.Success);
            Assert.NotNull(actual.Data);
            Assert.Equal(contacts.Count, actual.Data.Count());
            mockRepository.Verify(r => r.GetPaginatedContacts(page, pageSize, sortOrder, null), Times.Once);
        }

        [Fact]
        public void GetPaginatedContacts_ReturnsContacts_WhenContactsExistAndSearchIsNotNull()
        {

            // Arrange
            string sortOrder = "asc";
            string searchQuery = "dev"; 
            int page = 1;
            int pageSize = 2;

            var contacts = new List<Contactbook>
            {
            new Contactbook
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                FileName = "file1.txt",
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
                PhoneNumber = "9876543210",
                FileName = "file2.txt",
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
        };



            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.GetPaginatedContacts(page, pageSize, sortOrder, searchQuery)).Returns(contacts);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.GetPaginatedContacts(page, pageSize, sortOrder, searchQuery);

            // Assert
            Assert.True(actual.Success);
            Assert.NotNull(actual.Data);
            Assert.Equal(contacts.Count, actual.Data.Count());
            mockRepository.Verify(r => r.GetPaginatedContacts(page, pageSize, sortOrder, searchQuery), Times.Once);
        }

        //TotalFavouriteContacts
        [Fact]
        public void TotalFavouriteContacts_ReturnsContactCount()
        {
            var contacts = new List<Contactbook>
        {
            new Contactbook
            {
                ContactId = 1,
                Name = "John"

            },
            new Contactbook
            {
                ContactId = 2,
                Name = "Jane"

            }
        };

            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.TotalFavouriteContacts()).Returns(contacts.Count);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.TotalFavouriteContacts();

            // Assert
            Assert.True(actual.Success);
            Assert.Equal(contacts.Count, actual.Data);
            mockRepository.Verify(r => r.TotalFavouriteContacts(), Times.Once);
        }

        //GetPaginatedFavouriteContact

        [Fact]
        public void GetPaginatedFavouriteContacts_ReturnsContacts_WhenContactsExist()
        {

            // Arrange
            string sortOrder = "asc";
            int page = 1;
            int pageSize = 2;

            var contacts = new List<Contactbook>
            {
            new Contactbook
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                FileName = "file1.txt",
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
                PhoneNumber = "9876543210",
                FileName = "file2.txt",
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
        };



            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.GetPaginatedFavouriteContacts(page, pageSize, sortOrder)).Returns(contacts);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.GetPaginatedFavouriteContacts(page, pageSize, sortOrder);

            // Assert
            Assert.True(actual.Success);
            Assert.NotNull(actual.Data);
            Assert.Equal(contacts.Count, actual.Data.Count());
            mockRepository.Verify(r => r.GetPaginatedFavouriteContacts(page, pageSize, sortOrder), Times.Once);
        }

        [Fact]
        public void GetPaginatedFavouriteContacts_ReturnsNoRecord_WhenContactsNotExist()
        {

            // Arrange
            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string searchQuery = "dev";
            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.GetPaginatedFavouriteContacts(page, pageSize, sortOrder)).Returns<IEnumerable<Contactbook>>(null);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.GetPaginatedFavouriteContacts(page, pageSize, sortOrder);

            // Assert
            Assert.True(actual.Success);
            Assert.Null(actual.Data);
            Assert.Equal("No record found !.", actual.Message);
            mockRepository.Verify(r => r.GetPaginatedFavouriteContacts(page, pageSize, sortOrder), Times.Once);
        }

        //TotalSpecificContacts
        [Fact]
        public void TotalSpecificContacts_ReturnsContactCount_WhenSearchIsNull()
        {
            var contacts = new List<Contactbook>
        {
            new Contactbook
            {
                ContactId = 1,
                Name = "John"

            },
            new Contactbook
            {
                ContactId = 2,
                Name = "Jane"

            }
        };

            string letter = "d";


            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.TotalSpecificContacts(letter,null)).Returns(contacts.Count);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.TotalSpecificContacts(letter,null);

            // Assert
            Assert.True(actual.Success);
            Assert.Equal(contacts.Count, actual.Data);
            mockRepository.Verify(r => r.TotalSpecificContacts(letter,null), Times.Once);
        }

        [Fact]
        public void TotalSpecificContacts_ReturnsContactCount_WhenSearchIsNotNull()
        {
            string searchQuery = "dev";
            string letter = "d";
            var contacts = new List<Contactbook>
        {
            new Contactbook
            {
                ContactId = 1,
                Name = "John"

            },
            new Contactbook
            {
                ContactId = 2,
                Name = "Jane"

            }
        };

            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.TotalSpecificContacts(letter,searchQuery)).Returns(contacts.Count);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.TotalSpecificContacts(letter,searchQuery);

            // Assert
            Assert.True(actual.Success);
            Assert.Equal(contacts.Count, actual.Data);
            mockRepository.Verify(r => r.TotalSpecificContacts(letter,searchQuery), Times.Once);
        }


        //GetPaginatedContactsWithLetter
        [Fact]
        public void GetPaginatedContactsWithLetter_ReturnsNoRecord_WhenContactsNotExistAndSearchIsNull()
        {

            // Arrange
            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string letter = "d";
            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, null)).Returns<IEnumerable<Contactbook>>(null);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, null);

            // Assert
            Assert.True(actual.Success);
            Assert.Null(actual.Data);
            Assert.Equal("No record found !.", actual.Message);
            mockRepository.Verify(r => r.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, null), Times.Once);
        }


        [Fact]
        public void GetPaginatedContactsWithLetter_ReturnsNoRecord_WhenContactsNotExistAndSearchIsNoyNull()
        {

            // Arrange
            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string searchQuery = "dev";
            string letter = "d";
            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, searchQuery)).Returns<IEnumerable<Contactbook>>(null);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.GetPaginatedContactsWithLetter(page, pageSize, letter,sortOrder, searchQuery);

            // Assert
            Assert.True(actual.Success);
            Assert.Null(actual.Data);
            Assert.Equal("No record found !.", actual.Message);
            mockRepository.Verify(r => r.GetPaginatedContactsWithLetter(page, pageSize, letter,sortOrder, searchQuery), Times.Once);
        }

        [Fact]
        public void GetPaginatedContactsWithLetter_ReturnsContacts_WhenContactsExistAndSearchIsNull()
        {

            // Arrange
            string sortOrder = "asc";
            int page = 1;
            int pageSize = 2;
            string letter = "d";

            var contacts = new List<Contactbook>
            {
            new Contactbook
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                FileName = "file1.txt",
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
                PhoneNumber = "9876543210",
                FileName = "file2.txt",
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
        };



            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.GetPaginatedContactsWithLetter(page, pageSize, letter,sortOrder, null)).Returns(contacts);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, null);

            // Assert
            Assert.True(actual.Success);
            Assert.NotNull(actual.Data);
            Assert.Equal(contacts.Count, actual.Data.Count());
            mockRepository.Verify(r => r.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, null), Times.Once);
        }

        [Fact]
        public void GetPaginatedContactsWithLetter_ReturnsContacts_WhenContactsExistAndSearchIsNotNull()
        {

            // Arrange
            string sortOrder = "asc";
            string searchQuery = "dev";
            int page = 1;
            int pageSize = 2;
            string letter = "d";

            var contacts = new List<Contactbook>
            {
            new Contactbook
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                FileName = "file1.txt",
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
                PhoneNumber = "9876543210",
                FileName = "file2.txt",
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
        };



            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, searchQuery)).Returns(contacts);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, searchQuery);

            // Assert
            Assert.True(actual.Success);
            Assert.NotNull(actual.Data);
            Assert.Equal(contacts.Count, actual.Data.Count());
            mockRepository.Verify(r => r.GetPaginatedContactsWithLetter(page, pageSize, letter,sortOrder, searchQuery), Times.Once);
        }

        //TotalFavouriteSpecificContacts
        [Fact]
        public void TotalFavouriteSpecificContacts_ReturnsContactCount()
        {
            var contacts = new List<Contactbook>
        {
            new Contactbook
            {
                ContactId = 1,
                Name = "John"

            },
            new Contactbook
            {
                ContactId = 2,
                Name = "Jane"

            }
        };
            string letter = "d";

            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.TotalSpecificFavouriteContacts(letter)).Returns(contacts.Count);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.TotalFavouriteSpecificContacts(letter);

            // Assert
            Assert.True(actual.Success);
            Assert.Equal(contacts.Count, actual.Data);
            mockRepository.Verify(r => r.TotalSpecificFavouriteContacts(letter), Times.Once);
        }


        //GetPaginatedFavouriteContactsWithLetter
        [Fact]
        public void GetPaginatedFavouriteContactsWithLetter_ReturnsNoRecord_WhenContactsNotExist()
        {

            // Arrange
            int page = 1;
            int pageSize = 2;
            string sortOrder = "asc";
            string letter = "d";
            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.GetPaginatedFavouriteContactsWithLetter(page, pageSize, letter, sortOrder)).Returns<IEnumerable<Contactbook>>(null);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.GetPaginatedFavouriteContactsWithLetter(page, pageSize, letter, sortOrder);

            // Assert
            Assert.True(actual.Success);
            Assert.Null(actual.Data);
            Assert.Equal("No record found !.", actual.Message);
            mockRepository.Verify(r => r.GetPaginatedFavouriteContactsWithLetter(page, pageSize, letter, sortOrder), Times.Once);
        }

        [Fact]
        public void GetPaginatedFavouriteContactsWithLetter_ReturnsContacts_WhenContactsExist()
        {

            // Arrange
            string sortOrder = "asc";
            int page = 1;
            int pageSize = 2;
            string letter = "d";

            var contacts = new List<Contactbook>
            {
            new Contactbook
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                FileName = "file1.txt",
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
                PhoneNumber = "9876543210",
                FileName = "file2.txt",
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
        };



            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.GetPaginatedFavouriteContactsWithLetter(page, pageSize, letter, sortOrder)).Returns(contacts);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.GetPaginatedFavouriteContactsWithLetter(page, pageSize, letter, sortOrder);

            // Assert
            Assert.True(actual.Success);
            Assert.NotNull(actual.Data);
            Assert.Equal(contacts.Count, actual.Data.Count());
            mockRepository.Verify(r => r.GetPaginatedFavouriteContactsWithLetter(page, pageSize, letter, sortOrder), Times.Once);
        }

        //get contact by id

        [Fact]
        public void GetContactsById_ReturnEmpty_WhenNoContactExist()
        {
            //Arrange
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = false,

            };

            var mockRepository = new Mock<IContactbookRepository>();
            var target = new ContactbookService(mockRepository.Object);
            //Act
            var actual = target.GetContactsById(1);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal("No record found !.", actual.Message);
            Assert.False(actual.Success);
        }
        [Fact]
        public void GetContactsById_ReturnsContact_WhenContactsExist()
        {
            //Arrange
            var contact = new Contactbook
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                FileName = "file1.txt",
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
            };

            var response = new ServiceResponse<IEnumerable<ContactbookDto>>
            {
                Success = true,

            };
            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(c => c.GetContacts(contact.ContactId)).Returns(contact);
            var target = new ContactbookService(mockRepository.Object);

            //Act
            var actual = target.GetContactsById(contact.ContactId);

            //Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
            mockRepository.Verify(c => c.GetContacts(contact.ContactId), Times.Once);
           

          
        }

        //addcontact

        [Fact]
        public void AddContact_ReturnsAlreadyExists_WhenContactAlreadyExists()
        {
            var contact = new Contactbook()
            {
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                Gender = "Male",
                Favourite = true,
                CountryId = 1,
                StateId = 1
            };


            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.ContactExists(contact.PhoneNumber)).Returns(true);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.AddContact(contact);


            // Assert
            Assert.NotNull(actual);
            Assert.False(actual.Success);
            Assert.Equal("Contact already exists.", actual.Message);
            mockRepository.Verify(r => r.ContactExists(contact.PhoneNumber), Times.Once);

        }

        [Fact]
        public void AddContact_ReturnsInvalidDate_WhenDateIsNotValid()
        {
            var contact = new Contactbook()
            {
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                Gender = "Male",
                Favourite = true,
                BirthDate = new DateTime(2025, 1, 1),
                CountryId = 1,
                StateId = 1
            };


            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.ContactExists(contact.PhoneNumber)).Returns(false);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.AddContact(contact);


            // Assert
            Assert.NotNull(actual);
            Assert.False(actual.Success);
            Assert.Equal("Enter valid date", actual.Message);
            mockRepository.Verify(r => r.ContactExists(contact.PhoneNumber), Times.Once);

        }

        [Fact]
        public void AddContact_ReturnsContactSavedSuccessfully_WhenContactisSaved()
        {
            var contact = new Contactbook()
            {
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                Gender = "Male",
                Favourite = true,
                CountryId = 1,
                StateId = 1,

            };


            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.ContactExists(contact.PhoneNumber)).Returns(false);
            mockRepository.Setup(r => r.InsertContact(contact)).Returns(true);


            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.AddContact(contact);


            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
            Assert.Equal("Contact saved successfully.", actual.Message);
            mockRepository.Verify(r => r.ContactExists(contact.PhoneNumber), Times.Once);
            mockRepository.Verify(r => r.InsertContact(contact), Times.Once);


        }

        [Fact]
        public void AddContact_ReturnsSomethingWentWrong_WhenContactisNotSaved()
        {
            var contact = new Contactbook()
            {

                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                Gender = "Male",
                Favourite = true,
                CountryId = 1,
                StateId = 1,
            };


            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.ContactExists(contact.PhoneNumber)).Returns(false);
            mockRepository.Setup(r => r.InsertContact(contact)).Returns(false);


            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.AddContact(contact);


            // Assert
            Assert.NotNull(actual);
            Assert.False(actual.Success);
            Assert.Equal("Something went wrong. Please try after sometime.", actual.Message);
            mockRepository.Verify(r => r.ContactExists(contact.PhoneNumber), Times.Once);
            mockRepository.Verify(r => r.InsertContact(contact), Times.Once);


        }

        //modifycontact

        [Fact]
        public void ModifyContact_ReturnsAlreadyExists_WhenContactAlreadyExists()
        {
            var contactId = 1;
            var contact = new Contactbook()
            {
                ContactId = contactId,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                Gender = "Male",
                Favourite = true,
                CountryId = 1,
                StateId = 1,
            };


            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.ContactExists(contactId, contact.PhoneNumber)).Returns(true);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.ModifyContact(contact);


            // Assert
            Assert.NotNull(actual);
            Assert.False(actual.Success);
            Assert.Equal("Contact already exists.", actual.Message);
            mockRepository.Verify(r => r.ContactExists(contactId, contact.PhoneNumber), Times.Once);
        }

        [Fact]
        public void ModifyContact_ReturnsInvalidDate_WhenDateIsNotValid()
        {
            var contactId = 1;
            var contact = new Contactbook()
            {
                ContactId = contactId,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                Gender = "Male",
                BirthDate = new DateTime(2025, 1, 1),
                Favourite = true,
                CountryId = 1,
                StateId = 1,
            };


            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.ContactExists(contactId, contact.PhoneNumber)).Returns(false);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.ModifyContact(contact);


            // Assert
            Assert.NotNull(actual);
            Assert.False(actual.Success);
            Assert.Equal("Enter valid date", actual.Message);
            mockRepository.Verify(r => r.ContactExists(contactId, contact.PhoneNumber), Times.Once);
        }
        [Fact]
        public void ModifyContact_ReturnsSomethingWentWrong_WhenContactNotFound()
        {
            var contactId = 1;
            var existingContact = new Contactbook()
            {
                ContactId = contactId,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                Gender = "Male",
                Favourite = true,
                CountryId = 1,
                StateId = 1,

            };

            var updatedContact = new Contactbook()
            {
                ContactId = contactId,
                Name = "C1"
            };


            var mockRepository = new Mock<IContactbookRepository>();
            //mockRepository.Setup(r => r.ContactExists(contactId, updatedContact.ContactNumber)).Returns(false);
            mockRepository.Setup(r => r.GetContacts(updatedContact.ContactId)).Returns<IEnumerable<Contactbook>>(null);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.ModifyContact(existingContact);


            // Assert
            Assert.NotNull(actual);
            Assert.False(actual.Success);
            Assert.Equal("Something went wrong. Please try afte sometime.", actual.Message);
            //mockRepository.Verify(r => r.ContactExists(contactId, updatedContact.ContactNumber), Times.Once);
            mockRepository.Verify(r => r.GetContacts(contactId), Times.Once);
        }

        [Fact]
        public void ModifyContact_ReturnsUpdatedSuccessfully_WhenContactModifiedSuccessfully()
        {

            //Arrange
            var existingContact = new Contactbook()
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                Gender = "Male",
                Favourite = true,
                CountryId = 1,
                StateId = 1,

            };

            var updatedContact = new Contactbook()
            {
                ContactId = 1,
                Name = "Contact 1"
            };

            var mockContactRepository = new Mock<IContactbookRepository>();

            mockContactRepository.Setup(c => c.ContactExists(updatedContact.ContactId, updatedContact.PhoneNumber)).Returns(false);
            mockContactRepository.Setup(c => c.GetContacts(updatedContact.ContactId)).Returns(existingContact);
            mockContactRepository.Setup(c => c.UpdateContact(existingContact)).Returns(true);

            var target = new ContactbookService(mockContactRepository.Object);

            //Act

            var actual = target.ModifyContact(updatedContact);


            //Assert
            Assert.NotNull(actual);
            Assert.Equal("Contact updated successfully.", actual.Message);

            mockContactRepository.Verify(c => c.GetContacts(updatedContact.ContactId), Times.Once);


            mockContactRepository.Verify(c => c.UpdateContact(existingContact), Times.Once);

        }
        [Fact]
        public void ModifyContact_ReturnsError_WhenContactModifiedFails()
        {

            //Arrange
            var existingContact = new Contactbook()
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                Gender = "Male",
                Favourite = true,
                CountryId = 1,
                StateId = 1,
            };

            var updatedContact = new Contactbook()
            {
                ContactId = 1,
                Name = "Contact 1"
            };

            var mockContactRepository = new Mock<IContactbookRepository>();

            mockContactRepository.Setup(c => c.ContactExists(updatedContact.ContactId, updatedContact.PhoneNumber)).Returns(false);
            mockContactRepository.Setup(c => c.GetContacts(updatedContact.ContactId)).Returns(existingContact);
            mockContactRepository.Setup(c => c.UpdateContact(existingContact)).Returns(false);

            var target = new ContactbookService(mockContactRepository.Object);

            //Act

            var actual = target.ModifyContact(updatedContact);


            //Assert
            Assert.NotNull(actual);
            Assert.Equal("Something went wrong. Please try afte sometime.", actual.Message);
            mockContactRepository.Verify(c => c.GetContacts(updatedContact.ContactId), Times.Once);
            mockContactRepository.Verify(c => c.UpdateContact(existingContact), Times.Once);

        }


        //remove contact
        [Fact]
        public void RemoveContact_ReturnsDeletedSuccessfully_WhenDeletedSuccessfully()
        {
            var contactId = 1;


            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.DeleteContact(contactId)).Returns(true);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.RemoveContact(contactId);

            // Assert
            Assert.True(actual.Success);
            Assert.NotNull(actual);
            Assert.Equal("Contact deleted successfully.", actual.Message);
            mockRepository.Verify(r => r.DeleteContact(contactId), Times.Once);
        }

        [Fact]
        public void RemoveContact_SomethingWentWrong_WhenDeletionFailed()
        {
            var contactId = 1;


            var mockRepository = new Mock<IContactbookRepository>();
            mockRepository.Setup(r => r.DeleteContact(contactId)).Returns(false);

            var contactService = new ContactbookService(mockRepository.Object);

            // Act
            var actual = contactService.RemoveContact(contactId);

            // Assert
            Assert.False(actual.Success);
            Assert.NotNull(actual);
            Assert.Equal("Something went wrong. Please try afte sometime.", actual.Message);
            mockRepository.Verify(r => r.DeleteContact(contactId), Times.Once);
        }



    }
}
