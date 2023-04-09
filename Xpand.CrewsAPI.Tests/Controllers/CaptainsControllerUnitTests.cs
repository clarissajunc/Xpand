using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xpand.CrewsAPI.Controllers;
using Xpand.CrewsAPI.Data.Models;
using Xpand.CrewsAPI.Queries;

namespace Xpand.CrewsAPI.Tests.Controllers
{
    public class CaptainsControllerUnitTests
    {
        private readonly CaptainsController _controller;

        private readonly Mock<IMediator> _mediatorMock;

        public CaptainsControllerUnitTests()
        {
            _mediatorMock = new Mock<IMediator>(MockBehavior.Strict);
            _controller = new CaptainsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsNoContentResultAsync()
        {
            //Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCaptainsQuery>(), CancellationToken.None))
                         .ReturnsAsync((new List<Human>()).AsQueryable());
            //Act
            var result = await _controller.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkObjectResultAsync()
        {
            //Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCaptainsQuery>(), CancellationToken.None))
                         .ReturnsAsync((new List<Human> { new Human() }).AsQueryable());
            //Act
            var result = await _controller.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var crews = (result as OkObjectResult)!.Value;

            Assert.IsAssignableFrom<IEnumerable<Human>>(crews!);
            Assert.Single((IEnumerable<Human>)crews!);
        }
    }
}
