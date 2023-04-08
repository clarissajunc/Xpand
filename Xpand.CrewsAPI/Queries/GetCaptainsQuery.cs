using MediatR;
using System.Linq.Expressions;
using Xpand.CrewsAPI.Data.Models;

namespace Xpand.CrewsAPI.Queries
{
    public record GetCaptainsQuery: IRequest<IQueryable<Human>>
    {
        public Expression<Func<Human, bool>> Predicate { get; set; } = _ => true;
    }
}
