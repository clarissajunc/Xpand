using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xpand.CrewsAPI.Controllers;
using Xpand.CrewsAPI.Data.Models;
using Xpand.CrewsAPI.Queries;

namespace Xpand.CrewsAPI.Tests.Controllers
{
    public class CrewsControllerUnitTests
    {
        private readonly CrewsController _controller;

        private readonly Mock<IMediator> _mediatorMock;

        public CrewsControllerUnitTests()
        {
            _mediatorMock= new Mock<IMediator>(MockBehavior.Strict);
            _controller = new CrewsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResultAsync()
        { 
            //Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCrewsQuery>(), CancellationToken.None))
                         .ReturnsAsync((new List<Crew>()).AsQueryable());
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
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCrewsQuery>(), CancellationToken.None))
                         .ReturnsAsync((new List<Crew> { new Crew() }).AsQueryable());
            //Act
            var result = await _controller.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            
            var crews = (result as OkObjectResult)!.Value;

            Assert.IsAssignableFrom<IEnumerable<Crew>>(crews!);
            Assert.Single((IEnumerable<Crew>)crews!);
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
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCrewQuery>(), CancellationToken.None))
                         .ReturnsAsync(null as Crew);
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
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCrewQuery>(), CancellationToken.None))
                         .ReturnsAsync(new Crew());
            //Act
            var result = await _controller.GetAsync(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var crew = (result as OkObjectResult)!.Value;

            Assert.IsType<Crew>(crew);
        }
    }
}
