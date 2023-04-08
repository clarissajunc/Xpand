using MediatR;
using Xpand.CrewsAPI.Data;
using Xpand.CrewsAPI.Data.Models;
using Xpand.CrewsAPI.Queries;

namespace Xpand.CrewsAPI.QueryHandlers
{
    public class GetCaptainsQueryHandler : IRequestHandler<GetCaptainsQuery, IQueryable<Human>>
    {
        private readonly CrewContext _context;

        public GetCaptainsQueryHandler(CrewContext context)
        {
            _context = context;
        }
        public Task<IQueryable<Human>> Handle(GetCaptainsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Humans.Where(request.Predicate).AsQueryable());
        }
    }
}
