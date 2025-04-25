using Domain;
using Domain.Messaging;
using Domain.Remote;
using Domain.Repositories;
using Moq;

namespace UnitTests;

public class DemoServiceTests
{
    [Fact]
    public async Task SaveDemoMessageAsync_PassesMessageToRemoteApiAndSavesResponse()
    {
        // Arrange
        var message = "TestMessage";
        var apiResponse = "ApiResponse";

        var mockRepository = new Mock<IDemoRepository>();
        var mockRemoteApiService = new Mock<IRemoteApiService>();
        var mockMessageProducer = new Mock<IMessageProducer>();

        mockRemoteApiService.Setup(service => service.FetchDataAsync(message)).ReturnsAsync(apiResponse);

        var demoService = new DemoService(mockRepository.Object, mockRemoteApiService.Object, mockMessageProducer.Object);

        // Act
        await demoService.SaveDemoMessageAsync(message);

        // Assert
        mockRemoteApiService.Verify(service => service.FetchDataAsync(message), Times.Once);
        mockRepository.Verify(repo => repo.SaveDataAsync(apiResponse), Times.Once);
        mockMessageProducer.Verify(producer => producer.ProduceAsync(apiResponse), Times.Once);
    }
}