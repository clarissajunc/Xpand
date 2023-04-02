using MediatR;
using Microsoft.EntityFrameworkCore;
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
            return await _context.Planets.FirstOrDefaultAsync(request.Predicate, cancellationToken);
        }
    }
}
