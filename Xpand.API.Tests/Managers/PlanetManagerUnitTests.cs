using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using Xpand.API.Domain.Models;
using Xpand.API.Exceptions;
using Xpand.API.Managers;

namespace Xpand.API.Tests.Managers
{
    public class PlanetManagerUnitTests
    {
        private readonly HttpClient _httpClient;

        private readonly Mock<HttpMessageHandler> _handlerMock;

        private readonly Mock<IOptions<ServicesConfiguration>> _configMock;

        private readonly PlanetManager _manager;

        public PlanetManagerUnitTests()
        {
            _handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(_handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            _configMock = new Mock<IOptions<ServicesConfiguration>>();
            _configMock.Setup(x => x.Value)
                       .Returns(new ServicesConfiguration());

            _manager = new PlanetManager(_httpClient, _configMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_NotSuccesfulStatusCode_ReturnsEmptyListAsync()
        {
            //Arrange
            _handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.NotFound,
                   Content = new StringContent("[{'id':1,'value':'1'}]"),
               });

            //Act
            var result = await _manager.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_SuccesfulStatusCode_DeserializationError_ReturnsEmptyListAsync()
        {
            //Arrange
            _handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(""),
               });

            //Act
            var result = await _manager.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_SuccesfulStatusCode_ReturnsListAsync()
        {
            //Arrange
            _handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("[{\"id\":1,\"name\":\"Crew1\",\"captainId\":1,\"captain\":{\"id\":1,\"name\":\"Jonathan Smith\"},\"robots\":[{\"id\":1,\"name\":\"T2011\",\"crewId\":1},{\"id\":2,\"name\":\"T2020\",\"crewId\":1},{\"id\":3,\"name\":\"T2031\",\"crewId\":1}]}]"),
               });


            //Act
            var result = await _manager.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(default(int), true)]
        [InlineData(default(int), false)]
        [InlineData(1, true)]
        public async Task UpdateAsync_InvalidInput_ThrowsArgumentNullException(int id, bool nullEditPlanet)
        {
            //Arrange
            var editPlanet = nullEditPlanet ? null : new EditPlanet();

            //Act, Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _manager.UpdateAsync(id, editPlanet));
        }

        [Fact]
        public async Task UpdateAsync_InvalidInput_ThrowsValidationException()
        {
            await Assert.ThrowsAsync<ValidationException>(async () => await _manager.UpdateAsync(1, new EditPlanet { Id = 2 }));
        }

        [Fact]
        public async Task UpdateAsync_ValidInput_ReturnsTaskCompletedAsync()
        {
            //Arrange
            //Arrange
            _handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK
               });

            //Act, Assert
            await _manager.UpdateAsync(1, new EditPlanet { Id = 1 });
        }
    }
}
