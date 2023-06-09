﻿using Microsoft.AspNetCore.Mvc;
using Xpand.API.Managers.Abstractions;

namespace Xpand.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaptainsController : Controller
    {
        private readonly ICrewManager _manager;

        public CaptainsController(ICrewManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var captains = await _manager.GetAllCaptainsAsync();

            if (captains == null || !captains.Any())
            {
                return Ok();
            }

            return Ok(captains);
        }
    }
}
