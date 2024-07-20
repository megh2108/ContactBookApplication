using ContactBookApplication.Data;
using ContactBookApplication.Models;
using ContactBookApplication.Services.Contract;
using ContactBookApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Linq;

namespace ContactBookApplication.Controllers
{
   
    public class ContactbookController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IContacbookService _contacbookService;

        public ContactbookController(AppDbContext appDbContext, IContacbookService contacbookService)
        {
            _context = appDbContext;
            _contacbookService = contacbookService;
        }
        public IActionResult Index()
        {
            var contacts = _context.Contacts.ToList();
            if (contacts != null && contacts.Any())
            {

                return View(contacts);
            }
            return View(new List<Contactbook>());
        }

        public IActionResult RedirectGroupContact(string letter)
        {
            var contacts = _contacbookService.GetAllSpecificContact(letter);

            if (contacts != null && contacts.Any())
            {
                return View("RedirectGroupContact", contacts);
            }
            return View(new List<Contactbook>());

            //var contact = _context.Contacts.Where(c =>c.Name.StartsWith(letter.ToString())).ToList();

            //if (contact == null)
            //{
            //    return NotFound();
            //}
            //return View(contact);
        }

        public IActionResult ShowAllContact()
        {
            //var contact = _context.Contacts.OrderBy(c => c.Name).ToList();
            var contacts = _contacbookService.GetAllContacts();
            
            if (contacts != null && contacts.Any())
            {
                return View("ShowAllContact",contacts);
            }
            return View(new List<Contactbook>());
        }
        
        public IActionResult ShowAllContactWithPagination(int page = 1, int pageSize = 2)
        {
            ViewBag.CurrentPage = page; // Pass the current page number to the ViewBag
                                        // Get total count of categories
            var totalCount = _contacbookService.TotalContacts();

            // Calculate total number of pages
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            // Get paginated categories
            var contacts = _contacbookService.GetPaginatedContacts(page, pageSize);

            // Set ViewBag properties
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            return View(contacts);
        }


        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            //Contactbook contact = new Contactbook();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContactbookViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                //var
                var contact = new Contactbook()
                {
                    Name = contactViewModel.Name,
                    Email = contactViewModel.Email,
                    PhoneNumber = contactViewModel.PhoneNumber,
                    Company = contactViewModel.Company,
                };
                var result = _contacbookService.AddContact(contact,contactViewModel.File);


                if (result == "Contact already exists." || result == "Something went wrong. Please try after sometime.")
                {
                    TempData["ErrorMessage"] = result;


                }
                else if (result == "Contact saved successfully.")
                {
                    TempData["SuccessMessage"] = result;

                    return RedirectToAction("Index");

                }
            }

            return View(contactViewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var contact = _contacbookService.GetContacts(id);


            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [HttpGet]
        [Authorize]

        public IActionResult Edit(int id)
        {
            var contact = _context.Contacts.Find(id);



            if (contact == null)
            {
                return NotFound();
            }
            return View();
        }


        [HttpPost]
        public IActionResult Edit(Contactbook contact)
        {
            if (ModelState.IsValid)
            {
                var message = _contacbookService.ModifyContact(contact);
                if (message == "Contact already exists." || message == "Something went wrong. Please try afte sometime.")
                {

                    TempData["ErrorMessage"] = message;

                }
                else
                {
                    TempData["SuccessMessage"] = "Contact Updated Successfully.";

                    return RedirectToAction("Index");
                }

            }

            return View(contact);
        }

        [HttpGet]
        [Authorize]

        public IActionResult Delete(int id)
        {
            var contact = _context.Contacts.Find(id);

            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [HttpPost]

        public IActionResult DeleteConfirmed(int contactId)
        {
            var result = _contacbookService.RemoveContact(contactId);

            if (result == "Contact deleted successfully.")
            {
                TempData["SuccessMessage"] = result;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = result;

            }

            return RedirectToAction("Index");
        }



    }
}
