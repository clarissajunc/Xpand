using System.Net;
using Xpand.API.Domain.Models;

namespace Xpand.API.Managers
{
    public interface IDashboardManager
    {
        Task<IEnumerable<Planet>> GetDashboardAsync();

        Task<HttpStatusCode> UpdatePlanetAsync(int planetId, EditPlanet editPlanet);
    }
}
