using ContactBookApplication.Data;
using ContactBookApplication.Models;
using ContactBookApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Text;

namespace ContactBookApplication.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                //password strength
                //userexist
                //save user

                var message = CheckPasswordStrength(register.Password);
                if (!string.IsNullOrEmpty(message))
                {
                    TempData["ErrorMessage"] = message;
                }
                else if (UserExists(register.LoginId, register.Email))
                {
                    TempData["ErrorMessage"] = "User already exists.";

                }
                else
                {
                    //save user
                    User user = new User()
                    {
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        Email = register.Email,
                        LoginId = register.LoginId,
                        ContactNumber = register.ContactNumber,

                    };

                    CreatePasswordHash(register.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    _appDbContext.Add(user);
                    _appDbContext.SaveChanges();

                    return RedirectToAction("RegisterSuccess");
                }

            }
            return View(register);
        }

        public IActionResult RegisterSuccess()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = _appDbContext.Users.FirstOrDefault(c => c.LoginId.ToLower() == login.Username.ToLower() || c.Email == login.Username.ToLower());

                if (user == null)
                {
                    TempData["ErrorMessage"] = "Invalid username or password.";
                    return View(login);

                }
                else if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                {
                    TempData["ErrorMessage"] = "Invalid username or password.";
                    return View(login);

                }

                string token = CreateToken(user);

                Response.Cookies.Append("jwtToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return RedirectToAction("Index", "Contactbook");
            }

            return View(login);

        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwtToken");

            return RedirectToAction("Index", "Home");
        }

        private string CheckPasswordStrength(string password)
        {
            StringBuilder builder = new StringBuilder();
            if (password.Length < 8)
            {
                builder.Append("Minimumm password length should be 8" + Environment.NewLine);
            }

            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
            {
                builder.Append("Password should be alphanumeric" + Environment.NewLine);
            }
            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,*,(,),_,]"))
            {
                builder.Append("Password should contain special characters" + Environment.NewLine);
            }

            return builder.ToString();
        }

        private bool UserExists(string loginid, string email)
        {
            if (_appDbContext.Users.Any(c => c.LoginId.ToLower() == loginid.ToLower() || c.Email.ToLower() == email.ToLower()))
            {
                return true;
            }

            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claimes = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.LoginId.ToString()),

            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimes),
                Expires = DateTime.Now.AddSeconds(10),
                //Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredentials

            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
