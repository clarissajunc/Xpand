using Microsoft.AspNetCore.Mvc;

namespace Xpand.PlanetsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanetsController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
