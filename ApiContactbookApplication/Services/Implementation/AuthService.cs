using ApiContactbookApplication.Data.Contract;
using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Models;
using ApiContactbookApplication.Services.Contract;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace ApiContactbookApplication.Services.Implementation
{
    public class AuthService : IAuthService 
    {
        private readonly IAuthRepository _authRepository;
        private readonly IVerifyPasswordHashService _verifyPasswordHashService;



        public AuthService(IAuthRepository authRepository, IVerifyPasswordHashService verifyPasswordHashService)
        {
            _authRepository = authRepository; //this is registering the dependency. it is call dependency injection
            _verifyPasswordHashService = verifyPasswordHashService;


        }

      
        public ServiceResponse<string> RegisterUserService(RegisterDto register)
        {
            var response = new ServiceResponse<string>();
            var message = string.Empty;
            if (register != null)
            {
                message = CheckPasswordStrength(register.Password);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    response.Success = false;
                    response.Message = message;
                    return response;
                }
                else if (_authRepository.UserExists(register.LoginId, register.Email))
                {
                    response.Success = false;
                    response.Message = "User already exists";
                    return response;
                }
                else
                {
                    User user = new User()
                    {
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        Email = register.Email,
                        LoginId = register.LoginId,
                        ContactNumber = register.ContactNumber,
                        FileName = register.FileName,
                        File = register.File,
                    };

                    CreatePasswordHash(register.Password, out byte[] passwordHash, out byte[] passwordsalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordsalt;

                    var result = _authRepository.RegisterUser(user);

                    response.Success = result;
                    response.Message = result ? string.Empty : "Something went wrong. Please try after sometime.";
                }
            }
            return response;
        }


        public ServiceResponse<UserDetailDto> GetUserDetailById(int id)
        {
            var response = new ServiceResponse<UserDetailDto>();
            var userDetail = _authRepository.GetUserDetailById(id);

            if (userDetail != null)
            {

                var userDetailDto = new UserDetailDto()
                {
                    UserId = userDetail.UserId,
                    FirstName = userDetail.FirstName,
                    LastName = userDetail.LastName,
                    LoginId = userDetail.LoginId,
                    Email = userDetail.Email,
                    ContactNumber = userDetail.ContactNumber,  
                    FileName  = userDetail.FileName,
                    File = userDetail.File
                    
                };

                response.Data = userDetailDto;

            }
            else
            {
                response.Success = false;
                response.Message = "No record found !.";
            }

            return response;
        }


        public ServiceResponse<string> ModifyUser(User user)
        {
            var response = new ServiceResponse<string>();
            if (_authRepository.UserExists(user.UserId, user.LoginId, user.Email))
            {
                response.Success = false;
                response.Message = "User Exists!";
                return response;

            }
            var existingContact = _authRepository.GetUserDetailById(user.UserId);
            var result = false;
            if (existingContact != null)
            {
                existingContact.FirstName = user.FirstName;
                existingContact.LastName = user.LastName;
                existingContact.Email = user.Email;
                existingContact.LoginId = user.LoginId;
                existingContact.File = user.File;
                existingContact.FileName = user.FileName;
                existingContact.ContactNumber = user.ContactNumber;
                result = _authRepository.UpdateUser(existingContact);
            }
            if (result)
            {
                response.Success = true;
                response.Message = "User Updated successfully";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong after sometime";
            }
            return response;
        }

        public ServiceResponse<string> LoginUserService(LoginDto login)
        {
            var response = new ServiceResponse<string>();
            if (login != null)
            {
                var user = _authRepository.ValidateUser(login.Username);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Invalid username or password";
                    return response;
                }

                else if (!_verifyPasswordHashService.VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Success = false;
                    response.Message = "Invalid username or password";
                    return response;
                }

                string token = _verifyPasswordHashService.CreateToken(user);
                response.Data = token;
                return response;
            }
            response.Success = false;
            response.Message = "Something went wrong.Please try after sometime.";
            return response;
        }


        public ServiceResponse<string> PasswordService(PasswordDto passwordDto)
        {
            var response = new ServiceResponse<string>();
            var message = string.Empty;

            if (passwordDto != null)
            {
                var user = _authRepository.ValidateUser(passwordDto.UserName);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Invalid username!";
                    return response;
                }

                if (passwordDto.Password != passwordDto.ConfirmPassword)
                {
                    response.Success = false;
                    response.Message = "Password and confirmation password do not match!";
                    return response;
                }
                message = CheckPasswordStrength(passwordDto.Password);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    response.Success = false;
                    response.Message = message;
                    return response;
                }
                // Create password hash and salt
                CreatePasswordHash(passwordDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

                // Update user's password hash and salt
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                _authRepository.UpdateUser(user); // Update the user with the new password hash and salt

                response.Success = true;
                response.Message = "Password reset successfully!";
                return response;
            }

            response.Success = false;
            response.Message = "Something went wrong, please try again later.";
            return response;
        }


        private string CheckPasswordStrength(string password)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (password.Length < 8)
            {
                stringBuilder.Append("Mininum password length should be 8" + Environment.NewLine);
            }
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
            {
                stringBuilder.Append("Password should be alphanumeric" + Environment.NewLine);
            }
            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,*,(,),_,]"))
            {
                stringBuilder.Append("Password should contain special characters" + Environment.NewLine);
            }

            return stringBuilder.ToString();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }


        //private bool UserExists(string LoginId, string Email)
        //{
        //    //    if (_appdbcontext.users.any(c => c.loginid.tolower() == loginid.tolower() || c.email.tolower() == email.tolower()))
        //    //    {
        //    //        return true;
        //    //    }
        //    return false;
        //}

        //private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
        //    {
        //        var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        return computeHash.SequenceEqual(passwordHash);
        //    }
        //}


        //private string CreateToken(User user)
        //{
        //    List<Claim> claimes = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
        //        new Claim(ClaimTypes.Name,user.LoginId.ToString()),

        //    };

        //    SymmetricSecurityKey key = new SymmetricSecurityKey(
        //        Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        //    SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        //    SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claimes),
        //        Expires = DateTime.Now.AddDays(1),
        //        SigningCredentials = signingCredentials

        //    };

        //    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        //    SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
    }
}
