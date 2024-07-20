using ClientApiContactbookApplication.Infrastructure;
using ClientApiContactbookApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace ClientApiContactbookApplication.Controllers
{
    public class ContactbookController : Controller
    {

        private readonly IHttpClientService _httpClientService;
        private readonly IConfiguration _configuration;
        private string endPoint;

        public ContactbookController(IHttpClientService httpClientService, IConfiguration configuration)
        {
            _httpClientService = httpClientService;
            _configuration = configuration;
            endPoint = _configuration["Endpoint:CivicaContactApi"];
        }

        //sp

        public IActionResult ContactReports()
        {

            var countries = GetCountries();
            var states = GetStates();

            ViewBag.Countries = countries;
            ViewBag.States = states;
            return PartialView("_ReportPartialView");
        }

        public IActionResult GetAllContactsCountByCountrySP(int country)
        {
            var apiGetCountUrl = "";

            ServiceResponse<int> count_response = new ServiceResponse<int>();

            if (country >= 0)
            {

                 apiGetCountUrl = $"{endPoint}Contactbook/GetAllContactsCountByCountrySP/{country}";
            }
            else
            {
                apiGetCountUrl = $"{endPoint}Contactbook/GetAllContactsCountByCountrySP";
            }


            count_response = _httpClientService.ExecuteApiRequest<ServiceResponse<int>>(apiGetCountUrl, HttpMethod.Get, HttpContext.Request);

            int totalContactsCount = count_response.Data;


            var countries = GetCountries();
            var states = GetStates();

            ViewBag.Countries = countries;
            ViewBag.States = states;

            ViewBag.Country = country;
            ViewBag.TotalCount = totalContactsCount;




            if (totalContactsCount == 0)
            {
                return View(new List<CountViewModel>());
            }
            else
            {
                if (count_response.Success)
                {
                    return View(new List<CountViewModel>());

                }
            }
          
            return View(new List<CountViewModel>());
        }

        public IActionResult GetAllContactsByStatesSP(int state)
        {
            var apiGetUrl = "";

            ServiceResponse<IEnumerable<ContactbookViewModelSP>> response = new ServiceResponse<IEnumerable<ContactbookViewModelSP>>();

            if (state >= 0)
            {
                apiGetUrl = $"{endPoint}Contactbook/GetAllContactsByStatesSP/{state}";
            }
            else
            {
                apiGetUrl = $"{endPoint}Contactbook/GetAllContactsByStatesSP";
            }


            response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModelSP>>>(apiGetUrl, HttpMethod.Get, HttpContext.Request);


            var countries = GetCountries();
            var states = GetStates();

            ViewBag.Countries = countries;
            ViewBag.States = states;

            ViewBag.State = state;


                if (response.Success)
                {
                    return View(response.Data.ToList());

                }

            return View(new List<ContactbookViewModelSP>());
        }

        public IActionResult GetAllContactsCountByGenderSP(string gender)
        {
            var apiGetCountUrl = "";

            if(gender == null)
            {
                gender = "null";
            }

            ServiceResponse<int> count_response = new ServiceResponse<int>();

            if (gender != null)
            {

                apiGetCountUrl = $"{endPoint}Contactbook/GetAllContactsCountByGenderSP/{gender}";
            }
            else
            {
                apiGetCountUrl = $"{endPoint}Contactbook/GetAllContactsCountByGenderSP";
            }


            count_response = _httpClientService.ExecuteApiRequest<ServiceResponse<int>>(apiGetCountUrl, HttpMethod.Get, HttpContext.Request);

            int totalContactsCount = count_response.Data;

            var countries = GetCountries();
            var states = GetStates();

            ViewBag.Countries = countries;
            ViewBag.States = states;

            ViewBag.Gender = gender;
            ViewBag.TotalCount = totalContactsCount;




            if (totalContactsCount == 0)
            {
                return View(new List<CountViewModel>());
            }
            else
            {
                if (count_response.Success)
                {

                    return View(new List<CountViewModel>());

                }
            }

            return View(new List<CountViewModel>());
        }

        public IActionResult GetAllContactsByBirthdayMonthSP(int month)
        {
            var apiGetUrl = "";

            ServiceResponse<IEnumerable<ContactbookViewModelSP>> response = new ServiceResponse<IEnumerable<ContactbookViewModelSP>>();

            if (month >= 0)
            {

                apiGetUrl = $"{endPoint}Contactbook/GetAllContactsByBirthdayMonthSP/{month}";
            }
            else
            {
                apiGetUrl = $"{endPoint}Contactbook/GetAllContactsByBirthdayMonthSP";
            }


            response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModelSP>>>(apiGetUrl, HttpMethod.Get, HttpContext.Request);

            var countries = GetCountries();
            var states = GetStates();

            ViewBag.Countries = countries;
            ViewBag.States = states;

            ViewBag.Month = month;


                if (response.Success)
                {
                    return View(response.Data.ToList());

                }
               
            return View(new List<ContactbookViewModelSP>());
        }



        //

        //public IActionResult Index()
        //{
        //    return View(new List<ContactbookViewModel>());

        //}

        public IActionResult ShowAllContact()
        {
            ServiceResponse<IEnumerable<ContactbookViewModel>> response = new ServiceResponse<IEnumerable<ContactbookViewModel>>();


            response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>($"{endPoint}Contactbook/GetAllContacts", HttpMethod.Get, HttpContext.Request);

            if (response.Message.Equals("No record found !."))
            {
                return View(new List<ContactbookViewModel>());
            }
            else if (response.Success)
            {
                return View(response.Data);
            }

            return View(new List<ContactbookViewModel>());
        }

        public IActionResult RedirectGroupContact(string letter)
        {
            ServiceResponse<IEnumerable<ContactbookViewModel>> response = new ServiceResponse<IEnumerable<ContactbookViewModel>>();

            var apiGetCountUrl = $"{endPoint}Contactbook/GetAllSpecificContact" + "?letter=" + letter;

            response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>(apiGetCountUrl, HttpMethod.Get, HttpContext.Request);

            if (response.Message.Equals("No record found !."))
            {
                return View(new List<ContactbookViewModel>());
            }
            else if (response.Success)
            {
                return View(response.Data);
            }

            return View(new List<ContactbookViewModel>());
        }


        [HttpGet]
        public IActionResult ShowAllContactWithPagination(string? search, int page = 1, int pageSize = 2, string sortOrder = "asc")
        {
            var apiGetPositionsUrl = "";
            var apiGetCountUrl = "";
            var apiGetLettersUrl = $"{endPoint}Contactbook/GetAllContacts";


            if (search == null)
            {
                apiGetPositionsUrl = $"{endPoint}Contactbook/GetAllContactsByPagination" + "?page=" + page + "&pageSize=" + pageSize + "&sortOrder=" + sortOrder;

                apiGetCountUrl = $"{endPoint}Contactbook/GetContactsCount";

            }
            else
            {
                ViewBag.Search = search;

                apiGetPositionsUrl = $"{endPoint}Contactbook/GetAllContactsByPagination" + "?search=" + search + "&page=" + page + "&pageSize=" + pageSize + "&sortOrder=" + sortOrder;

                apiGetCountUrl = $"{endPoint}Contactbook/GetContactsCount" + "?search=" + search;

            }



            ServiceResponse<int> countOfContacts = new ServiceResponse<int>();

            countOfContacts = _httpClientService.ExecuteApiRequest<ServiceResponse<int>>
                (apiGetCountUrl, HttpMethod.Get, HttpContext.Request);

            int totalCount = countOfContacts.Data;
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.SortOrder = sortOrder;

            if (totalCount == 0)
            {
                return View(new List<ContactbookViewModel>());

            }
            else
            {
                if (page > totalPages)
                {
                    return RedirectToAction("ShowAllContactWithPagination", new { page = totalPages, pageSize });
                }

                ServiceResponse<IEnumerable<ContactbookViewModel>> response = new ServiceResponse<IEnumerable<ContactbookViewModel>>();

                response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>
                    (apiGetPositionsUrl, HttpMethod.Get, HttpContext.Request);

                ServiceResponse<IEnumerable<ContactbookViewModel>>? lettersResponse = new ServiceResponse<IEnumerable<ContactbookViewModel>>();

                lettersResponse = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>
                    (apiGetLettersUrl, HttpMethod.Get, HttpContext.Request);

                if (lettersResponse.Success)
                {
                    var distinctLetters = lettersResponse.Data.Select(contact => char.ToUpper(contact.Name.FirstOrDefault()))
                                                                .Where(firstLetter => firstLetter != default(char))
                                                                .Distinct()
                                                                 .OrderBy(letter => letter)
                                                                .ToList();
                    ViewBag.DistinctLetters = distinctLetters;

                }



                if (response.Message.Equals("No record found !."))
                {
                    return View(new List<ContactbookViewModel>());
                }
                else if (response.Success)
                {

                    return View(response.Data);
                }
            }

            return View(new List<ContactbookViewModel>());
        }


        [HttpGet]
        public IActionResult ShowAllFavouriteContactWithPagination(int page = 1, int pageSize = 2, string sortOrder = "asc")
        {
            var apiGetPositionsUrl = $"{endPoint}Contactbook/GetAllFavouriteContactsByPagination" + "?page=" + page + "&pageSize=" + pageSize + "&sortOrder=" + sortOrder;


            var apiGetCountUrl = $"{endPoint}Contactbook/GetFavouriteContactsCount";

            var apiGetLettersUrl = $"{endPoint}Contactbook/GetAllFavouriteContacts";


            ServiceResponse<int> countOfContacts = new ServiceResponse<int>();

            countOfContacts = _httpClientService.ExecuteApiRequest<ServiceResponse<int>>
                (apiGetCountUrl, HttpMethod.Get, HttpContext.Request);

            int totalCount = countOfContacts.Data;
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.SortOrder = sortOrder;


            if (totalCount == 0)
            {
                return View(new List<ContactbookViewModel>());

            }
            else
            {



                if (page > totalPages)
                {
                    return RedirectToAction("ShowAllFavouriteContactWithPagination", new { page = totalPages, pageSize });
                }




                ServiceResponse<IEnumerable<ContactbookViewModel>> response = new ServiceResponse<IEnumerable<ContactbookViewModel>>();

                response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>
                    (apiGetPositionsUrl, HttpMethod.Get, HttpContext.Request);

                ServiceResponse<IEnumerable<ContactbookViewModel>>? lettersResponse = new ServiceResponse<IEnumerable<ContactbookViewModel>>();

                lettersResponse = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>
                    (apiGetLettersUrl, HttpMethod.Get, HttpContext.Request);

                if (lettersResponse.Success)
                {
                    var distinctLetters = lettersResponse.Data
                                         .Select(contact => char.ToUpper(contact.Name.FirstOrDefault()))
                                         .Where(firstLetter => firstLetter != default(char))
                                         .Distinct()
                                         .OrderBy(letter => letter)
                                         .ToList();
                    ViewBag.DistinctLetters = distinctLetters;

                }

                if (response.Message.Equals("No record found !."))
                {
                    return View(new List<ContactbookViewModel>());
                }
                else if (response.Success)
                {
                    return View(response.Data);
                }

            }
            return View(new List<ContactbookViewModel>());
        }


        [HttpGet]
        public IActionResult ShowSpecificContactWithPagination(string? search, int page = 1, int pageSize = 2, string letter = "a", string sortOrder = "asc")
        {
            var apiGetPositionsUrl = "";
            var apiGetCountUrl = "";
            var apiGetLettersUrl = $"{endPoint}Contactbook/GetAllContacts";

            if (search == null)
            {
                apiGetPositionsUrl = $"{endPoint}Contactbook/GetSpecificContactsByPaginationWithLetter" + "?page=" + page + "&pageSize=" + pageSize + "&letter=" + letter + "&sortOrder=" + sortOrder;

                apiGetCountUrl = $"{endPoint}Contactbook/GetSpecificContactsCount" + "?letter=" + letter;
            }
            else
            {
                ViewBag.Search = search;

                apiGetPositionsUrl = $"{endPoint}Contactbook/GetSpecificContactsByPaginationWithLetter" + "?search=" + search + "&page=" + page + "&pageSize=" + pageSize + "&letter=" + letter + "&sortOrder=" + sortOrder;

                apiGetCountUrl = $"{endPoint}Contactbook/GetSpecificContactsCount" + "?letter=" + letter + "&search=" + search;
            }

            ServiceResponse<int> countOfContacts = new ServiceResponse<int>();

            countOfContacts = _httpClientService.ExecuteApiRequest<ServiceResponse<int>>
                (apiGetCountUrl, HttpMethod.Get, HttpContext.Request);

            int totalCount = countOfContacts.Data;
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.Letter = letter;
            ViewBag.SortOrder = sortOrder;


            if (totalCount == 0)
            {
                return View(new List<ContactbookViewModel>());

            }
            else
            {
                if (page > totalPages)
                {
                    return RedirectToAction("ShowSpecificContactWithPagination", new { page = totalPages, pageSize, letter });
                }


                ServiceResponse<IEnumerable<ContactbookViewModel>> response = new ServiceResponse<IEnumerable<ContactbookViewModel>>();

                response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>
                    (apiGetPositionsUrl, HttpMethod.Get, HttpContext.Request);

                ServiceResponse<IEnumerable<ContactbookViewModel>>? lettersResponse = new ServiceResponse<IEnumerable<ContactbookViewModel>>();

                lettersResponse = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>
                    (apiGetLettersUrl, HttpMethod.Get, HttpContext.Request);

                if (lettersResponse.Success)
                {
                    var distinctLetters = lettersResponse.Data.Select(contact => char.ToUpper(contact.Name.FirstOrDefault()))
                                                                .Where(firstLetter => firstLetter != default(char))
                                                                .Distinct()
                                                                 .OrderBy(letter => letter)
                                                                .ToList();
                    ViewBag.DistinctLetters = distinctLetters;

                }


                if (response.Message.Equals("No record found !."))
                {
                    return View(new List<ContactbookViewModel>());
                }
                else if (response.Success)
                {
                    return View(response.Data);
                }

            }


            return View(new List<ContactbookViewModel>());
        }

        [HttpGet]
        public IActionResult ShowSpecificFavouriteContactWithPagination(int page = 1, int pageSize = 2, string letter = "a", string sortOrder = "asc")
        {
            var apiGetPositionsUrl = $"{endPoint}Contactbook/GetSpecificFavouriteContactsByPaginationWithLetter" + "?page=" + page + "&pageSize=" + pageSize + "&letter=" + letter + "&sortOrder=" + sortOrder;


            var apiGetCountUrl = $"{endPoint}Contactbook/GetSpecificFavouriteContactsCount" + "?letter=" + letter;

            var apiGetLettersUrl = $"{endPoint}Contactbook/GetAllFavouriteContacts";


            ServiceResponse<int> countOfContacts = new ServiceResponse<int>();

            countOfContacts = _httpClientService.ExecuteApiRequest<ServiceResponse<int>>
                (apiGetCountUrl, HttpMethod.Get, HttpContext.Request);

            int totalCount = countOfContacts.Data;
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.Letter = letter;
            ViewBag.SortOrder = sortOrder;


            if (totalCount == 0)
            {
                return View(new List<ContactbookViewModel>());

            }
            else
            {


                if (page > totalPages)
                {
                    return RedirectToAction("ShowSpecificFavouriteContactWithPagination", new { page = totalPages, pageSize, letter });
                }


                ServiceResponse<IEnumerable<ContactbookViewModel>> response = new ServiceResponse<IEnumerable<ContactbookViewModel>>();

                response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>
                    (apiGetPositionsUrl, HttpMethod.Get, HttpContext.Request);

                ServiceResponse<IEnumerable<ContactbookViewModel>>? lettersResponse = new ServiceResponse<IEnumerable<ContactbookViewModel>>();

                lettersResponse = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<ContactbookViewModel>>>
                    (apiGetLettersUrl, HttpMethod.Get, HttpContext.Request);

                if (lettersResponse.Success)
                {
                    var distinctLetters = lettersResponse.Data
                                         .Select(contact => char.ToUpper(contact.Name.FirstOrDefault()))
                                         .Where(firstLetter => firstLetter != default(char))
                                         .Distinct()
                                         .OrderBy(letter => letter)
                                         .ToList();
                    ViewBag.DistinctLetters = distinctLetters;

                }

                if (response.Message.Equals("No record found !."))
                {
                    return View(new List<ContactbookViewModel>());
                }
                else if (response.Success)
                {
                    return View(response.Data);
                }

            }


            return View(new List<ContactbookViewModel>());
        }


        [HttpGet]
        [Authorize]
        public IActionResult CreateContact()
        {
            AddContactbookViewModel addContactbookViewModel = new AddContactbookViewModel();
            addContactbookViewModel.CountryContactbook = GetCountries();
            addContactbookViewModel.StateContactbook = GetStates();
            return View(addContactbookViewModel);
        }

        [HttpPost]
        public IActionResult CreateContact(AddContactbookViewModel addContactbookViewModel)
        {
            addContactbookViewModel.CountryContactbook = GetCountries();
            addContactbookViewModel.StateContactbook = GetStates();
            if (ModelState.IsValid)
            {

                var apiUrl = $"{endPoint}Contactbook/AddContact";
                var response = _httpClientService.PostHttpResponseMessage(apiUrl, addContactbookViewModel, HttpContext.Request);

                if (response.IsSuccessStatusCode)
                {
                    string successResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successResponse);

                    TempData["SuccessMessage"] = serviceResponse.Message;
                    return RedirectToAction("ShowAllContactWithPagination");
                }
                else
                {
                    string errorResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(errorResponse);
                    if (serviceResponse != null)
                    {
                        TempData["ErrorMessage"] = serviceResponse.Message;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Something went wrong. Please try after sometime.";
                        return RedirectToAction("ShowAllContactWithPagination");
                    }

                }

            }

            return View(addContactbookViewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var apiUrl = $"{endPoint}Contactbook/GetContactsById?id=" + id;
            var response = _httpClientService.GetHttpResponseMessage<ContactbookViewModel>(apiUrl, HttpContext.Request);
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<ContactbookViewModel>>(data);
                if (serviceResponse != null && serviceResponse.Success && serviceResponse.Data != null)
                {
                    return View(serviceResponse.Data);
                }
                else
                {
                    TempData["ErrorMessage"] = serviceResponse.Message;
                    return RedirectToAction("ShowAllContactWithPagination");
                }
            }
            else
            {
                string errorData = response.Content.ReadAsStringAsync().Result;
                var errorResponse = JsonConvert.DeserializeObject<ServiceResponse<ContactbookViewModel>>(errorData);
                if (errorResponse != null)
                {
                    TempData["ErrorMessage"] = errorResponse.Message;
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.Please try after sometime.";
                }
                return RedirectToAction("ShowAllContactWithPagination");
            }

        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {

            var apiUrl = $"{endPoint}Contactbook/GetContactsById?id=" + id;
            var response = _httpClientService.GetHttpResponseMessage<UpdateContactbookViewModel>(apiUrl, HttpContext.Request);
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<UpdateContactbookViewModel>>(data);
                if (serviceResponse != null && serviceResponse.Success && serviceResponse.Data != null)
                {
                    UpdateContactbookViewModel updateContactbookViewModel = serviceResponse.Data;

                    updateContactbookViewModel.CountryContactbook = GetCountries();
                    updateContactbookViewModel.StateContactbook = GetStates();

                    return View(updateContactbookViewModel);
                }
                else
                {
                    TempData["ErrorMessage"] = serviceResponse.Message;
                    return RedirectToAction("ShowAllContactWithPagination");
                }
            }
            else
            {
                string errorData = response.Content.ReadAsStringAsync().Result;
                var errorResponse = JsonConvert.DeserializeObject<ServiceResponse<UpdateContactbookViewModel>>(errorData);
                if (errorResponse != null)
                {
                    TempData["ErrorMessage"] = errorResponse.Message;
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.Please try after sometime.";
                }
                return RedirectToAction("ShowAllContactWithPagination");
            }
        }

        [HttpPost]
        public IActionResult Edit(UpdateContactbookViewModel updateContactbookViewModel)
        {
            updateContactbookViewModel.CountryContactbook = GetCountries();
            updateContactbookViewModel.StateContactbook = GetStates();
            if (ModelState.IsValid)
            {

                //IFormFile imageFile = updateContactbookViewModel.File;
                //if (imageFile != null && imageFile.Length > 0)
                //{

                //    var fileName = Path.GetFileName(imageFile.FileName);

                //    var fileExtension = Path.GetExtension(fileName).ToLower();

                //    if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
                //    {
                //        TempData["ErrorMessage"] = "Invalid file type. Only .jpg, .jpeg, and .png files are allowed.";
                //        return View(updateContactbookViewModel);
                //    }

                //    fileName = AddImageFileToPath(imageFile);
                //    updateContactbookViewModel.FileName = fileName;
                //}

                //else if (updateContactbookViewModel.RemoveImageHidden == "true")
                //{
                //    // User wants to remove the current image
                //    updateContactbookViewModel.FileName = string.Empty;
                //}
                //else
                //{
                //    // Use the previous file name if no new file is provided
                //    updateContactbookViewModel.FileName = updateContactbookViewModel.FileName;
                //}

                var apiUrl = $"{endPoint}Contactbook/ModifyContact";
                HttpResponseMessage response = _httpClientService.PutHttpResponseMessage(apiUrl, updateContactbookViewModel, HttpContext.Request);
                if (response.IsSuccessStatusCode)
                {
                    string successResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successResponse);
                    TempData["SuccessMessage"] = serviceResponse.Message;
                    return RedirectToAction("ShowAllContactWithPagination");
                }
                else
                {
                    string errorData = response.Content.ReadAsStringAsync().Result;
                    var errorResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(errorData);
                    if (errorResponse != null)
                    {
                        TempData["ErrorMessage"] = errorResponse.Message;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Something went wrong.Please try after sometime.";
                        return RedirectToAction("ShowAllContactWithPagination");
                    }
                }
            }
            return View(updateContactbookViewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var apiUrl = $"{endPoint}Contactbook/GetContactsById?id=" + id;
            var response = _httpClientService.GetHttpResponseMessage<ContactbookViewModel>(apiUrl, HttpContext.Request);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var serviceResponse = JsonConvert
                    .DeserializeObject<ServiceResponse<ContactbookViewModel>>(data);
                if (serviceResponse != null && serviceResponse.Success && serviceResponse.Data != null)
                {
                    return View(serviceResponse.Data);
                }
                else
                {
                    TempData["ErrorMessage"] = serviceResponse.Message;
                    return RedirectToAction("ShowAllContactWithPagination");
                }
            }
            else
            {
                string errorData = response.Content.ReadAsStringAsync().Result;
                var errorResponse = JsonConvert
                    .DeserializeObject<ServiceResponse<ContactbookViewModel>>(errorData);
                if (errorResponse != null)
                {
                    TempData["ErrorMessage"] = errorResponse.Message;
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong. Please try after sometime.";
                }

                return RedirectToAction("ShowAllContactWithPagination");
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int contactId)
        {
            var apiUrl = $"{endPoint}Contactbook/RemoveContact/" + contactId;

            //var response = _httpClientService.ExecuteApiRequest<string>(apiUrl,HttpContext.Request);
            var response = _httpClientService.ExecuteApiRequest<ServiceResponse<string>>($"{apiUrl}", HttpMethod.Delete, HttpContext.Request);

            if (response.Success)
            {

                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction("ShowAllContactWithPagination");
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
                return RedirectToAction("ShowAllContactWithPagination");
            }

        }


        //private string AddImageFileToPath(IFormFile imageFile)
        //{

        //    // Process the uploaded file(eq. save it to disk)
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", imageFile.FileName);

        //    // Save the file to storage and set path
        //    using (var stream = new FileStream(filePath, FileMode.Create))

        //    {

        //        imageFile.CopyTo(stream);
        //        return imageFile.FileName;

        //    }

        //}


        private List<StateContactbookViewModel> GetStates()
        {
            ServiceResponse<IEnumerable<StateContactbookViewModel>> response = new ServiceResponse<IEnumerable<StateContactbookViewModel>>();
            response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<StateContactbookViewModel>>>
                ($"{endPoint}State/GetAllStates", HttpMethod.Get, HttpContext.Request);

            if (response.Success)
            {
                return response.Data.ToList();
            }
            return new List<StateContactbookViewModel>();
        }

        private List<CountryContactbookViewModel> GetCountries()
        {
            ServiceResponse<IEnumerable<CountryContactbookViewModel>> response = new ServiceResponse<IEnumerable<CountryContactbookViewModel>>();
            response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<CountryContactbookViewModel>>>
                ($"{endPoint}Country/GetAllCountries", HttpMethod.Get, HttpContext.Request);

            if (response.Success)
            {
                return response.Data.ToList();
            }
            return new List<CountryContactbookViewModel>();
        }

    }
}
