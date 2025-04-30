using Domain.Repositories;
using Domain.Remote;
using Confluent.Kafka;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.Kafka;
using Domain;
using System.Text.Json;
using Domain.UserContext;

namespace Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
    }

    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IRemoteApiClient, RemoteApiClient>();

        services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
        services.AddScoped<IDbClient, DbClient>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();

        services.Configure<KafkaOptions>(configuration.GetSection("Kafka"));
        services.AddSingleton<IProducer<string, Domain.Kafka.Note>>(sp =>
        {
            var config = new ProducerConfig { BootstrapServers = configuration["Kafka:Server"] };
            return new ProducerBuilder<string, Domain.Kafka.Note>(config)
                .SetValueSerializer(new NoteSerializer())
                .Build();
        });
        services.AddScoped<INoteProducer, NoteProducer>();
        services.AddScoped<INoteService, NoteService>();
    }
}