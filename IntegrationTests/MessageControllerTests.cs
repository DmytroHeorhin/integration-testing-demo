using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Api.Controllers;
using Domain.Kafka;
using Domain.Remote;
using IntegrationTests.Infrastructure;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Shouldly;

public class MessageControllerTests : BaseTest<ApplicationFixture>
{
    public MessageControllerTests(ApplicationFixture application) : base(application) { }

    [Fact]
    public async Task CreateMessage_ShouldProduceMessageAndReturnOk()
    {
        // Arrange
        var testEmail = "user@example.com";
        var testUserId = 1;
        var testMessage = "Hello Integration!";
        var testOccurrenceCount = 42;

        Application.ArrangeUser(testUserId, testEmail);
        Application.SetupRemoteApiOccurrenceCount(testOccurrenceCount);

        // Act
        var response = await Application.As(testEmail).CreateMessageAsync(testMessage);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Application.MessageProducer.Messages.Single().ShouldBeEquivalentTo(new
        {
            UserId = testUserId,
            MessageText = testMessage,
            MessageOccurrenceCount = testOccurrenceCount
        });
    }
} 