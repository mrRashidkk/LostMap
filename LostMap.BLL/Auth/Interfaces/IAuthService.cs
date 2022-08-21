using LostMap.BLL.Auth.Models;
using LostMap.DAL.Models;

namespace LostMap.BLL.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<UserAuthData> Login(LoginRequest loginData);
        Task<UserAuthData> RecreateTokens(User user, string refreshToken);
        Task<UserAuthData> SignUp(SignUpRequest signUpRequest);
        Task Logout(User user);
        Task<User?> GetByAccessToken(string accessToken);
    }
}
