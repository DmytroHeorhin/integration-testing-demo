using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace IntegrationTests.Infrastructure
{
    public class ApplicationFixture : IApplicationFixture
    {
        protected WebApplicationFactory<Api.Program> Factory { get; init; }

        protected ApplicationFixture()
        {
            Factory = new WebApplicationFactory<Api.Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        ConfigureServices(services);
                    });
                });
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                return connection;
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
                    });
                })
                .CreateClient();

            return new ApiClient(httpClient);
        }

        public void CleanDatabase()
        {
            throw new NotImplementedException();
        }

        public void CleanKafkaMessages()
        {
            throw new NotImplementedException();
        }
    }
}
