using Microsoft.AspNetCore.Mvc;
using Xpand.API.Managers;

namespace Xpand.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : Controller
    {
        private readonly IDashboardManager _dashboardManager;

        public DashboardController(IDashboardManager dashboardManager)
        {
            _dashboardManager = dashboardManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardAsync()
        {
            var dashboard = await _dashboardManager.GetDashboardAsync();

            return Ok(dashboard);
        }
    }
}
