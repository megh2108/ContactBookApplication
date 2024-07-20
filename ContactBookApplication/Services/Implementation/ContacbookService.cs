using ContactBookApplication.Data.Contract;
using ContactBookApplication.Models;
using ContactBookApplication.Services.Contract;

namespace ContactBookApplication.Services.Implementation
{
    public class ContacbookService : IContacbookService
    {
        private readonly IContactbookRepository _contactbookRepository;

        public ContacbookService(IContactbookRepository contactbookRepository)
        {
            _contactbookRepository = contactbookRepository;
        }

        public IEnumerable<Contactbook> GetAllContacts()
        {
            var contacts = _contactbookRepository.GetAll();

            if (contacts != null && contacts.Any())
            {
                foreach (var contact in contacts.Where(c => c.FileName == string.Empty))
                {
                    contact.FileName = "defaultimage.png";
                }
                return contacts;
            }
            return new List<Contactbook>();
        }

        public IEnumerable<Contactbook> GetAllSpecificContact(string letter)
        {
            var contacts = _contactbookRepository.GetSpecificContact(letter);

            if (contacts != null && contacts.Any())
            {
                foreach (var contact in contacts.Where(c => c.FileName == string.Empty))
                {
                    contact.FileName = "defaultimage.png";
                }
                return contacts;
            }
            return new List<Contactbook>();
        }

        public int TotalContacts()
        {
            return _contactbookRepository.TotalContacts();
        }

        public IEnumerable<Contactbook> GetPaginatedContacts(int page, int pageSize)
        {
           var contacts = _contactbookRepository.GetPaginatedContacts(page, pageSize);

            if (contacts != null && contacts.Any())
            {
                foreach (var contact in contacts.Where(c => c.FileName == string.Empty))
                {
                    contact.FileName = "defaultimage.png";
                }
                return contacts;
            }
            return new List<Contactbook>();
        }

        public string AddContact(Contactbook contact, IFormFile file)
        {
            if (_contactbookRepository.ContactExists(contact.Name))
            {
                return "Contact already exists.";
            }
            var fileName = string.Empty;

            if (file != null && file.Length > 0)
            {
                //Process the uploaded file (eg. save it to disk)
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file.FileName);

                //save the file to storage and set path

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    fileName = file.FileName;
                }

                contact.FileName = fileName;
            }
            var result = _contactbookRepository.InsertContact(contact);
            return result ? "Contact saved successfully." : "Something went wrong. Please try after sometime.";
        }


        public Contactbook? GetContacts(int id)
        {
            var contact = _contactbookRepository.GetContacts(id);

            return contact;
        }


        public string ModifyContact(Contactbook contact)
        {
            var message = string.Empty;
            if (_contactbookRepository.ContactExists(contact.ContactId, contact.Name))
            {
                message = "Contact already exists.";
            }

            var existingContact = _contactbookRepository.GetContacts(contact.ContactId);
            var result = false;

            if (existingContact != null)
            {

                existingContact.Name = contact.Name;
                existingContact.Email = contact.Email;
                existingContact.PhoneNumber = contact.PhoneNumber;
                existingContact.Company = contact.Company;
                result = _contactbookRepository.UpdateContact(existingContact);
            }

            message = result ? "Contact updated successfully." : "Something went wrong. Please try afte sometime.";

            return message;
        }

        public string RemoveContact(int id)
        {
            var result = _contactbookRepository.DeleteContact(id);
            if (result)
            {
                return "Contact deleted successfully.";
            }
            else
            {
                return "Something went wrong, please try again later";
            }
        }
    }
}
