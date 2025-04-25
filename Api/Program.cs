using Domain.Repositories;
using Domain.Messaging;
using Domain.Remote;
using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IDemoRepository, DemoRepository>();
builder.Services.AddHttpClient<IRemoteApiService, RemoteApiService>();
builder.Services.AddScoped<IDbClient, DbClient>();

// Register MessageProducer abstraction
var kafkaConfig = new ProducerConfig { BootstrapServers = builder.Configuration["Kafka:BootstrapServers"] };
builder.Services.AddSingleton<IProducer<string, string>>(new ProducerBuilder<string, string>(kafkaConfig).Build());
builder.Services.AddScoped<IMessageProducer, MessageProducer>();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.Run();
