using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Models;
using ApiContactbookApplication.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiContactbookApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactbookController : ControllerBase
    {
        private readonly IContactbookService _contactbookService;

        public ContactbookController(IContactbookService contactbookService)
        {
            _contactbookService = contactbookService;
        }

        //sp

        [HttpGet("GetAllContactsByPaginationSP")]
        public IActionResult GetAllContactsByPaginationSP(string? search, int page = 1, int pageSize = 2, string sortOrder = "asc")
        {
            var response = _contactbookService.GetPaginatedContactsSP(search, page, pageSize, sortOrder);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        } 
        
        [HttpGet("GetAllContactsByBirthdayMonthSP/{month}")]
        public IActionResult GetAllContactsByBirthdayMonthSP(int month)
        {
            var response = _contactbookService.GetAllContactsByBirthdayMonth(month);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("GetAllContactsByStatesSP/{state}")]
        public IActionResult GetAllContactsByStatesSP(int state)
        {
            var response = _contactbookService.GetAllContactsByStates(state);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("GetAllContactsCountByCountrySP/{country}")]
        public IActionResult GetAllContactsCountByCountrySP(int country)
        {
            var response = _contactbookService.GetAllContactsCountByCountry(country);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        } 
        
        [HttpGet("GetAllContactsCountByGenderSP/{gender}")]
        public IActionResult GetAllContactsCountByGenderSP(string gender)
        {
            var response = _contactbookService.GetAllContactsCountByGender(gender);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        //

        [HttpGet("GetAllSpecificContact")]
        public IActionResult GetAllSpecificContact(string letter)
        {
            var response = _contactbookService.GetAllSpecificContact(letter);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("GetAllContacts")]
        public IActionResult GetAllContacts()
        {
            var response = _contactbookService.GetAllContacts();

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        } 
        
        [HttpGet("GetAllFavouriteContacts")]
        public IActionResult GetAllFavouriteContacts()
        {
            var response = _contactbookService.GetAllFavouriteContacts();

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("GetContactsById")]
        public IActionResult GetContactsById(int id)
        {
            var response = _contactbookService.GetContactsById(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        [HttpGet("GetContactsCount")]
        public IActionResult GetContactsCount(string? search)
        {
            var response = _contactbookService.TotalContacts(search);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        [HttpGet("GetAllContactsByPagination")]
        public IActionResult GetAllContactsByPagination(string? search,int page = 1, int pageSize = 2, string sortOrder = "asc")
        {
            var response = _contactbookService.GetPaginatedContacts(page, pageSize, sortOrder, search);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("GetFavouriteContactsCount")]
        public IActionResult GetFavouriteContactsCount()
        {
            var response = _contactbookService.TotalFavouriteContacts();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("GetAllFavouriteContactsByPagination")]
        public IActionResult GetAllFavouriteContactsByPagination(int page = 1, int pageSize = 2, string sortOrder = "asc")
        {
            var response = _contactbookService.GetPaginatedFavouriteContacts(page, pageSize, sortOrder);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpGet("GetSpecificContactsCount")]
        public IActionResult GetSpecificContactsCount(string letter, string? search)
        {
            var response = _contactbookService.TotalSpecificContacts(letter, search);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("GetSpecificContactsByPaginationWithLetter")]
        public IActionResult GetSpecificContactsByPaginationWithLetter(string? search, int page = 1, int pageSize = 2, string letter = "a", string sortOrder = "asc")
        {
            var response = _contactbookService.GetPaginatedContactsWithLetter(page, pageSize, letter, sortOrder, search);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }



        [HttpGet("GetSpecificFavouriteContactsCount")]
        public IActionResult GetSpecificFavouriteContactsCount(string letter)
        {
            var response = _contactbookService.TotalFavouriteSpecificContacts(letter);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("GetSpecificFavouriteContactsByPaginationWithLetter")]
        public IActionResult GetSpecificFavouriteContactsByPaginationWithLetter(int page = 1, int pageSize = 2, string letter = "a", string sortOrder = "asc")
        {
            var response = _contactbookService.GetPaginatedFavouriteContactsWithLetter(page, pageSize, letter, sortOrder);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [Authorize]
        [HttpPost("AddContact")]

        public IActionResult AddContact(AddContactbookDto contactDto)
        {

            var contact = new Contactbook()
            {
                Name = contactDto.Name,
                Email = contactDto.Email,
                PhoneNumber = contactDto.PhoneNumber,
                Company = contactDto.Company,
                FileName = contactDto.FileName,
                File = contactDto.File,
                BirthDate = contactDto.BirthDate,
                Gender = contactDto.Gender,
                Favourite = contactDto.Favourite,
                CountryId = contactDto.CountryId,
                StateId = contactDto.StateId,
            };

            var result = _contactbookService.AddContact(contact);
            return !result.Success ? BadRequest(result) : Ok(result);


        }

        [Authorize]
        [HttpPut("ModifyContact")]
        public IActionResult UpdateContact(UpdateContactbookDto contactDto)
        {
            var contact = new Contactbook()
            {
                ContactId = contactDto.ContactId,
                Name = contactDto.Name,
                Email = contactDto.Email,
                PhoneNumber = contactDto.PhoneNumber,
                Company = contactDto.Company,
                FileName = contactDto.FileName,
                File = contactDto.File,
                BirthDate = contactDto.BirthDate,
                Gender = contactDto.Gender,
                Favourite = contactDto.Favourite,
                CountryId = contactDto.CountryId,
                StateId = contactDto.StateId,


            };

            var response = _contactbookService.ModifyContact(contact);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [Authorize]
        [HttpDelete("RemoveContact/{contactId}")]
        public IActionResult RemoveContact(int contactId)
        {

            if (contactId > 0)
            {

                var response = _contactbookService.RemoveContact(contactId);
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            else
            {
                return BadRequest("Please enter proper data.");
            }
        }

    }
}
