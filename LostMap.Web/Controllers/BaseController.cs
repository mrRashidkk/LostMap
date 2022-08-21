using LostMap.BLL.Auth.Interfaces;
using LostMap.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace LostMap.Web.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private readonly IAuthService _authService;

        public BaseController(IAuthService authService)
        {
            _authService = authService;
        }

        protected async Task<User?> GetCurrentUser()
        {
            if (HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeaders))
            {
                var accessToken = authorizationHeaders.FirstOrDefault()
                    ?.Replace(JwtBearerDefaults.AuthenticationScheme, string.Empty)?.Trim();

                if (!string.IsNullOrWhiteSpace(accessToken))
                    return await _authService.GetByAccessToken(accessToken);
            }
            return null;
        }
    }
}
