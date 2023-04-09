using MediatR;

using Xpand.PlanetsAPI.Commands;
using Xpand.PlanetsAPI.Data;
using Xpand.PlanetsAPI.Data.Models;
using Xpand.PlanetsAPI.Data.Models.Enums;
using Xpand.PlanetsAPI.Exceptions;

namespace Xpand.PlanetsAPI.CommandHandlers
{
    public class UpdatePlanetCommandHandler : INotificationHandler<UpdatePlanetCommand>
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
                throw new NotFoundException("Planet not found.");
            }

            if (!IsValidStatus(request.EditPlanet.PlanetStatus, planet.Status))
            {
                throw new ValidationException("Planet status invalid.");
            }

            DeleteExistingDescription(planet.DescriptionId);

            planet.Status = request.EditPlanet.PlanetStatus;
            planet.Description = new Description { 
                Text = request.EditPlanet!.Description!, 
                AuthorId = request.EditPlanet!.DescriptionAuthorId!.Value
            };

            _context.Planets.Update(planet);

            try
            {
                _context.SaveChanges();
            } 
            catch(Exception ex)
            {
                throw new DatabaseException($"An error occured when saving the data: {ex.Message}");
            }

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
            if (request.EditPlanet == null)
            {
                throw new ArgumentException(nameof(request));
            }

            if (string.IsNullOrEmpty(request.EditPlanet.Description) && request.EditPlanet.DescriptionAuthorId.HasValue)
            {
                throw new ValidationException("If description is empty, then author needs to be empty as well.");
            }

            if (!string.IsNullOrEmpty(request.EditPlanet.Description) && !request.EditPlanet.DescriptionAuthorId.HasValue)
            {
                throw new ValidationException("If description has value, then author needs to have value as well.");
            }        
        }

        private void DeleteExistingDescription(int? descriptionId)
        {
            if (!descriptionId.HasValue)
            {
                return;
            }

            var description = _context.Descriptions.FirstOrDefault(d => d.Id == descriptionId);
            if (description != null)
            {
                _context.Descriptions.Remove(description);
            }
        }
    }
}
