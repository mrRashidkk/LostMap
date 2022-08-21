using LostMap.BLL.Auth.Interfaces;
using LostMap.BLL.Interfaces;
using LostMap.BLL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostMap.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService, IAuthService authService)
            : base(authService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOwn()
        {
            var currentUser = await GetCurrentUser();
            return Ok(AccountDto.Create(currentUser));
        }

        [HttpPut]
        public async Task<IActionResult> Save(AccountDto accountDto)
        {
            var currentUser = await GetCurrentUser();
            await _userService.UpdateAccount(accountDto, currentUser);
            return Ok();
        }
    }
}
