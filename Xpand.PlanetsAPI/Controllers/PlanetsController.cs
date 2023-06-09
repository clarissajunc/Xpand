﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xpand.PlanetsAPI.Commands;
using Xpand.PlanetsAPI.DTOs;
using Xpand.PlanetsAPI.Exceptions;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var planets = await _mediator.Send(new GetPlanetsQuery());
            var planetsWithAuthors = planets.Include(p => p.Description)
                                            .ThenInclude(d => d.Author)
                                            .Include(p => p.Image)
                                            .ToList();

            if (planetsWithAuthors == null || !planetsWithAuthors.Any())
            {
                return Ok();
            }

            return Ok(planetsWithAuthors);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] EditPlanet planet)
        {
            if (id == default)
            {
                ModelState.AddModelError(nameof(id), $"The {nameof(id)} is required");
            }

            if (planet == null)
            {
                ModelState.AddModelError(nameof(planet), $"The {nameof(planet)} is required");
            }

            if (id != planet?.Id)
            {
                ModelState.AddModelError(nameof(id), $"The {id} cannot be different");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Publish(new UpdatePlanetCommand(planet!));

            return NoContent();
        }
    }
}
