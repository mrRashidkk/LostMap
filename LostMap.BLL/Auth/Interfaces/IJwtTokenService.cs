using LostMap.DAL.Models;

namespace LostMap.BLL.Auth.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(User user);
        string? GetClaimValue(string accessToken, string claimType);
    }
}
