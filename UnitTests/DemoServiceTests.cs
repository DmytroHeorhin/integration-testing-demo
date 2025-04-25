using Domain.Messaging;
using Domain.Remote;
using Domain.Repositories;

namespace UnitTests;

public class DemoServiceTests
{
    [Fact]
    public async Task GetDemoMessageAsync_ReturnsCombinedMessage()
    {
        // Arrange
        var localData = "LocalData";
        var remoteData = "RemoteData";
        var expectedMessage = $"Local: {localData}, Remote: {remoteData}";

        var mockRepository = new Mock<IDemoRepository>();
        mockRepository.Setup(repo => repo.GetData()).Returns(localData);

        var mockRemoteApiService = new Mock<IRemoteApiService>();
        mockRemoteApiService.Setup(service => service.FetchDataAsync()).ReturnsAsync(remoteData);

        var mockMessageProducer = new Mock<IMessageProducer>();

        var demoService = new DemoService(mockRepository.Object, mockRemoteApiService.Object, mockMessageProducer.Object);

        // Act
        var result = await demoService.GetDemoMessageAsync();

        // Assert
        Assert.Equal(expectedMessage, result);
        mockMessageProducer.Verify(producer => producer.ProduceAsync(expectedMessage), Times.Once);
    }
}