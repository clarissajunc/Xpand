using MediatR;
using System.Linq.Expressions;
using Xpand.CrewsAPI.Data.Models;

namespace Xpand.CrewsAPI.Queries
{
    public record GetCrewsQuery: IRequest<IQueryable<Crew>>
    {
        public Expression<Func<Crew, bool>> Predicate { get; set; } = _ => true;
    }
}
