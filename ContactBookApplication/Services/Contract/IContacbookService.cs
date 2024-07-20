using ContactBookApplication.Models;
using ContactBookApplication.ViewModels;

namespace ContactBookApplication.Services.Contract
{
    public interface IContacbookService
    {
        IEnumerable<Contactbook> GetAllContacts();

        IEnumerable<Contactbook> GetAllSpecificContact(string letter);

        int TotalContacts();

        IEnumerable<Contactbook> GetPaginatedContacts(int page, int pageSize);
        string AddContact(Contactbook contact, IFormFile file);

        Contactbook? GetContacts(int id);

        string ModifyContact(Contactbook contact);

        string RemoveContact(int id);
    }
}
