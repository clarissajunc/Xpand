using System.Net;
using Xpand.API.Domain.Models;

namespace Xpand.API.Managers.Abstractions
{
    public interface IPlanetManager
    {
        Task<IEnumerable<Planet>> GetAllAsync();

        Task<HttpStatusCode> UpdateAsync(int planetId, EditPlanet editPlanet);
    }
}
