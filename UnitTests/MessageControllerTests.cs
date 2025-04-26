using Api.Controllers;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests;

public class MessageControllerTests
{
    [Fact]
    public async Task CreateMessage_CallsServiceWithCorrectMessage()
    {
        // Arrange
        var mockMessageService = new Mock<IMessageService>();

        var controller = new MessageController(mockMessageService.Object);

        // Act
        var result = await controller.CreateMessage(new CreateMessageRequest
        {
            Message = "TestMessage"
        }) as OkResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        mockMessageService.Verify(service => service.SaveMessageAsync("TestMessage"), Times.Once);
    }
}
