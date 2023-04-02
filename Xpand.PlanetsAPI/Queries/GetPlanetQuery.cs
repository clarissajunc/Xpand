using MediatR;
using System.Linq.Expressions;
using Xpand.PlanetsAPI.Models;

namespace Xpand.PlanetsAPI.Queries
{
    public record GetPlanetQuery : IRequest<Planet?> 
    { 
        public Expression<Func<Planet, bool>> Predicate { get; set; } = _ => true;
    }
}
