using MediatR;
using Xpand.CrewsAPI.Data;
using Xpand.CrewsAPI.Models;
using Xpand.CrewsAPI.Queries;

namespace Xpand.CrewsAPI.QueryHandlers
{
    public class GetCrewsQueryHandler : IRequestHandler<GetCrewsQuery, IQueryable<Crew>>
    {
        private readonly CrewContext _context;

        public GetCrewsQueryHandler(CrewContext context)
        {
            _context = context;
        }

        public Task<IQueryable<Crew>> Handle(GetCrewsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Crews.Where(request.Predicate).AsQueryable());
        }
    }
}
