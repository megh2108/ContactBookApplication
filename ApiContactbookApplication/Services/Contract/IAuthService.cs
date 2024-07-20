using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Models;

namespace ApiContactbookApplication.Services.Contract
{
    public interface IAuthService
    {
        ServiceResponse<string> RegisterUserService(RegisterDto register);

        ServiceResponse<UserDetailDto> GetUserDetailById(int id);

        ServiceResponse<string> ModifyUser(User user);
        ServiceResponse<string> LoginUserService(LoginDto login);

        ServiceResponse<string> PasswordService(PasswordDto forgetDto);
    }
}
