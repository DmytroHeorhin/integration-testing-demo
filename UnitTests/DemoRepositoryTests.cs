using Domain.Repositories;
using Microsoft.Extensions.Options;

namespace UnitTests;

public class DemoRepositoryTests
{
    [Fact]
    public void GetData_ReturnsExpectedData()
    {
        // Arrange
        var expectedData = "TestMessage";

        var mockDbClient = new Mock<IDbClient>();
        mockDbClient.Setup(m => m.QuerySingleOrDefault<string>(It.IsAny<string>()))
                      .Returns(expectedData);

        var repository = new DemoRepository(mockDbClient.Object);

        // Act
        var result = repository.GetData();

        // Assert
        Assert.Equal(expectedData, result);
    }
}