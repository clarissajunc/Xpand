using MediatR;
using Xpand.PlanetsAPI.Models;

namespace Xpand.PlanetsAPI.Commands
{
    public record UpdatePlanetCommand(EditPlanet EditPlanet): IRequest;
}
