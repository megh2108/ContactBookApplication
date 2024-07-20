using ContactBookApplication.Models;

namespace ContactBookApplication.Data.Contract
{
    public interface IContactbookRepository
    {

        IEnumerable<Contactbook> GetAll();

        IEnumerable<Contactbook> GetSpecificContact(string letter);
        int TotalContacts();

        IEnumerable<Contactbook> GetPaginatedContacts(int page, int pageSize);
        bool InsertContact(Contactbook contact);

        Contactbook? GetContacts(int id);

        bool ContactExists(string name);

        bool ContactExists(int contactId, string name);

        bool UpdateContact(Contactbook contact);

        bool DeleteContact(int id);
    }
}
