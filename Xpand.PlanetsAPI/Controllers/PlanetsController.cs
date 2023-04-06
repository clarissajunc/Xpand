using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xpand.PlanetsAPI.Queries;

namespace Xpand.PlanetsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanetsController : Controller
    {
        private readonly IMediator _mediator;

        public PlanetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var planets = await _mediator.Send(new GetPlanetsQuery());
            var planetsWithAuthors = planets.Include(p => p.DescriptionAuthor).Include(p => p.Image).ToList();

            if (planetsWithAuthors == null || !planetsWithAuthors.Any())
            {
                return NoContent();
            }

            return Ok(planetsWithAuthors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (id == default)
            {
                ModelState.AddModelError(nameof(id), $"The {nameof(id)} is required");
                return BadRequest(ModelState);
            }

            var planet = await _mediator.Send(new GetPlanetQuery 
            { 
                Predicate = p => p.Id == id 
            });

            return planet != null ? Ok(planet) : NotFound();
        }
    }
}
