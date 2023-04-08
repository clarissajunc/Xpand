using Microsoft.AspNetCore.Mvc;
using Xpand.API.Managers.Abstractions;

namespace Xpand.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaptainsController : Controller
    {
        private readonly ICaptainManager _manager;

        public CaptainsController(ICaptainManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var captains = await _manager.GetAllAsync();

            return Ok(captains);
        }
    }
}
