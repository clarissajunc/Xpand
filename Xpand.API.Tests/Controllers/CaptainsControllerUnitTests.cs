using Microsoft.AspNetCore.Mvc;
using Moq;
using Xpand.API.Controllers;
using Xpand.API.Domain.Models;
using Xpand.API.Managers.Abstractions;

namespace Xpand.API.Tests.Controllers
{
    public class CaptainsControllerUnitTests
    {
        private readonly CaptainsController _controller;

        private readonly Mock<ICrewManager> _managerMock;

        public CaptainsControllerUnitTests()
        {
            _managerMock= new Mock<ICrewManager>();
            _controller = new CaptainsController(_managerMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsNoContentResultAsync()
        {
            //Arrange
            _managerMock.Setup(m => m.GetAllCaptainsAsync())
                         .ReturnsAsync(new List<Human>());
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
            _managerMock.Setup(m => m.GetAllCaptainsAsync())
                                     .ReturnsAsync(new List<Human> { new Human() });
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
