using Domain.Repositories;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class MessageRepositoryTests
    {
        [Fact]
        public async Task SaveMessageAsync_InsertsMessageWithDetailsIntoDatabase()
        {
            // Arrange
            var mockDbClient = new Mock<IDbClient>();
            var repository = new MessageRepository(mockDbClient.Object);
            var message = "TestMessage";
            var occurrenceCount = 3;
            var authorEmail = "test@example.com";

            // Act
            await repository.SaveMessageAsync(message, occurrenceCount, authorEmail);

            // Assert
            mockDbClient.Verify(db => db.ExecuteAsync(
                "INSERT INTO messages (Message, OccurrenceCount, AuthorEmail) VALUES (@Message, @OccurrenceCount, @AuthorEmail)",
                It.Is<object>(o => 
                    o.GetType().GetProperty("Message").GetValue(o).ToString() == message &&
                    (int)o.GetType().GetProperty("OccurrenceCount").GetValue(o) == occurrenceCount &&
                    o.GetType().GetProperty("AuthorEmail").GetValue(o).ToString() == authorEmail
                )
            ), Times.Once);
        }
    }
}
