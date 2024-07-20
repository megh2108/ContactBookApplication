using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Models;

namespace ApiContactbookApplication.Services.Contract
{
    public interface IContactbookService
    {

        //sp
        ServiceResponse<IEnumerable<ContactbookDtoSP>> GetPaginatedContactsSP(string? search,int page, int pageSize, string sortOrder);

        ServiceResponse<IEnumerable<ContactbookDtoSP>> GetAllContactsByBirthdayMonth(int month);

        ServiceResponse<IEnumerable<ContactbookDtoSP>> GetAllContactsByStates(int state);

        ServiceResponse<int> GetAllContactsCountByCountry(int country);

        ServiceResponse<int> GetAllContactsCountByGender(string gender);

        //
        ServiceResponse<IEnumerable<ContactbookDto>> GetAllContacts();
        ServiceResponse<IEnumerable<ContactbookDto>> GetAllFavouriteContacts();
        ServiceResponse<IEnumerable<ContactbookDto>> GetAllSpecificContact(string letter);

        ServiceResponse<int> TotalContacts(string? search);

         ServiceResponse<IEnumerable<ContactbookDto>> GetPaginatedContacts(int page, int pageSize, string sortOrder, string? search);

        ServiceResponse<int> TotalFavouriteContacts();

        ServiceResponse<IEnumerable<ContactbookDto>> GetPaginatedFavouriteContacts(int page, int pageSize, string sortOrder);
        ServiceResponse<int> TotalSpecificContacts(string letter, string? search);

        ServiceResponse<IEnumerable<ContactbookDto>> GetPaginatedContactsWithLetter(int page, int pageSize, string letter, string sortOrder, string? search);

        ServiceResponse<int> TotalFavouriteSpecificContacts(string letter);
        ServiceResponse<IEnumerable<ContactbookDto>> GetPaginatedFavouriteContactsWithLetter(int page, int pageSize, string letter, string sortOrder);

        ServiceResponse<string> AddContact(Contactbook contact);

        ServiceResponse<ContactbookDto> GetContactsById(int id);

        ServiceResponse<string> ModifyContact(Contactbook contact);

        ServiceResponse<string> RemoveContact(int id);
    }
}
