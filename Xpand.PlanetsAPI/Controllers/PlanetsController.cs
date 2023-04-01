using Microsoft.AspNetCore.Mvc;

namespace Xpand.PlanetsAPI.Controllers
{
    public class PlanetsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
