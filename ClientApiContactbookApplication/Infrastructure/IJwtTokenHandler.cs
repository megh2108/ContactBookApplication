using System.IdentityModel.Tokens.Jwt;

namespace ClientApiContactbookApplication.Infrastructure
{
    public interface IJwtTokenHandler
    {
        JwtSecurityToken ReadJwtToken(string token);

    }
}
