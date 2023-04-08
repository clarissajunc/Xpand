using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Xpand.API.Domain.Models;
using Xpand.API.Managers.Abstractions;

namespace Xpand.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : Controller
    {
        private readonly IPlanetManager _planetManager;

        private readonly ICrewManager _crewManager;

        private readonly IMapper _mapper;

        public DashboardController(IMapper mapper, IPlanetManager planetManager, ICrewManager crewManager)
        {
            _mapper = mapper;
            _planetManager = planetManager;
            _crewManager = crewManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardAsync()
        {
            IEnumerable<Planet> planets = await _planetManager.GetAllAsync();
            IEnumerable<Crew> crews = await _crewManager.GetAllCrewsAsync();

            List<DTOs.Planet> planetDTOs = planets.Select(_mapper.Map<DTOs.Planet>).ToList();
            if (!planetDTOs.Any() || !crews.Any())
            {
                Ok(planetDTOs);
            }

            foreach (var planet in planetDTOs)
            {
                if (!planet.CrewId.HasValue)
                {
                    continue;
                }

                planet.Robots = crews.Where(c => c.Id == planet.CrewId.Value)
                    .SelectMany(c => c.Robots)
                    .Select(r => r.Name)
                    .ToList();
            }

            return Ok(planetDTOs);
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

            var httpStatusCode = await _planetManager.UpdateAsync(planetId, planet);

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
