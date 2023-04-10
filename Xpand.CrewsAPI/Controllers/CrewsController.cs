using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xpand.CrewsAPI.Queries;

namespace Xpand.CrewsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrewsController: Controller
    {
        private readonly IMediator _mediator;

        public CrewsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var crews = await _mediator.Send(new GetCrewsQuery());
            var fullCrews = crews.Include(c => c.Robots).Include(c => c.Captain)?.ToList();

            if (fullCrews == null || !fullCrews.Any()) 
            {
                return Ok();
            }

            return Ok(fullCrews);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (id == default)
            {
                ModelState.AddModelError(nameof(id), $"The {nameof(id)} is required");
                return BadRequest(ModelState);
            }

            var crew = await _mediator.Send(new GetCrewQuery
            {
                Predicate = c => c.Id == id
            });

            return crew != null ? Ok(crew) : NotFound();
        }
    }
}
