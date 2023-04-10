using Moq;
using Moq.EntityFrameworkCore;
using Xpand.PlanetsAPI.CommandHandlers;
using Xpand.PlanetsAPI.Commands;
using Xpand.PlanetsAPI.Data;
using Xpand.PlanetsAPI.Data.Models;
using Xpand.PlanetsAPI.Data.Models.Enums;
using Xpand.PlanetsAPI.DTOs;
using Xpand.PlanetsAPI.Exceptions;

namespace Xpand.PlanetsAPI.Tests.CommandHandlers
{
    public class UpdatePlanetCommandHandlerUnitTests
    {
        private readonly UpdatePlanetCommandHandler _commandHandler;

        private readonly Mock<PlanetContext> _contextMock;

        public UpdatePlanetCommandHandlerUnitTests()
        {
            _contextMock = new Mock<PlanetContext>();
            _commandHandler = new UpdatePlanetCommandHandler(_contextMock.Object);
        }

        [Fact]
        public async Task UpdatePlanetCommand_InvalidInput_ThrowsArgumentNullExceptionAsync()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _commandHandler.Handle(new UpdatePlanetCommand(null), CancellationToken.None));
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public async Task UpdatePlanetCommand_DescriptionAuthorMismatch_ThrowsValidationExceptionAsync(bool nullDescription, bool nullAuthor)
        {
            //Arrange
            var editPlanet = new EditPlanet 
            { 
                Description = nullDescription ? null : "test", 
                DescriptionAuthorId = nullAuthor ? null : 1 
            };

            //Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _commandHandler.Handle(new UpdatePlanetCommand(editPlanet), CancellationToken.None));
        }

        [Fact]
        public async Task UpdatePlanetCommand_PlanetNotFound_ThrowsNotFoundExceptionAsync()
        {
            //Arrange
            var editPlanet = new EditPlanet 
            { 
                Id = 1,
                Description = "test",
                DescriptionAuthorId = 1 
            };
            _contextMock.Setup(c => c.Planets)
                        .ReturnsDbSet(new List<Planet>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _commandHandler.Handle(new UpdatePlanetCommand(editPlanet), CancellationToken.None));
        }

        [Theory]
        [InlineData(PlanetStatus.Todo, PlanetStatus.EnRoute)]
        [InlineData(PlanetStatus.Todo, PlanetStatus.Ok)]
        [InlineData(PlanetStatus.Todo, PlanetStatus.NotOk)]
        [InlineData(PlanetStatus.EnRoute, PlanetStatus.Ok)]
        [InlineData(PlanetStatus.EnRoute, PlanetStatus.NotOk)]
        [InlineData(PlanetStatus.Ok, PlanetStatus.Todo)]
        [InlineData(PlanetStatus.NotOk, PlanetStatus.Todo)]
        public async Task UpdatePlanetCommand_InvalidState_ThrowsValidationExceptionAsync(PlanetStatus newStatus, PlanetStatus oldStatus)
        {
            //Arrange
            var editPlanet = new EditPlanet
            {
                Id = 1,
                Description = "test",
                DescriptionAuthorId = 1,
                PlanetStatus = newStatus
            };

            _contextMock.Setup(c => c.Planets)
                        .ReturnsDbSet(new List<Planet> { new Planet { Id = 1, Status = oldStatus } });

            //Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _commandHandler.Handle(new UpdatePlanetCommand(editPlanet), CancellationToken.None));
        }

        [Fact]
        public async Task UpdatePlanetCommand_ValidInput_ThrowsDatabaseExceptionAsync()
        {
            //Arrange
            var editPlanet = new EditPlanet
            {
                Id = 1,
                Description = "test",
                DescriptionAuthorId = 1,
                PlanetStatus = PlanetStatus.EnRoute
            };

            _contextMock.Setup(c => c.Planets)
                        .ReturnsDbSet(new List<Planet> { new Planet { Id = 1, Status = PlanetStatus.Todo } });
            _contextMock.Setup(c => c.Descriptions)
                        .ReturnsDbSet(new List<Description>());
            _contextMock.Setup(c => c.SaveChanges())
                        .Throws(new Exception());

            //Assert
            await Assert.ThrowsAsync<DatabaseException>(async () => await _commandHandler.Handle(new UpdatePlanetCommand(editPlanet), CancellationToken.None));
        }

        [Fact]
        public async Task UpdatePlanetCommand_ValidInput_Succedes()
        {
            //Arrange
            var editPlanet = new EditPlanet
            {
                Id = 1,
                Description = "test",
                DescriptionAuthorId = 1,
                PlanetStatus = PlanetStatus.EnRoute
            };

            _contextMock.Setup(c => c.Planets)
                        .ReturnsDbSet(new List<Planet> { new Planet { Id = 1, Status = PlanetStatus.Todo } });
            _contextMock.Setup(c => c.Descriptions)
                        .ReturnsDbSet(new List<Description>());
            _contextMock.Setup(c => c.SaveChanges())
                        .Returns(It.IsAny<int>());

           //Assert
           await _commandHandler.Handle(new UpdatePlanetCommand(editPlanet), CancellationToken.None);
        }
    }
}
