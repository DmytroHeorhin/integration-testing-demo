using Domain;
using Domain.Kafka;
using Domain.Remote;
using Domain.UserContext;
using Domain.Repositories;
using Moq;

namespace UnitTests
{
    public class MessageServiceTests
    {
        [Fact]
        public async Task SaveMessageAsync_CallsDependenciesCorrectly()
        {
            // Arrange
            var mockRemoteApiService = new Mock<IRemoteApiClient>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMessageProducer = new Mock<IMessageProducer>();
            var mockUserContext = new Mock<IUserContext>();

            var testMessage = "TestMessage";
            var apiResponse = 5;
            var userEmail = "test@example.com";
            var userId = 42;

            mockUserContext.Setup(uc => uc.GetUserEmail()).Returns(userEmail);
            mockUserRepository.Setup(repo => repo.GetUserIdByEmailAsync(userEmail)).ReturnsAsync(userId);
            mockRemoteApiService.Setup(ras => ras.GetOccurrenceCountAsync(testMessage)).ReturnsAsync(apiResponse);

            var messageService = new MessageService(
                mockRemoteApiService.Object,
                mockMessageProducer.Object,
                mockUserContext.Object,
                mockUserRepository.Object
            );

            // Act
            await messageService.SaveMessageAsync(testMessage);

            // Assert
            mockMessageProducer.Verify(mp => mp.ProduceAsync(It.Is<Message>(m => 
                m.MessageText == testMessage && 
                m.UserId == userId && 
                m.MessageOccurrenceCount == apiResponse
            )), Times.Once);
        }
    }
}