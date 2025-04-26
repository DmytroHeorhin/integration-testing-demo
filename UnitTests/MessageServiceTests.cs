using Domain;
using Domain.Kafka;
using Domain.Remote;
using Domain.Repositories;
using Domain.UserContext;
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
            var mockRepository = new Mock<IMessageRepository>();
            var mockMessageProducer = new Mock<IKafkaProducer>();
            var mockUserContext = new Mock<IUserContext>();

            var testMessage = "TestMessage";
            var apiResponse = 5;
            var userEmail = "test@example.com";

            mockUserContext.Setup(uc => uc.GetUserEmail()).Returns(userEmail);
            mockRemoteApiService.Setup(ras => ras.GetOccurrenceCountAsync(testMessage)).ReturnsAsync(apiResponse);

            var messageService = new MessageService(
                mockRemoteApiService.Object,
                mockRepository.Object,
                mockMessageProducer.Object,
                mockUserContext.Object
            );

            // Act
            await messageService.SaveMessageAsync(testMessage);

            // Assert
            mockRepository.Verify(repo => repo.SaveMessageAsync(testMessage, apiResponse, userEmail), Times.Once);
            mockMessageProducer.Verify(mp => mp.ProduceAsync(testMessage), Times.Once);
        }
    }
}