using LostMap.BLL.Auth.Interfaces;
using LostMap.BLL.Interfaces;
using LostMap.BLL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LostMap.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LossController : BaseController
    {
        private readonly ILossService _lossService;

        public LossController(ILossService lossService, IAuthService authService)
            : base(authService)
        {
            _lossService = lossService;
        }

        [HttpPost]
        public async Task<IActionResult> Save(LossDto lossDto)
        {
            var currentUser = await GetCurrentUser();
            await _lossService.Create(lossDto, currentUser);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allLosses = await _lossService.GetAll();
            return Ok(allLosses);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var loss = await _lossService.Get(id);
            return Ok(loss);
        }
    }
}
