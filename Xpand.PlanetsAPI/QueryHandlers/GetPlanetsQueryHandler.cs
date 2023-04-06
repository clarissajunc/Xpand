using MediatR;
using Xpand.PlanetsAPI.Data;
using Xpand.PlanetsAPI.Data.Models;
using Xpand.PlanetsAPI.Queries;

namespace Xpand.PlanetsAPI.Handlers
{
    public class GetPlanetsQueryHandler : IRequestHandler<GetPlanetsQuery, IQueryable<Planet>>
    {
        private readonly PlanetContext _context;

        public GetPlanetsQueryHandler(PlanetContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Planet>> Handle(GetPlanetsQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_context.Planets.Where(request.Predicate));
        }
    }
}
