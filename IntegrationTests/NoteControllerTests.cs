using System.Net;
using IntegrationTests.Infrastructure;
using Shouldly;

public class NoteControllerTests : BaseTest<ApplicationFixture>
{
    public NoteControllerTests(ApplicationFixture application) : base(application) { }

    [Fact]
    public async Task CreateNote_ShouldProduceNoteAndReturnOk()
    {
        // Arrange
        var email = "user@example.com";
        var userId = 1;
        var note = "Hello Integration!";
        var score = 42;

        Application.ArrangeUser(userId, email);
        Application.SetupRemoteApiScore(score);

        // Act
        var response = await Application.As(email).CreateNoteAsync(note);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        Application.NoteProducer.Messages.Single().ShouldBeEquivalentTo(new
        {
            UserId = userId,
            Text = note,
            Score = score
        });
    }
} 