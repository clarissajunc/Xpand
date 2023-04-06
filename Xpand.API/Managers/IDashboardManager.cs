using Xpand.API.Models;

namespace Xpand.API.Managers
{
    public interface IDashboardManager
    {
        Task<IEnumerable<Planet>> GetDashboardAsync();
    }
}
