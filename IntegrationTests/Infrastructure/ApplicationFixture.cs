using IntegrationTests.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

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
                    builder.UseJsonConfigurationFile("appsettings.tests.json");
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
