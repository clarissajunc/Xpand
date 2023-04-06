using MediatR;

using Xpand.PlanetsAPI.Commands;
using Xpand.PlanetsAPI.Data;
using Xpand.PlanetsAPI.Data.Models.Enums;
using Xpand.PlanetsAPI.Exceptions;

namespace Xpand.PlanetsAPI.CommandHandlers
{
    public class UpdatePlanetCommandHandler : IRequestHandler<UpdatePlanetCommand>
    {
        private readonly PlanetContext _context;

        public UpdatePlanetCommandHandler(PlanetContext context)
        {
            _context = context;
        }

        public Task Handle(UpdatePlanetCommand request, CancellationToken cancellationToken)
        {
            ValidateRequest(request);

            var planet = _context.Planets.FirstOrDefault(p => p.Id == request.EditPlanet.Id);
            if (planet == null)
            {
                throw new NotFoundException();
            }

            if (!IsValidStatus(request.EditPlanet.PlanetStatus, planet.Status))
            {
                throw new ValidationException();
            }

            planet.Status = request.EditPlanet.PlanetStatus;
            planet.Description = request.EditPlanet.Description;
            planet.DescriptionAuthorId = request.EditPlanet.DescriptionAuthorId;

            _context.Planets.Update(planet);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        private bool IsValidStatus(PlanetStatus newStatus, PlanetStatus oldStatus)
        {
            return newStatus switch
            {
                PlanetStatus.Todo => oldStatus == PlanetStatus.Todo,
                PlanetStatus.EnRoute => oldStatus == PlanetStatus.Todo || oldStatus == PlanetStatus.EnRoute,
                PlanetStatus.Ok or PlanetStatus.NotOk => oldStatus == PlanetStatus.Ok || oldStatus == PlanetStatus.NotOk,
                _ => false
            };
        }

        private void ValidateRequest(UpdatePlanetCommand request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.EditPlanet == null)
            {
                throw new ArgumentException(nameof(request));
            }

            if (request.EditPlanet.Description == null && request.EditPlanet.DescriptionAuthorId.HasValue)
            {
                throw new ValidationException();
            }

            if (request.EditPlanet.Description != null && !request.EditPlanet.DescriptionAuthorId.HasValue)
            {
                throw new ValidationException();
            }        
        }
    }
}
