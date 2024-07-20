using ApiContactbookApplication.Data.Contract;
using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Models;
using ApiContactbookApplication.Services.Contract;
using Microsoft.IdentityModel.Tokens;

namespace ApiContactbookApplication.Services.Implementation
{
    public class ContactbookService : IContactbookService
    {
        private readonly IContactbookRepository _contactbookRepository;

        public ContactbookService(IContactbookRepository contactbookRepository)
        {
            _contactbookRepository = contactbookRepository;
        }


        //sp
        public ServiceResponse<IEnumerable<ContactbookDtoSP>> GetPaginatedContactsSP(string? search,int page, int pageSize, string sortOrder)
        {
            var response = new ServiceResponse<IEnumerable<ContactbookDtoSP>>();
            var contacts = _contactbookRepository.GetPaginatedContactsSP(search,page, pageSize, sortOrder);

            if (contacts != null && contacts.Any())
            {

                List<ContactbookDtoSP> contactDtos = new List<ContactbookDtoSP>();
                foreach (var contact in contacts)
                {
                    contactDtos.Add(
                        new ContactbookDtoSP()
                        {
                            ContactId = contact.ContactId,
                            Name = contact.Name,
                            Email = contact.Email,
                            PhoneNumber = contact.PhoneNumber,
                            Company = contact.Company,
                            FileName = contact.FileName,
                            File = contact.File,
                            BirthDate = contact.BirthDate,
                            Gender = contact.Gender,
                            Favourite = contact.Favourite,
                            CountryName = contact.CountryName,
                            StateName = contact.StateName,
                        });
                }
                response.Data = contactDtos;
                response.Success = true;
                response.Message = "Success";
            }
            else
            {
                response.Success = false;
                response.Message = "No record found !.";
            }

            return response;
        }

        public ServiceResponse<IEnumerable<ContactbookDtoSP>> GetAllContactsByBirthdayMonth(int month)
        {
            var response = new ServiceResponse<IEnumerable<ContactbookDtoSP>>();
            var contacts = _contactbookRepository.GetAllContactsByBirthdayMonth(month);

            if (contacts != null && contacts.Any())
            {

                List<ContactbookDtoSP> contactDtos = new List<ContactbookDtoSP>();
                foreach (var contact in contacts)
                {
                    contactDtos.Add(
                        new ContactbookDtoSP()
                        {
                            ContactId = contact.ContactId,
                            Name = contact.Name,
                            Email = contact.Email,
                            PhoneNumber = contact.PhoneNumber,
                            Company = contact.Company,
                            FileName = contact.FileName,
                            File = contact.File,
                            BirthDate = contact.BirthDate,
                            Gender = contact.Gender,
                            Favourite = contact.Favourite,
                            CountryName = contact.CountryName,
                            StateName = contact.StateName,
                        });
                }
                response.Data = contactDtos;
                response.Success = true;
                response.Message = "Success";
            }
            else
            {
                response.Success = false;
                response.Message = "No record found !.";
            }

            return response;
        }

        public ServiceResponse<IEnumerable<ContactbookDtoSP>> GetAllContactsByStates(int state)
        {
            var response = new ServiceResponse<IEnumerable<ContactbookDtoSP>>();
            var contacts = _contactbookRepository.GetAllContactsByStates(state);
            

            if (contacts != null && contacts.Any())
            {
                List<ContactbookDtoSP> contactDtos = new List<ContactbookDtoSP>();

                foreach (var contact in contacts)
                {
                    contactDtos.Add(
                        new ContactbookDtoSP()
                        {
                            ContactId = contact.ContactId,
                            Name = contact.Name,
                            Email = contact.Email,
                            PhoneNumber = contact.PhoneNumber,
                            Company = contact.Company,
                            FileName = contact.FileName,
                            File = contact.File,
                            BirthDate = contact.BirthDate,
                            Gender = contact.Gender,
                            Favourite = contact.Favourite,
                            CountryName = contact.CountryName,
                            StateName = contact.StateName,

                        });
                }
                response.Data = contactDtos;
                response.Success = true;
                response.Message = "Success";
            }
            else
            {
                response.Success = false;
                response.Message = "No record found !.";
            }

            return response;
        }

        public ServiceResponse<int> GetAllContactsCountByCountry(int country)
        {
            var response = new ServiceResponse<int>();

            var result = _contactbookRepository.GetAllContactsCountByCountry(country);

            if (result >= 0)
            {
                response.Data = result;
                response.Success = true;
            }
            else
            {
                response.Success = false;
            }
               

            return response;
        }

         public ServiceResponse<int> GetAllContactsCountByGender(string gender)
        {
            var response = new ServiceResponse<int>();

            var result = _contactbookRepository.GetAllContactsCountByGender(gender);


            if (result >= 0)
            {
                response.Data = result;
                response.Success = true;
            }
            else
            {
                response.Success = false;
            }
           

            return response;
        }


        //

        public ServiceResponse<IEnumerable<ContactbookDto>> GetAllContacts()
        {
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>();
            var contacts = _contactbookRepository.GetAll();

            if (contacts != null && contacts.Any())
            {
                List<ContactbookDto> contactDtos = new List<ContactbookDto>();
                foreach (var contact in contacts)
                {
                    contactDtos.Add(
                        new ContactbookDto()
                        {
                           ContactId = contact.ContactId,
                           Name = contact.Name,
                           Email = contact.Email,
                           PhoneNumber = contact.PhoneNumber,
                           Company = contact.Company,
                           FileName = contact.FileName,
                           File = contact.File,
                           BirthDate = contact.BirthDate,
                           Gender = contact.Gender,
                           Favourite = contact.Favourite,
                           CountryId = contact.CountryId,
                           StateId = contact.StateId,
                           Country = new CountryDto
                           {
                               CountryId = contact.CountryId,
                               CountryName = contact.Country.CountryName,
                           },
                           State = new StateDto
                           {
                               StateId = contact.StateId,
                               StateName = contact.State.StateName,
                           }
                        });
                }
                response.Data = contactDtos;

            }
            else
            {
                response.Success = false;
                response.Message = "No record found !.";
            }

            return response;
        }

        public ServiceResponse<IEnumerable<ContactbookDto>> GetAllFavouriteContacts()
        {
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>();
            var contacts = _contactbookRepository.GetAllFavourite();

            if (contacts != null && contacts.Any())
            {
                List<ContactbookDto> contactDtos = new List<ContactbookDto>();
                foreach (var contact in contacts)
                {
                    contactDtos.Add(
                        new ContactbookDto()
                        {
                            ContactId = contact.ContactId,
                            Name = contact.Name,
                            Email = contact.Email,
                            PhoneNumber = contact.PhoneNumber,
                            Company = contact.Company,
                            FileName = contact.FileName,
                            File = contact.File,
                            BirthDate = contact.BirthDate,
                            Gender = contact.Gender,
                            Favourite = contact.Favourite,
                            CountryId = contact.CountryId,
                            StateId = contact.StateId,
                            Country = new CountryDto
                            {
                                CountryId = contact.CountryId,
                                CountryName = contact.Country.CountryName,
                            },
                            State = new StateDto
                            {
                                StateId = contact.StateId,
                                StateName = contact.State.StateName,
                            }
                        });
                }
                response.Data = contactDtos;

            }
            else
            {
                response.Success = false;
                response.Message = "No record found !.";
            }

            return response;
        }
        public ServiceResponse<IEnumerable<ContactbookDto>> GetAllSpecificContact(string letter)
        {
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>();
            var contacts = _contactbookRepository.GetSpecificContact(letter);

            if (contacts != null && contacts.Any())
            {
                List<ContactbookDto> contactDtos = new List<ContactbookDto>();
                foreach (var contact in contacts)
                {
                    contactDtos.Add(
                        new ContactbookDto()
                        {
                           ContactId = contact.ContactId,
                           Name = contact.Name,
                           Email = contact.Email,
                           PhoneNumber = contact.PhoneNumber,
                           Company = contact.Company,
                            FileName = contact.FileName,
                            File = contact.File,
                            BirthDate = contact.BirthDate,
                            Gender = contact.Gender,
                            Favourite = contact.Favourite,
                            CountryId = contact.CountryId,
                            StateId = contact.StateId,
                            Country = new CountryDto
                            {
                                CountryId = contact.CountryId,
                                CountryName = contact.Country.CountryName,
                            },
                            State = new StateDto
                            {
                                StateId = contact.StateId,
                                StateName = contact.State.StateName,
                            }
                        });
                }
                response.Data = contactDtos;

            }
            else
            {
                //response.Success = false;
                response.Message = "No record found !.";
            }

            return response;
        }

        public ServiceResponse<int> TotalContacts(string? search)
        {
            var response = new ServiceResponse<int>();

            var result = _contactbookRepository.TotalContacts(search);
            response.Data = result;
            response.Success = true;

            return response;
        }

     

        public ServiceResponse<IEnumerable<ContactbookDto>> GetPaginatedContacts(int page, int pageSize, string sortOrder,string? search)
        {
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>();
            var contacts = _contactbookRepository.GetPaginatedContacts(page, pageSize,sortOrder,search);

            if (contacts != null && contacts.Any())
            {

                List<ContactbookDto> contactDtos = new List<ContactbookDto>();
                foreach (var contact in contacts)
                {
                    contactDtos.Add(
                        new ContactbookDto()
                        {
                            ContactId = contact.ContactId,
                            Name = contact.Name,
                            Email = contact.Email,
                            PhoneNumber = contact.PhoneNumber,
                            Company = contact.Company,
                            FileName = contact.FileName,
                            File = contact.File,
                            BirthDate = contact.BirthDate,
                            Gender = contact.Gender,
                            Favourite = contact.Favourite,
                            CountryId = contact.CountryId,
                            StateId = contact.StateId,
                            Country = new CountryDto
                            {
                                CountryId = contact.CountryId,
                                CountryName = contact.Country.CountryName,
                            },
                            State = new StateDto
                            {
                                StateId = contact.StateId,
                                StateName = contact.State.StateName,
                            }
                        });
                }
                response.Data = contactDtos;
                response.Success = true;
                response.Message = "Success";
            }
            else
            {
                //response.Success = false;
                response.Message = "No record found !.";
            }

            return response;
        }

        public ServiceResponse<int> TotalFavouriteContacts()
        {
            var response = new ServiceResponse<int>();

            var result = _contactbookRepository.TotalFavouriteContacts();
            response.Data = result;
            response.Success = true;

            return response;
        }

        public ServiceResponse<IEnumerable<ContactbookDto>> GetPaginatedFavouriteContacts(int page, int pageSize, string sortOrder)
        {
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>();
            var contacts = _contactbookRepository.GetPaginatedFavouriteContacts(page, pageSize,sortOrder);

            if (contacts != null && contacts.Any())
            {
              
                List<ContactbookDto> contactDtos = new List<ContactbookDto>();
                foreach (var contact in contacts)
                {
                    contactDtos.Add(
                        new ContactbookDto()
                        {
                            ContactId = contact.ContactId,
                            Name = contact.Name,
                            Email = contact.Email,
                            PhoneNumber = contact.PhoneNumber,
                            Company = contact.Company,
                            FileName = contact.FileName,
                            File = contact.File,
                            BirthDate = contact.BirthDate,
                            Gender = contact.Gender,
                            Favourite = contact.Favourite,
                            CountryId = contact.CountryId,
                            StateId = contact.StateId,
                            Country = new CountryDto
                            {
                                CountryId = contact.CountryId,
                                CountryName = contact.Country.CountryName,
                            },
                            State = new StateDto
                            {
                                StateId = contact.StateId,
                                StateName = contact.State.StateName,
                            }
                        });
                }
                response.Data = contactDtos;
                response.Success = true;
                response.Message = "Success";
            }
            else
            {
                //response.Success = false;
                response.Message = "No record found !.";
            }

            return response;
        }
        public ServiceResponse<int> TotalSpecificContacts(string letter ,string? search)
        {
            var response = new ServiceResponse<int>();

            var result = _contactbookRepository.TotalSpecificContacts(letter,search);
            response.Data = result;
            response.Success = true;

            return response;
        }
        public ServiceResponse<IEnumerable<ContactbookDto>> GetPaginatedContactsWithLetter(int page, int pageSize, string letter, string sortOrder, string? search)
        {
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>();
            var contacts = _contactbookRepository.GetPaginatedContactsWithLetter(page, pageSize,letter,sortOrder,search);

            if (contacts != null && contacts.Any())
            {
              
                List<ContactbookDto> contactDtos = new List<ContactbookDto>();
                foreach (var contact in contacts)
                {
                    contactDtos.Add(
                        new ContactbookDto()
                        {
                            ContactId = contact.ContactId,
                            Name = contact.Name,
                            Email = contact.Email,
                            PhoneNumber = contact.PhoneNumber,
                            Company = contact.Company,
                            FileName = contact.FileName,
                            File = contact.File,
                            BirthDate = contact.BirthDate,
                            Gender = contact.Gender,
                            Favourite = contact.Favourite,
                            CountryId = contact.CountryId,
                            StateId = contact.StateId,
                            Country = new CountryDto
                            {
                                CountryId = contact.CountryId,
                                CountryName = contact.Country.CountryName,
                            },
                            State = new StateDto
                            {
                                StateId = contact.StateId,
                                StateName = contact.State.StateName,
                            }
                        });
                }
                response.Data = contactDtos;
                response.Success = true;
                response.Message = "Success";
            }
            else
            {
                //response.Success = false;
                response.Message = "No record found !.";
            }

            return response;
        }

        public ServiceResponse<int> TotalFavouriteSpecificContacts(string letter)
        {
            var response = new ServiceResponse<int>();

            var result = _contactbookRepository.TotalSpecificFavouriteContacts(letter);
            response.Data = result;
            response.Success = true;

            return response;
        }
        public ServiceResponse<IEnumerable<ContactbookDto>> GetPaginatedFavouriteContactsWithLetter(int page, int pageSize, string letter, string sortOrder)
        {
            var response = new ServiceResponse<IEnumerable<ContactbookDto>>();
            var contacts = _contactbookRepository.GetPaginatedFavouriteContactsWithLetter(page, pageSize, letter, sortOrder);

            if (contacts != null && contacts.Any())
            {
             
                List<ContactbookDto> contactDtos = new List<ContactbookDto>();
                foreach (var contact in contacts)
                {
                    contactDtos.Add(
                        new ContactbookDto()
                        {
                            ContactId = contact.ContactId,
                            Name = contact.Name,
                            Email = contact.Email,
                            PhoneNumber = contact.PhoneNumber,
                            Company = contact.Company,
                            FileName = contact.FileName,
                            File = contact.File,
                            BirthDate = contact.BirthDate,
                            Gender = contact.Gender,
                            Favourite = contact.Favourite,
                            CountryId = contact.CountryId,
                            StateId = contact.StateId,
                            Country = new CountryDto
                            {
                                CountryId = contact.CountryId,
                                CountryName = contact.Country.CountryName,
                            },
                            State = new StateDto
                            {
                                StateId = contact.StateId,
                                StateName = contact.State.StateName,
                            }
                        });
                }
                response.Data = contactDtos;
                response.Success = true;
                response.Message = "Success";
            }
            else
            {
                //response.Success = false;
                response.Message = "No record found !.";
            }

            return response;
        }

        public ServiceResponse<string> AddContact(Contactbook contact)
        {
            var response = new ServiceResponse<string>();
            if (_contactbookRepository.ContactExists(contact.PhoneNumber))
            {
                response.Success = false;
                response.Message = "Contact already exists.";
                return response;
            }

            if (contact.File == null)
            {
                contact.File = new byte[0];
            }

            if (contact.FileName == null)
            {
                contact.FileName = string.Empty ;
            }

            if (contact.BirthDate > DateTime.Now)
            {
                response.Success = false;
                response.Message = "Enter valid date";
                return response;
            }

            var result = _contactbookRepository.InsertContact(contact);

            if (result)
            {
                response.Message = "Contact saved successfully.";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong. Please try after sometime.";
            }
            return response;
        }

        public ServiceResponse<ContactbookDto> GetContactsById(int id)
        {
            var response = new ServiceResponse<ContactbookDto>();
            var contact = _contactbookRepository.GetContacts(id);

            if (contact != null)
            {
               
               var contactDto = new ContactbookDto()
               {
                   ContactId = contact.ContactId,
                   Name = contact.Name,
                   Email = contact.Email,
                   PhoneNumber = contact.PhoneNumber,
                   Company = contact.Company,
                   FileName = contact.FileName,
                   File = contact.File,
                   BirthDate = contact.BirthDate,
                   Gender = contact.Gender,
                   Favourite = contact.Favourite,
                   CountryId = contact.CountryId,
                   StateId = contact.StateId,
                   Country = new CountryDto
                   {
                       CountryId = contact.CountryId,
                       CountryName = contact.Country.CountryName,
                   },
                   State = new StateDto
                   {
                       StateId = contact.StateId,
                       StateName = contact.State.StateName,
                   }
               };
                
                response.Data = contactDto;

            }
            else
            {
                response.Success = false;
                response.Message = "No record found !.";
            }

            return response;
        }

        public ServiceResponse<string> ModifyContact(Contactbook contact)
        {
            var response = new ServiceResponse<string>();

            if (_contactbookRepository.ContactExists(contact.ContactId, contact.PhoneNumber))
            {
                response.Success = false;
                response.Message = "Contact already exists.";
                return response;

            }

            if(contact.File == null)
            {
                contact.File = new byte[0];
            }

            if (contact.FileName == null)
            {
                contact.FileName = string.Empty;
            }

            if (contact.BirthDate > DateTime.Now)
            {
                response.Success = false;
                response.Message = "Enter valid date";
                return response;
            }


            var existingContact = _contactbookRepository.GetContacts(contact.ContactId);
            var result = false;

            if (existingContact != null)
            {

                existingContact.Name = contact.Name;
                existingContact.Email = contact.Email;
                existingContact.PhoneNumber = contact.PhoneNumber;
                existingContact.Company = contact.Company;
                existingContact.FileName = contact.FileName;
                existingContact.File = contact.File;
                existingContact.BirthDate = contact.BirthDate;
                existingContact.Gender = contact.Gender;
                existingContact.Favourite = contact.Favourite;
                existingContact.CountryId = contact.CountryId;
                existingContact.StateId = contact.StateId;

                
                result = _contactbookRepository.UpdateContact(existingContact);
            }

            if (result)
            {
                response.Message = "Contact updated successfully.";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong. Please try afte sometime.";
            }

            return response;
        }

        public ServiceResponse<string> RemoveContact(int id)
        {

            var response = new ServiceResponse<string>();

            var result = _contactbookRepository.DeleteContact(id);
            if (result)
            {
                response.Message = "Contact deleted successfully.";

            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong. Please try afte sometime.";
            }

            return response;

        }

    }
}
