using Api.Controllers;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests;

public class DemoControllerTests
{
    [Fact]
    public async Task CreateDemoRecord_CallsServiceWithCorrectMessage()
    {
        // Arrange
        var mockDemoService = new Mock<IDemoService>();

        var controller = new DemoController(mockDemoService.Object);

        // Act
        var result = await controller.CreateDemoRecord(new CreateDemoRecordRequest
        {
            Message = "TestMessage"
        }) as OkResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        mockDemoService.Verify(service => service.SaveDemoMessageAsync("TestMessage"), Times.Once);
    }
}
