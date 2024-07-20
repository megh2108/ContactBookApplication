using ApiContactbookApplication.Data.Contract;
using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ApiContactbookApplication.Data.Implementation
{
    public class ContactbookRepository : IContactbookRepository
    {
        private readonly IAppDbContext _appDbContext;

        public ContactbookRepository(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        //sp
        public IEnumerable<ContactbookDtoSP> GetPaginatedContactsSP(string? search, int page, int pageSize, string sortOrder)
        {
            var results = _appDbContext.GetPaginatedContactsSP(search,page, pageSize, sortOrder);

            return results.ToList();
        }  
        public IEnumerable<ContactbookDtoSP> GetAllContactsByBirthdayMonth(int month)
        {
            var results = _appDbContext.GetAllContactsByBirthdayMonth(month);

            return results.ToList();
        } 
        
        public IEnumerable<ContactbookDtoSP> GetAllContactsByStates(int state)
        {
            var results = _appDbContext.GetAllContactsByStates(state);

            return results.ToList();
        }

        public int GetAllContactsCountByCountry(int country)
        {
            var results = _appDbContext.GetAllContactsCountByCountry(country);

            return results;
        }
          public int GetAllContactsCountByGender(string gender)
        {
            var results = _appDbContext.GetAllContactsCountByGender(gender);

            return results;
        }


        //
        public IEnumerable<Contactbook> GetAll()
        {
            List<Contactbook> contacts = _appDbContext.Contactbooks.Include(c => c.Country).Include(c => c.State).OrderBy(c => c.Name).ToList();
            return contacts;
        }  
        public IEnumerable<Contactbook> GetAllFavourite()
        {
            List<Contactbook> contacts = _appDbContext.Contactbooks.Where(c => c.Favourite == true).Include(c => c.Country).Include(c => c.State).OrderBy(c => c.Name).ToList();
            return contacts;
        }

        public IEnumerable<Contactbook> GetSpecificContact(string letter)
        {
            List<Contactbook> contacts = _appDbContext.Contactbooks.Include(c => c.Country).Include(c => c.State).Where(c => c.Name.StartsWith(letter.ToString())).ToList();

            return contacts;

        }

        public int TotalContacts(string? search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return _appDbContext.Contactbooks.Where(c => c.Name.Contains(search)).Count();
            }
            else
            {
                return _appDbContext.Contactbooks.Count();
            }
        }


        public IEnumerable<Contactbook> GetPaginatedContacts(int page, int pageSize, string sortOrder, string? search)
        {

            int skip = (page - 1) * pageSize;

            IQueryable<Contactbook> paginatedContacts = _appDbContext.Contactbooks.Include(c => c.Country).Include(c => c.State);

            if (!string.IsNullOrEmpty(search))
            {
                paginatedContacts = paginatedContacts.Where(c => c.Name.Contains(search));
            }

            if (sortOrder == "asc")
            {
                paginatedContacts = paginatedContacts.OrderBy(c => c.Name);
            }
            else if (sortOrder == "desc")
            {
                paginatedContacts = paginatedContacts.OrderByDescending(c => c.Name);
            }
            else
            {
                throw new ArgumentException("Invalid sorting order");
            }

            return paginatedContacts.Skip(skip).Take(pageSize).ToList();
        }
        public int TotalFavouriteContacts()
        {
            return _appDbContext.Contactbooks.Where(c => c.Favourite == true).Count();

        }
        public IEnumerable<Contactbook> GetPaginatedFavouriteContacts(int page, int pageSize, string sortOrder)
        {

            int skip = (page - 1) * pageSize;

            IQueryable<Contactbook> paginatedContacts = _appDbContext.Contactbooks.Include(c => c.Country).Include(c => c.State).Where(c => c.Favourite == true);

            if (sortOrder == "asc")
            {
                paginatedContacts = paginatedContacts.OrderBy(c => c.Name);
            }
            else if (sortOrder == "desc")
            {
                paginatedContacts = paginatedContacts.OrderByDescending(c => c.Name);
            }
            else
            {
                throw new ArgumentException("Invalid sorting order");
            }

            return paginatedContacts.Skip(skip).Take(pageSize).ToList();
        }


        public int TotalSpecificContacts(string letter, string? search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return _appDbContext.Contactbooks.Where(c => c.Name.StartsWith(letter.ToString()) && c.Name.Contains(search)).Count();
            }
            else
            {

                return _appDbContext.Contactbooks.Where(c => c.Name.StartsWith(letter.ToString())).Count();
            }

        }
        public IEnumerable<Contactbook> GetPaginatedContactsWithLetter(int page, int pageSize, string letter, string sortOrder, string? search)
        {
            int skip = (page - 1) * pageSize;

            IQueryable<Contactbook> paginatedContacts = _appDbContext.Contactbooks.Include(c => c.Country).Include(c => c.State).Where(c => c.Name.StartsWith(letter.ToString()));

            if (!string.IsNullOrEmpty(search))
            {
                paginatedContacts = paginatedContacts.Where(c => c.Name.Contains(search));
            }

            if (sortOrder == "asc")
            {
                paginatedContacts = paginatedContacts.OrderBy(c => c.Name);
            }
            else if (sortOrder == "desc")
            {
                paginatedContacts = paginatedContacts.OrderByDescending(c => c.Name);
            }
            else
            {
                throw new ArgumentException("Invalid sorting order");
            }

            return paginatedContacts.Skip(skip).Take(pageSize).ToList();

        }

        public int TotalSpecificFavouriteContacts(string letter)
        {
            return _appDbContext.Contactbooks.Where(c => c.Name.StartsWith(letter.ToString()) && c.Favourite == true).Count();

        }
        public IEnumerable<Contactbook> GetPaginatedFavouriteContactsWithLetter(int page, int pageSize, string letter, string sortOrder)
        {
            int skip = (page - 1) * pageSize;

            IQueryable<Contactbook> paginatedContacts = _appDbContext.Contactbooks.Include(c => c.Country).Include(c => c.State).Where(c => c.Name.StartsWith(letter.ToString()) && c.Favourite == true);

            if (sortOrder == "asc")
            {
                paginatedContacts = paginatedContacts.OrderBy(c => c.Name);
            }
            else if (sortOrder == "desc")
            {
                paginatedContacts = paginatedContacts.OrderByDescending(c => c.Name);
            }
            else
            {
                throw new ArgumentException("Invalid sorting order");
            }

            return paginatedContacts.Skip(skip).Take(pageSize).ToList();
        }


        public bool InsertContact(Contactbook contact)
        {
            var result = false;
            if (contact != null)
            {
                _appDbContext.Contactbooks.Add(contact);
                _appDbContext.SaveChanges();


                result = true;

            }
            return result;
        }

        public Contactbook? GetContacts(int id)
        {
            var contacts = _appDbContext.Contactbooks.Include(c => c.Country).Include(c => c.State).FirstOrDefault(c => c.ContactId == id);

            return contacts;
        }

        public bool UpdateContact(Contactbook contact)
        {
            var result = false;
            if (contact != null)
            {
                _appDbContext.Contactbooks.Update(contact);
                _appDbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public bool DeleteContact(int id)
        {
            var result = false;
            var contact = _appDbContext.Contactbooks.Find(id);
            if (contact != null)
            {
                _appDbContext.Contactbooks.Remove(contact);
                _appDbContext.SaveChanges();
                return true;
            }

            return result;
        }


        //exists

        public bool ContactExists(string numbar)
        {
            var contact = _appDbContext.Contactbooks.FirstOrDefault(c => c.PhoneNumber == numbar);

            if (contact != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ContactExists(int contactId, string numbar)
        {
            var contact = _appDbContext.Contactbooks.FirstOrDefault(c => c.ContactId != contactId && c.PhoneNumber == numbar);

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
