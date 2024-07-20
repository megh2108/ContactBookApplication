using ApiContactbookApplication.Models;

namespace ApiContactbookApplication.Services.Contract
{
    public interface IVerifyPasswordHashService
    {
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

        string CreateToken(User user);
    }
}
