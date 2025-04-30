using Domain.Kafka;
using Domain.Repositories;
using IntegrationTests.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using Moq;
using Domain.Remote;

namespace IntegrationTests.Infrastructure;

public class ApplicationFixture : IApplicationFixture
{
    protected WebApplicationFactory<Api.Program> Factory { get; init; }

    protected SqlConnection DbConnection => new(Factory.Services.GetRequiredService<IOptions<ConnectionStrings>>().Value.Database);

    public FakeNoteProducer NoteProducer { get; } = new();

    public Mock<IRemoteApiClient> RemoteApiClientMock { get; } = new();

    public ApplicationFixture()
    {
        Factory = new WebApplicationFactory<Api.Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseJsonConfigurationFile("appsettings.tests.json");
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IRemoteApiClient>(_ => RemoteApiClientMock.Object);
                });
            });
    }

    public ApiClient As(string email)
    {
        var httpClient = Factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IPolicyEvaluator>(sp => new TestPolicyEvaluator(sp.GetRequiredService<IAuthorizationService>(), email));
                    services.Replace(new ServiceDescriptor(typeof(INoteProducer), _ => NoteProducer, ServiceLifetime.Singleton));
                });
            })
            .CreateClient();

        return new ApiClient(httpClient);
    }

    public void SetupRemoteApiScore(int score)
    {
        RemoteApiClientMock
            .Setup(x => x.GetScoreAsync(It.IsAny<string>()))
            .ReturnsAsync(score);
    }

    public void SetupDatabase()
    {
        var scriptPath = Path.Combine(AppContext.BaseDirectory, "create_users_table.sql");
        var script = File.ReadAllText(scriptPath);

        using var connection = DbConnection;
        connection.Open();

        using var command = new SqlCommand(script, connection);
        command.ExecuteNonQuery();
    }

    public void ArrangeUser(int userId, string email)
    {
        using var connection = DbConnection;
        connection.Open();
        using var cmd = connection.CreateCommand();
        cmd.CommandText = "INSERT INTO users (Id, Email) VALUES (@Id, @Email)";
        cmd.Parameters.AddWithValue("@Id", userId);
        cmd.Parameters.AddWithValue("@Email", email);
        cmd.ExecuteNonQuery();
    }

    public void CleanDatabase()
    {
        using var connection = DbConnection;
        connection.Open();

        using var truncateCommand = new SqlCommand("TRUNCATE TABLE users;", connection);
        truncateCommand.ExecuteNonQuery();
    }

    public void CleanKafkaMessages()
    {
        NoteProducer.CleanProducedMessages();
    }
}
