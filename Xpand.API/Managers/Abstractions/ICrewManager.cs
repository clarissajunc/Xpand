using Xpand.API.Domain.Models;

namespace Xpand.API.Managers.Abstractions
{
    public interface ICrewManager
    {
        Task<IEnumerable<Human>> GetAllCaptainsAsync();

        Task<IEnumerable<Crew>> GetAllCrewsAsync();
    }
}
