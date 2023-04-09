using Moq;
using Moq.EntityFrameworkCore;
using Xpand.CrewsAPI.Data;
using Xpand.CrewsAPI.Data.Models;
using Xpand.CrewsAPI.Queries;
using Xpand.CrewsAPI.QueryHandlers;

namespace Xpand.CrewsAPI.Tests.QueryHandlers
{
    public class GetCrewQueryHandlerUnitTests
    {
        private readonly GetCrewQueryHandler _queryHandler;

        private readonly Mock<CrewContext> _contextMock;

        public GetCrewQueryHandlerUnitTests()
        {
            _contextMock = new Mock<CrewContext>();
            _queryHandler = new GetCrewQueryHandler(_contextMock.Object);
        }

        [Fact]
        public async Task GetCrewQuery_ReturnsCrewAsync()
        {
            //Arrange
            var crews = new List<Crew>
            {
                new Crew { Id = 1 },
                new Crew { Id = 2 }
            };
            _contextMock.Setup(c => c.Crews)
                        .ReturnsDbSet(crews);

            //Act
            var query = new GetCrewQuery 
            { 
                Predicate = (x) => x.Id == 1 
            };

            var x = await _queryHandler.Handle(query, CancellationToken.None);

            //Assert
            Assert.NotNull(x);
            Assert.IsAssignableFrom<Crew>(x);
            Assert.Equal(1, x.Id);
        }

        [Fact]
        public async Task GetCrewQuery_ReturnsNullAsync()
        {
            //Arrange
            var crews = new List<Crew>
            {
                new Crew { Id = 1 },
                new Crew { Id = 2 }
            };
            _contextMock.Setup(c => c.Crews)
                        .ReturnsDbSet(crews);

            //Act
            var query = new GetCrewQuery
            {
                Predicate = (x) => x.Id == 3
            };

            var x = await _queryHandler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Null(x);
        }
    }
}
