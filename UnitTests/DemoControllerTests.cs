namespace UnitTests;

public class DemoControllerTests
{
    [Fact]
    public async Task GetHello_ReturnsExpectedMessage()
    {
        // Arrange
        var expectedMessage = "Hello, World!";
        var mockDemoService = new Mock<IDemoService>();
        mockDemoService.Setup(service => service.GetDemoMessageAsync()).ReturnsAsync(expectedMessage);

        var controller = new DemoController(mockDemoService.Object);

        // Act
        var result = await controller.GetHello() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(expectedMessage, result.Value);
    }
}
