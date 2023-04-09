using Moq;
using Moq.EntityFrameworkCore;
using Xpand.CrewsAPI.Data;
using Xpand.CrewsAPI.Data.Models;
using Xpand.CrewsAPI.Queries;
using Xpand.CrewsAPI.QueryHandlers;

namespace Xpand.CrewsAPI.Tests.QueryHandlers
{
    public class GetCrewsQueryHandlerUnitTests
    {
        private readonly GetCrewsQueryHandler _queryHandler;

        private readonly Mock<CrewContext> _contextMock;

        public GetCrewsQueryHandlerUnitTests()
        {
            _contextMock = new Mock<CrewContext>();
            _queryHandler = new GetCrewsQueryHandler(_contextMock.Object);
        }

        [Fact]
        public async Task GetCrewsQuery_ReturnsIQueryableAsync()
        {
            //Arrange
            _contextMock.Setup(c => c.Crews)
                        .ReturnsDbSet(new List<Crew>());
            _contextMock.Setup(c => c.Humans)
                        .ReturnsDbSet(new List<Human>());

            //Act
            var x = await _queryHandler.Handle(new GetCrewsQuery(), CancellationToken.None);

            //Assert
            Assert.NotNull(x);
            Assert.IsAssignableFrom<IQueryable<Crew>>(x);
        }
    }
}
