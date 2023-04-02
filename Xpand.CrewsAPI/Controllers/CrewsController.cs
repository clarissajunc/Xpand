using Microsoft.AspNetCore.Mvc;

namespace Xpand.CrewsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrewsController: Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
