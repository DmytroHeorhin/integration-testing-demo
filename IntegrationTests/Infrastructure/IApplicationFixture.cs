namespace IntegrationTests.Infrastructure;

public interface IApplicationFixture
{
    void SetupDatabase();
    void CleanDatabase();
    void CleanKafkaMessages();
}