using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xpand.PlanetsAPI.Commands;
using Xpand.PlanetsAPI.Controllers;
using Xpand.PlanetsAPI.Data.Models;
using Xpand.PlanetsAPI.DTOs;
using Xpand.PlanetsAPI.Queries;

namespace Xpand.PlanetsAPI.Tests.Controllers
{
    public class PlanetControllerUnitTests
    {
        private readonly PlanetsController _controller;

        private readonly Mock<IMediator> _mediatorMock;

        public PlanetControllerUnitTests()
        {
            _mediatorMock = new Mock<IMediator>(MockBehavior.Strict);
            _controller = new PlanetsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsNoContentResultAsync()
        {
            //Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPlanetsQuery>(), CancellationToken.None))
                         .ReturnsAsync((new List<Planet>()).AsQueryable());
            //Act
            var result = await _controller.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkObjectResultAsync()
        {
            //Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPlanetsQuery>(), CancellationToken.None))
                         .ReturnsAsync((new List<Planet> { new Planet() }).AsQueryable());
            //Act
            var result = await _controller.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var crews = (result as OkObjectResult)!.Value;

            Assert.IsAssignableFrom<IEnumerable<Planet>>(crews!);
            Assert.Single((IEnumerable<Planet>)crews!);
        }

        [Fact]
        public async Task GetAsync_ReturnsBadRequestObjectResultAsync()
        {
            //Act
            var result = await _controller.GetAsync(default);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetAsync_ReturnsNotFoundResultAsync()
        {
            //Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPlanetQuery>(), CancellationToken.None))
                         .ReturnsAsync(null as Planet);
            //Act
            var result = await _controller.GetAsync(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAsync_ReturnsOkObjectResultAsync()
        {
            //Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPlanetQuery>(), CancellationToken.None))
                         .ReturnsAsync(new Planet());
            //Act
            var result = await _controller.GetAsync(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var crew = (result as OkObjectResult)!.Value;

            Assert.IsType<Planet>(crew);
        }

        [Theory]
        [InlineData(default(int), true)]
        [InlineData(default(int), false)]
        [InlineData(1, true)]
        public async Task UpdateAsync_InvalidInput_ReturnsBadRequestObjectResultAsync(int id, bool nullEditPlanet)
        {
            //Arrange
            var editPlanet = nullEditPlanet ? null : new EditPlanet();

            //Act
            var result = await _controller.UpdateAsync(id, editPlanet);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateAsync_ValidInput_ReturnsNoContentResult()
        {
            //Arrange
            _mediatorMock.Setup(m => m.Publish(It.IsAny<UpdatePlanetCommand>(), CancellationToken.None))
                         .Returns(Task.CompletedTask);

            //Act
            var result = await _controller.UpdateAsync(1, new EditPlanet { Id = 1 });

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
