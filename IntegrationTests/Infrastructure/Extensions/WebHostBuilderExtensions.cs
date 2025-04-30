using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Infrastructure.Extensions;

public static class WebHostBuilderExtensions
{
    public static IWebHostBuilder UseJsonConfigurationFile(this IWebHostBuilder builder, string testConfigFileName)
    {
        var path = Path.GetDirectoryName(typeof(WebHostBuilderExtensions).Module.FullyQualifiedName);

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile(Path.Combine(path!, testConfigFileName), false);
        });

        return builder;
    }
}