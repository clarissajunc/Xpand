using MediatR;
using Xpand.PlanetsAPI.DTOs;

namespace Xpand.PlanetsAPI.Commands
{
    public record UpdatePlanetCommand(EditPlanet EditPlanet): INotification;
}
