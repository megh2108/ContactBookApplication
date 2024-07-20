using ClientApiContactbookApplication.Infrastructure;
using ClientApiContactbookApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace ClientApiContactbookApplication.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenHandler _tokenHandler;
        private string endPoint;

        public AuthController(IHttpClientService httpClientService, IConfiguration configuration, IJwtTokenHandler tokenHandler)
        {
            _httpClientService = httpClientService;
            _configuration = configuration;
            endPoint = _configuration["Endpoint:CivicaContactApi"];
            _tokenHandler = tokenHandler;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                //password strength
                //userexist
                //save user

                var apiUrl = $"{endPoint}Auth/Register";
                var response = _httpClientService.PostHttpResponseMessage(apiUrl, registerViewModel, HttpContext.Request);

                if (response.IsSuccessStatusCode)
                {
                    string successResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successResponse);

                    TempData["SuccessMessage"] = serviceResponse.Message;
                    return RedirectToAction("RegisterSuccess");
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
                    }
                    return View(registerViewModel);

                }


            }
            return View(registerViewModel);
        }


        public IActionResult RegisterSuccess()
        {
            return View();
        }

        [HttpGet]
        [ExcludeFromCodeCoverage]
        [Authorize]
        public virtual UserDetailViewModel UserDetailById(int id)
        {
        
            var apiUrl = $"{endPoint}Auth/GetUserDetailById?id=" + id;
            var response = _httpClientService.GetHttpResponseMessage<UserDetailViewModel>(apiUrl, HttpContext.Request);
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<UserDetailViewModel>>(data);
                if (serviceResponse != null && serviceResponse.Success && serviceResponse.Data != null)
                {
                    return serviceResponse.Data;
                }
                else
                {
                    throw new Exception(serviceResponse.Message);

                }
            }
            else
            {
                string errorData = response.Content.ReadAsStringAsync().Result;
                var errorResponse = JsonConvert.DeserializeObject<ServiceResponse<UserDetailViewModel>>(errorData);
                if (errorResponse != null)
                {
                    throw new Exception(errorResponse.Message);
                }
                else
                {
                    throw new Exception("Something went wrong. Please try after some time.");
                }
            }

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {

                var apiUrl = $"{endPoint}Auth/Login";
                var response = _httpClientService.PostHttpResponseMessage(apiUrl, loginViewModel, HttpContext.Request);

                if (response.IsSuccessStatusCode)
                {
                    string successResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successResponse);

                    string token = serviceResponse.Data;

                    Response.Cookies.Append("jwtToken", token, new CookieOptions
                    {
                        HttpOnly = false,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddDays(1),
                    });

                    var jwtToken = _tokenHandler.ReadJwtToken(token);
                    var userId = jwtToken.Claims.First(claim => claim.Type == "UserId").Value;

                    Response.Cookies.Append("userId", userId, new CookieOptions
                    {
                        HttpOnly = false,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddDays(1),
                    });


                    var id = Convert.ToInt32(userId);

                    //Get user details
                    var userDetails = UserDetailById(id);

                    //// Store user image in cookie
                    if (userDetails != null && userDetails.File != null)
                    {
                        var image = Convert.ToBase64String(userDetails.File);

                        // Split image into smaller chunks if necessary to fit cookie size limit
                        int chunkSize = 3800; // safe size under 4KB considering other cookie data
                        int totalChunks = (image.Length + chunkSize - 1) / chunkSize;

                        for (int i = 0; i < totalChunks; i++)
                        {
                            string chunk = image.Substring(i * chunkSize, Math.Min(chunkSize, image.Length - i * chunkSize));
                            Response.Cookies.Append($"image_chunk_{i}", chunk, new CookieOptions
                            {
                                HttpOnly = false,
                                Secure = true,
                                SameSite = SameSiteMode.None,
                                Expires = DateTime.UtcNow.AddDays(1),
                            });
                        }
                    }




                    return RedirectToAction("ShowAllContactWithPagination", "Contactbook");
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
                    }
                    return View(loginViewModel);

                }


            }

            return View(loginViewModel);

        }

        [Authorize]
        [HttpGet]
        public IActionResult EditUser(int id)
        {
            //var username = @User.Identity.Name;
            var apiUrl = $"{endPoint}Auth/GetUserDetailById?id=" + id;
            var response = _httpClientService.GetHttpResponseMessage<UserDetailViewModel>(apiUrl, HttpContext.Request);
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<UserDetailViewModel>>(data);
                if (serviceResponse != null && serviceResponse.Success && serviceResponse.Data != null)
                {
                    UserDetailViewModel viewModel = serviceResponse.Data;
                    return View(viewModel);
                }
                else
                {
                    TempData["ErrorMessage"] = serviceResponse.Message;
                    return RedirectToAction("Register");
                }
            }
            else
            {
                string errorData = response.Content.ReadAsStringAsync().Result;
                var errorResponse = JsonConvert.DeserializeObject<ServiceResponse<UserDetailViewModel>>(errorData);
                if (errorResponse != null)
                {
                    TempData["ErrorMessage"] = errorResponse.Message;
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.Please try after sometime.";
                }
                return RedirectToAction("Register");
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult EditUser(UserDetailViewModel updateContact)
        {
            if (ModelState.IsValid)
            {
               
                var apiUrl = $"{endPoint}Auth/EditUserDetail";
                HttpResponseMessage response = _httpClientService.PutHttpResponseMessage(apiUrl, updateContact, HttpContext.Request);
                if (response.IsSuccessStatusCode)
                {
                    string successResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successResponse);
                    TempData["SuccessMessage"] = serviceResponse.Message;

                    //return RedirectToAction("ShowAllContactWithPagination", "Contactbook");
                    return RedirectToAction("Logout");
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
                        TempData["ErrorMessage"] = "Something went wrong try after some time";
                        return RedirectToAction("ShowAllContactWithPagination", "Contactbook");
                    }
                }
            }
            return View(updateContact);
        }

        public IActionResult Logout()
        {
            int chunkIndex = 0;
            while (Request.Cookies.ContainsKey($"image_chunk_{chunkIndex}"))
            {
                Response.Cookies.Delete($"image_chunk_{chunkIndex}");
                chunkIndex++;
            }
            Response.Cookies.Delete("jwtToken");
            Response.Cookies.Delete("userId");
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(PasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var apiUrl = $"{endPoint}Auth/PasswordService";
                var response = _httpClientService.PostHttpResponseMessage(apiUrl, viewModel, HttpContext.Request);
                if (response.IsSuccessStatusCode)
                {
                    string successResponse =  response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successResponse);

                    TempData["SuccessMessage"] = serviceResponse.Message;
                    return RedirectToAction("PasswordConfirmation");
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
                        TempData["ErrorMessage"] = "Something went wrong. Please try again later.";
                    }
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            PasswordViewModel viewModel = new PasswordViewModel();
            viewModel.UserName = @User.Identity.Name;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ChangePassword(PasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var apiUrl = $"{endPoint}Auth/PasswordService";
                var response = _httpClientService.PostHttpResponseMessage(apiUrl, viewModel, HttpContext.Request);
                if (response.IsSuccessStatusCode)
                {
                    string successResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successResponse);

                    TempData["SuccessMessage"] = serviceResponse.Message;
                    return RedirectToAction("Logout");
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
                        TempData["ErrorMessage"] = "Something went wrong. Please try again later.";
                    }
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult PasswordConfirmation()
        {
            return View();
        }
    }
}
