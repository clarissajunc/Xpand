using MediatR;
using System.Linq.Expressions;
using Xpand.CrewsAPI.Data.Models;

namespace Xpand.CrewsAPI.Queries
{
    public class GetCrewQuery: IRequest<Crew?>
    {
        public Expression<Func<Crew, bool>> Predicate { get; set; } = _ => true;
    }
}
