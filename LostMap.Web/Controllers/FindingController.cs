using LostMap.BLL.Auth.Interfaces;
using LostMap.BLL.Interfaces;
using LostMap.BLL.Models;
using LostMap.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LostMap.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FindingController : BaseController
    {
        private readonly IFindingService _findingService;

        public FindingController(IFindingService findingService , IAuthService authService)
            : base(authService)
        {
            _findingService = findingService;
        }

        [HttpPost]
        public async Task<IActionResult> Save(FindingDto findingDto)
        {
            var currentUser = await GetCurrentUser();
            await _findingService.Create(findingDto, currentUser);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allFindings = await _findingService.GetAll();
            return Ok(allFindings);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var finding = await _findingService.Get(id);
            return Ok(finding);
        }
    }
}
