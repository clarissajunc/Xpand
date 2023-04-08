using Microsoft.AspNetCore.Mvc;
using Xpand.API.Domain.Models;
using Xpand.API.Managers.Abstractions;

namespace Xpand.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : Controller
    {
        private readonly IDashboardManager _manager;

        public DashboardController(IDashboardManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardAsync()
        {
            var dashboard = await _manager.GetDashboardAsync();

            return Ok(dashboard);
        }

        [HttpPost("{planetId}")]
        public async Task<IActionResult> UpdatePlanetAsync(int planetId, [FromBody] EditPlanet planet)
        {
            if (planetId == default)
            {
                ModelState.AddModelError(nameof(planetId), $"The {nameof(planetId)} is required");
            }

            if (planet == null)
            {
                ModelState.AddModelError(nameof(planet), $"The {nameof(planet)} is required");
            }

            if (planetId != planet?.Id)
            {
                ModelState.AddModelError(nameof(planetId), $"The {planetId} cannot be different");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var httpStatusCode = await _manager.UpdatePlanetAsync(planetId, planet);

            return httpStatusCode switch
            {
                System.Net.HttpStatusCode.NoContent => NoContent(),
                System.Net.HttpStatusCode.NotFound => NotFound(),
                System.Net.HttpStatusCode.InternalServerError => BadRequest(),
                _ => BadRequest(),
            };
        }
    }
}
