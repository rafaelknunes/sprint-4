using Microsoft.AspNetCore.Mvc;
using Moq;
using Sprint4.Services;
using System.Threading.Tasks;
using Xunit;

namespace Sprint4.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task Login_ReturnsOkResult_WithToken()
        {
            // Arrange
            var mockFirebaseService = new Mock<IFirebaseService>();
            mockFirebaseService
                .Setup(service => service.AuthenticateUserAsync("teste@teste.com", "123456"))
                .ReturnsAsync("mocked_token");

            var controller = new AuthController(mockFirebaseService.Object);

            // Act
            var result = await controller.Login("teste@teste.com", "123456") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("mocked_token", result.Value);
        }
    }
}

