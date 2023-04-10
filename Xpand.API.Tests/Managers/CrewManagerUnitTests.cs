using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using Xpand.API.Managers;

namespace Xpand.API.Tests.Managers
{
    public class CrewManagerUnitTests
    {
        private readonly HttpClient _httpClient;

        private readonly Mock<HttpMessageHandler> _handlerMock;

        private readonly Mock<IOptions<ServicesConfiguration>> _configMock;

        private readonly CrewManager _manager;

        public CrewManagerUnitTests()
        {
            _handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(_handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            _configMock = new Mock<IOptions<ServicesConfiguration>>();
            _configMock.Setup(x => x.Value)
                       .Returns(new ServicesConfiguration());

            _manager = new CrewManager(_httpClient, _configMock.Object);
        }

        [Fact]
        public async Task GetAllCrewsAsync_NotSuccesfulStatusCode_ReturnsEmptyListAsync()
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
            var result = await _manager.GetAllCrewsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllCrewsAsync_SuccesfulStatusCode_DeserializationError_ReturnsEmptyListAsync()
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
            var result = await _manager.GetAllCrewsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllCrewsAsync_SuccesfulStatusCode_ReturnsListAsync()
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
            var result = await _manager.GetAllCrewsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllCaptainsAsync_NotSuccesfulStatusCode_ReturnsEmptyListAsync()
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
            var result = await _manager.GetAllCaptainsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllCaptainsAsync_SuccesfulStatusCode_DeserializationError_ReturnsEmptyListAsync()
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
            var result = await _manager.GetAllCaptainsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllCaptainsAsync_SuccesfulStatusCode_ReturnsListAsync()
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
                   Content = new StringContent("[{\"id\":1,\"name\":\"Jonathan Smith\",\"crew\":null},{\"id\":2,\"name\":\"Hannah Intellis\",\"crew\":null}]"),
               });


            //Act
            var result = await _manager.GetAllCaptainsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
