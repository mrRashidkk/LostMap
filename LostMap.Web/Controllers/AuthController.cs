using LostMap.BLL.Auth.Interfaces;
using LostMap.BLL.Auth.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostMap.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) : base(authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var userAuthData = await _authService.Login(loginRequest);
            return Ok(userAuthData);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromForm] SignUpRequest signUpRequest)
        {
            var userAuthData = await _authService.SignUp(signUpRequest);
            return Ok(userAuthData);
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> RecreateTokens([FromBody] string refreshToken)
        {
            var user = await GetCurrentUser();
            if (user == null)
            {
                return StatusCode(500); //todo
            }
            var userAuthData = await _authService.RecreateTokens(user, refreshToken);
            return Ok(userAuthData);
        }

        [HttpPut]
        public async Task<IActionResult> Logout()
        {
            var user = await GetCurrentUser();
            if (user == null)
            {
                return StatusCode(500); //todo
            }
            await _authService.Logout(user);
            return Ok();
        }
    }
}
