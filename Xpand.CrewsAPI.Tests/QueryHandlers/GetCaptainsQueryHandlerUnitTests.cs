using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System.Linq.Expressions;
using Xpand.CrewsAPI.Data;
using Xpand.CrewsAPI.Data.Models;
using Xpand.CrewsAPI.Queries;
using Xpand.CrewsAPI.QueryHandlers;

namespace Xpand.CrewsAPI.Tests.QueryHandlers
{
    public class GetCaptainsQueryHandlerUnitTests
    {
        private readonly GetCaptainsQueryHandler _queryHandler;

        private readonly Mock<CrewContext> _contextMock;

        public GetCaptainsQueryHandlerUnitTests()
        {
            _contextMock = new Mock<CrewContext>();
            _queryHandler = new GetCaptainsQueryHandler(_contextMock.Object);
        }

        [Fact]
        public async Task GetCaptainsQuery_ReturnsIQueryableAsync()
        {
            //Arrange
            _contextMock.Setup(c => c.Crews)
                        .ReturnsDbSet(new List<Crew>());
            _contextMock.Setup(c => c.Humans)
                        .ReturnsDbSet(new List<Human>());

            //Act
            var x = await _queryHandler.Handle(new GetCaptainsQuery(), CancellationToken.None);

            //Assert
            Assert.NotNull(x);
            Assert.IsAssignableFrom<IQueryable<Human>>(x);
        }
    }
}
