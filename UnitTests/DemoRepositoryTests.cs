using Domain.Repositories;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests;

public class DemoRepositoryTests
{
    [Fact]
    public async Task SaveDataAsync_InsertsMessageIntoDatabase()
    {
        // Arrange
        var message = "TestMessage";
        var mockDbClient = new Mock<IDbClient>();
        mockDbClient.Setup(client => client.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>())).Returns(Task.CompletedTask);

        var repository = new DemoRepository(mockDbClient.Object);

        // Act
        await repository.SaveDataAsync(message);

        // Assert
        mockDbClient.Verify(client => client.ExecuteAsync("INSERT INTO DemoTable (Message) VALUES (@Message)", It.IsAny<object>()), Times.Once);
    }
}
