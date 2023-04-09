using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xpand.CrewsAPI.Queries;

namespace Xpand.CrewsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaptainsController : Controller
    {
        private readonly IMediator _mediator;

        public CaptainsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllCaptainsAsync()
        {
            var crews = await _mediator.Send(new GetCaptainsQuery());

            if (crews == null || !crews.Any())
            {
                return NoContent();
            }

            return Ok(crews);
        }
    }
}
