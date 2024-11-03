using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprint4.Services
{


    public class FirebaseServiceTests
    {
        [Fact]
        public async Task AuthenticateUserAsync_ReturnsResponseBody_WhenAuthenticationIsSuccessful()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"idToken\": \"mocked_token\"}")
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var firebaseService = new FirebaseService(httpClient);

            // Act
            var result = await firebaseService.AuthenticateUserAsync("teste@teste.com", "123456");

            // Assert
            Assert.Contains("mocked_token", result);
        }
    }


}
