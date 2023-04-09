using Moq;
using Moq.EntityFrameworkCore;
using System.Security.Cryptography;
using Xpand.PlanetsAPI.Data;
using Xpand.PlanetsAPI.Data.Models;
using Xpand.PlanetsAPI.Handlers;
using Xpand.PlanetsAPI.Queries;

namespace Xpand.PlanetsAPI.Tests.QueryHandlers
{
    public class GetPlanetsQueryHandlerUnitTests
    {
        private readonly GetPlanetsQueryHandler _queryHandler;

        private readonly Mock<PlanetContext> _contextMock;

        public GetPlanetsQueryHandlerUnitTests()
        {
            _contextMock= new Mock<PlanetContext>();
            _queryHandler = new GetPlanetsQueryHandler(_contextMock.Object);
        }

        [Fact]
        public async Task GetCrewsQuery_ReturnsIQueryableAsync()
        {
            //Arrange
            _contextMock.Setup(c => c.Planets)
                        .ReturnsDbSet(new List<Planet>());

            //Act
            var x = await _queryHandler.Handle(new GetPlanetsQuery(), CancellationToken.None);

            //Assert
            Assert.NotNull(x);
            Assert.IsAssignableFrom<IQueryable<Planet>>(x);
        }
    }
}
