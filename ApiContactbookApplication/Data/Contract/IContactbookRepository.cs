using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Models;

namespace ApiContactbookApplication.Data.Contract
{
    public interface IContactbookRepository
    {

        //sp
        IEnumerable<ContactbookDtoSP> GetPaginatedContactsSP(string? search, int page, int pageSize, string sortOrder);

        IEnumerable<ContactbookDtoSP> GetAllContactsByBirthdayMonth(int month);

        IEnumerable<ContactbookDtoSP> GetAllContactsByStates(int state);

        int GetAllContactsCountByCountry(int country);

        int GetAllContactsCountByGender(string gender);

        //
        IEnumerable<Contactbook> GetAll();

        IEnumerable<Contactbook> GetAllFavourite();

        IEnumerable<Contactbook> GetSpecificContact(string letter);
        int TotalContacts(string? search);

        IEnumerable<Contactbook> GetPaginatedContacts(int page, int pageSize, string sortOrder,string? search);

        int TotalFavouriteContacts();
        IEnumerable<Contactbook> GetPaginatedFavouriteContacts(int page, int pageSize, string sortOrder);

        int TotalSpecificContacts(string letter, string? search);

        IEnumerable<Contactbook> GetPaginatedContactsWithLetter(int page, int pageSize, string letter, string sortOrder, string? search);

        int TotalSpecificFavouriteContacts(string letter);
        IEnumerable<Contactbook> GetPaginatedFavouriteContactsWithLetter(int page, int pageSize, string letter, string sortOrder);
        bool InsertContact(Contactbook contact);

        Contactbook? GetContacts(int id);

        bool ContactExists(string name);

        bool ContactExists(int contactId, string name);

        bool UpdateContact(Contactbook contact);

        bool DeleteContact(int id);
    }
}
