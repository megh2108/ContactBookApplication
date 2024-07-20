using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Models;
using ApiContactbookApplication.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiContactbookApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Register")]

        public IActionResult RegisterUser(RegisterDto registerDto)
        {

            var result = _authService.RegisterUserService(registerDto);

            return !result.Success ? BadRequest(result) : Ok(result);


        }

        [HttpGet("GetUserDetailById")]
        public IActionResult GetUserDetailById(int id)
        {
            var response = _authService.GetUserDetailById(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpPut("EditUserDetail")]
        public IActionResult Edit(UserDetailDto userDto)
        {
            
                var user = new User()
                {
                    UserId = userDto.UserId,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    LoginId = userDto.LoginId,
                    File = userDto.File,
                    FileName = userDto.FileName,
                    ContactNumber = userDto.ContactNumber,
                };
                var result = _authService.ModifyUser(user);
                return !result.Success ? BadRequest(result) : Ok(result);
         

        }
        [HttpPost("Login")]

        public IActionResult LoginUser(LoginDto loginDto)
        {

            var result = _authService.LoginUserService(loginDto);

            return !result.Success ? BadRequest(result) : Ok(result);


        }


        [HttpPost("PasswordService")]
        public IActionResult PasswordService(PasswordDto passwordDto)
        {
            var response = _authService.PasswordService(passwordDto);
            return !response.Success ? BadRequest(response) : Ok(response);
        }
    }
}
