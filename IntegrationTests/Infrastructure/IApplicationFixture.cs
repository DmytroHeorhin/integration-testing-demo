namespace IntegrationTests.Infrastructure
{
    internal interface IApplicationFixture
    {
        void SetupDatabase();
        void CleanDatabase();
        void CleanKafkaMessages();
    }
}