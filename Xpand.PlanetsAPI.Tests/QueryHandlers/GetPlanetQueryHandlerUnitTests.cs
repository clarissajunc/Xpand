using Moq;
using Moq.EntityFrameworkCore;
using Xpand.PlanetsAPI.Data;
using Xpand.PlanetsAPI.Data.Models;
using Xpand.PlanetsAPI.Handlers;
using Xpand.PlanetsAPI.Queries;

namespace Xpand.PlanetsAPI.Tests.QueryHandlers
{
    public class GetPlanetQueryHandlerUnitTests
    {
        private readonly GetPlanetQueryHandler _queryHandler;

        private readonly Mock<PlanetContext> _contextMock;

        public GetPlanetQueryHandlerUnitTests()
        {
            _contextMock = new Mock<PlanetContext>();
            _queryHandler = new GetPlanetQueryHandler(_contextMock.Object);
        }

        [Fact]
        public async Task GetCrewQuery_ReturnsCrewAsync()
        {
            //Arrange
            var crews = new List<Planet>
            {
                new Planet { Id = 1 },
                new Planet { Id = 2 }
            };
            _contextMock.Setup(c => c.Planets)
                        .ReturnsDbSet(crews);

            //Act
            var query = new GetPlanetQuery
            {
                Predicate = (x) => x.Id == 1
            };

            var x = await _queryHandler.Handle(query, CancellationToken.None);

            //Assert
            Assert.NotNull(x);
            Assert.IsAssignableFrom<Planet>(x);
            Assert.Equal(1, x.Id);
        }

        [Fact]
        public async Task GetCrewQuery_ReturnsNullAsync()
        {
            //Arrange
            var crews = new List<Planet>
            {
                new Planet { Id = 1 },
                new Planet { Id = 2 }
            };
            _contextMock.Setup(c => c.Planets)
                        .ReturnsDbSet(crews);

            //Act
            var query = new GetPlanetQuery
            {
                Predicate = (x) => x.Id == 3
            };

            var x = await _queryHandler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Null(x);
        }
    }
}
