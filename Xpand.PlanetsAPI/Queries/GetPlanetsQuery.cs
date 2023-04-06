using MediatR;
using System.Linq.Expressions;
using Xpand.PlanetsAPI.Data.Models;

namespace Xpand.PlanetsAPI.Queries
{
    public record GetPlanetsQuery: IRequest<IQueryable<Planet>>
    {
        public Expression<Func<Planet, bool>> Predicate { get; set; } = _ => true;
    }
}
