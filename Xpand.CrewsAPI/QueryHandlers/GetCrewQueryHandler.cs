using MediatR;
using Microsoft.EntityFrameworkCore;
using Xpand.CrewsAPI.Data;
using Xpand.CrewsAPI.Data.Models;
using Xpand.CrewsAPI.Queries;

namespace Xpand.CrewsAPI.QueryHandlers
{
    public class GetCrewQueryHandler : IRequestHandler<GetCrewQuery, Crew?>
    {
        private readonly CrewContext _context;

        public GetCrewQueryHandler(CrewContext context)
        {
            _context = context;
        }

        public async Task<Crew?> Handle(GetCrewQuery request, CancellationToken cancellationToken)
        {
            return await _context.Crews.FirstOrDefaultAsync(request.Predicate, cancellationToken);
        }
    }
}
