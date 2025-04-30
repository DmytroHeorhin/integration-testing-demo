using Domain;
using Domain.Kafka;
using Domain.Remote;
using Domain.UserContext;
using Domain.Repositories;
using Moq;

namespace UnitTests;

public class NoteServiceTests
{
    [Fact]
    public async Task SaveNoteAsync_CallsDependenciesCorrectly()
    {
        // Arrange
        var mockRemoteApiService = new Mock<IRemoteApiClient>();
        var mockUserRepository = new Mock<IUserRepository>();
        var mockNoteProducer = new Mock<INoteProducer>();
        var mockUserContext = new Mock<IUserContext>();

        var testNote = "TestNote";
        var apiResponse = 5;
        var userEmail = "test@example.com";
        var userId = 42;

        mockUserContext.Setup(uc => uc.GetUserEmail()).Returns(userEmail);
        mockUserRepository.Setup(repo => repo.GetUserIdByEmailAsync(userEmail)).ReturnsAsync(userId);
        mockRemoteApiService.Setup(ras => ras.GetScoreAsync(testNote)).ReturnsAsync(apiResponse);

        var noteService = new NoteService(
            mockRemoteApiService.Object,
            mockNoteProducer.Object,
            mockUserContext.Object,
            mockUserRepository.Object
        );

        // Act
        await noteService.SaveNoteAsync(testNote);

        // Assert
        mockNoteProducer.Verify(np => np.ProduceAsync(It.Is<Note>(n => 
            n.Text == testNote && 
            n.UserId == userId && 
            n.Score == apiResponse
        )), Times.Once);
    }
}
