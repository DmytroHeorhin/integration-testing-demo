using Domain.Repositories;
using Moq;

namespace UnitTests;

public class UserRepositoryTests
{
    [Fact]
    public async Task GetUserIdByEmailAsync_ReturnsCorrectUserId()
    {
        // Arrange
        var mockDbClient = new Mock<IDbClient>();
        var repository = new UserRepository(mockDbClient.Object);
        var email = "test@example.com";
        var expectedUserId = 42;

        mockDbClient.Setup(db => db.QuerySingleOrDefaultAsync<int?>(
            "SELECT Id FROM users WHERE Email = @Email",
            It.IsAny<object>()
        )).ReturnsAsync(expectedUserId);

        // Act
        var userId = await repository.GetUserIdByEmailAsync(email);

        // Assert
        Assert.Equal(expectedUserId, userId);
    }
}