using MediatR;
using Xpand.PlanetsAPI.Data;
using Xpand.PlanetsAPI.Models;
using Xpand.PlanetsAPI.Queries;

namespace Xpand.PlanetsAPI.Handlers
{
    public class GetPlanetQueryHandler : IRequestHandler<GetPlanetQuery, Planet?>
    {
        private readonly PlanetContext _context;

        public GetPlanetQueryHandler(PlanetContext context)
        {
            _context = context;
        }

        public async Task<Planet?> Handle(GetPlanetQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_context.Planets.FirstOrDefault(request.Predicate));
        }
    }
}
