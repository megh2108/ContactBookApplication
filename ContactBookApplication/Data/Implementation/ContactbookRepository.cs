using ContactBookApplication.Data.Contract;
using ContactBookApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace ContactBookApplication.Data.Implementation
{
    public class ContactbookRepository : IContactbookRepository
    {
        private readonly AppDbContext _appDbContext;

        public ContactbookRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Contactbook> GetAll()
        {
            List<Contactbook> contacts = _appDbContext.Contacts.OrderBy(c => c.Name).ToList();
            return contacts;
        }

        public IEnumerable<Contactbook> GetSpecificContact(string letter)
        {
            List<Contactbook> contacts = _appDbContext.Contacts.Where(c => c.Name.StartsWith(letter.ToString())).ToList();

            return contacts;

        }

        public int TotalContacts()
        {
            return _appDbContext.Contacts.Count();
        }
        public IEnumerable<Contactbook> GetPaginatedContacts(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            List<Contactbook> paginatedContacts = _appDbContext.Contacts.OrderBy(c => c.Name)
                                                    .Skip(skip).Take(pageSize).ToList();
            return paginatedContacts;
        }

        public bool InsertContact(Contactbook contact)
        {
            var result = false;
            if (contact != null)
            {
                _appDbContext.Contacts.Add(contact);
                _appDbContext.SaveChanges();


                result = true;

            }
            return result;
        }

        public Contactbook? GetContacts(int id)
        {
            var contacts = _appDbContext.Contacts.FirstOrDefault(c => c.ContactId == id);

            return contacts;
        }

        public bool UpdateContact(Contactbook contact)
        {
            var result = false;
            if (contact != null)
            {
                _appDbContext.Contacts.Update(contact);
                _appDbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public bool DeleteContact(int id)
        {
            var result = false;
            var contact = _appDbContext.Contacts.Find(id);
            if (contact != null)
            {
                _appDbContext.Contacts.Remove(contact);
                _appDbContext.SaveChanges();
                return true;
            }

            return result;
        }


        //exists

        public bool ContactExists(string name)
        {
            var contact = _appDbContext.Contacts.FirstOrDefault(c => c.Name == name);

            if (contact != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ContactExists(int contactId, string name)
        {
            var contact = _appDbContext.Contacts.FirstOrDefault(c => c.ContactId != contactId && c.Name == name);

            if (contact != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
