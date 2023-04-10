using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xpand.API.Controllers;
using Xpand.API.Domain.Models;
using Xpand.API.Managers.Abstractions;

namespace Xpand.API.Tests.Controllers
{
    public class DashboardControllerUnitTests
    {
        private readonly DashboardController _controller;

        private readonly Mock<IMapper> _mapperMock;

        private readonly Mock<IPlanetManager> _planetManagerMock;

        private readonly Mock<ICrewManager> _crewManagerMock;

        public DashboardControllerUnitTests()
        {
            _mapperMock = new Mock<IMapper>();
            _planetManagerMock = new Mock<IPlanetManager>();
            _crewManagerMock = new Mock<ICrewManager>();

            _controller = new DashboardController(_mapperMock.Object, _planetManagerMock.Object, _crewManagerMock.Object);
        }

        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        public async Task GetDashboardAsync_NoData_ReturnsOkObjectResult(bool noPlanets, bool noCrews)
        {
            //Arrange
            _planetManagerMock.Setup(m => m.GetAllAsync())
                              .ReturnsAsync(noPlanets ? new List<Planet>() : new List<Planet> { new Planet() });
            _crewManagerMock.Setup(m => m.GetAllCrewsAsync())
                            .ReturnsAsync(noCrews ? new List<Crew>() : new List<Crew> { new Crew() });
            _mapperMock.Setup(m => m.Map<DTOs.Planet>(It.IsAny<Planet>()))
                       .Returns(new DTOs.Planet());

            //Act
            var result = await _controller.GetDashboardAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetDashboardAsync_DataExists_ReturnsOkObjectResult()
        {
            //Arrange
            #region initialize test data
            var planets = new List<Planet>()
            { 
                new Planet()
                {
                    Id = 1,
                    CrewId = 1,
                }
            };
            var crews = new List<Crew>()
            {
                new Crew()
                {
                    Id = 1,
                    Robots= new List<Robot>()
                    {
                        new Robot()
                        {
                            Id = 1,
                            Name = "test"
                        },
                        new Robot()
                        {
                            Id = 2,
                            Name = "test2"
                        },
                    }
                }
            };

            var planetDTO = new DTOs.Planet()
            {
                Id = 1,
                CrewId = 1,

            };

            #endregion

            _planetManagerMock.Setup(m => m.GetAllAsync())
                              .ReturnsAsync(planets);
            _crewManagerMock.Setup(m => m.GetAllCrewsAsync())
                            .ReturnsAsync(crews);
            _mapperMock.Setup(m => m.Map<DTOs.Planet>(It.IsAny<Planet>()))
                       .Returns(planetDTO);

            //Act
            var result = await _controller.GetDashboardAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var returnedObject = (result as OkObjectResult)!.Value;

            Assert.IsAssignableFrom<IEnumerable<DTOs.Planet>>(returnedObject);

            var returnedPlanets = (IEnumerable<DTOs.Planet>)returnedObject!;
            Assert.Single(returnedPlanets!);
            Assert.Equal(2, returnedPlanets!.First().Robots.Count());
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
            var result = await _controller.UpdatePlanetAsync(id, editPlanet);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateAsync_ValidInput_ReturnsNoContentResult()
        {
            //Arrange
            _planetManagerMock.Setup(m => m.UpdateAsync(It.IsAny<int>(), It.IsAny<EditPlanet>()))
                              .Returns(Task.CompletedTask);

            //Act
            var result = await _controller.UpdatePlanetAsync(1, new EditPlanet { Id = 1 });

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
