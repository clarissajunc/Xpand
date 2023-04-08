using Xpand.API.Domain.Models;

namespace Xpand.API.Managers.Abstractions
{
    public interface ICaptainManager
    {
        Task<IEnumerable<Human>> GetAllAsync();
    }
}
