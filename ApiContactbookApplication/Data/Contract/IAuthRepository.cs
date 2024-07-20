using ApiContactbookApplication.Models;

namespace ApiContactbookApplication.Data.Contract
{
    public interface IAuthRepository
    {
        bool RegisterUser(User user);

        User? GetUserDetailById(int id);

        bool UserExists(int userId, string loginId, string email);

        User? ValidateUser(string username);

        bool UserExists(string loginId, string email);

        bool UpdateUser(User user);
    }
}
