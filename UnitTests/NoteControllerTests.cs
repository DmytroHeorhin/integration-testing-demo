using Api.Controllers;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests;

public class NoteControllerTests
{
    [Fact]
    public async Task CreateNote_CallsServiceWithCorrectNote()
    {
        // Arrange
        var mockNoteService = new Mock<INoteService>();

        var controller = new NoteController(mockNoteService.Object);

        // Act
        var result = await controller.CreateNote(new CreateNoteRequest
        {
            Note = "TestNote"
        }) as OkResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        mockNoteService.Verify(service => service.SaveNoteAsync("TestNote"), Times.Once);
    }
}
