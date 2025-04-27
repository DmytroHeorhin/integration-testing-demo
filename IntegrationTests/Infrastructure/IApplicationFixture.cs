namespace IntegrationTests.Infrastructure
{
    internal interface IApplicationFixture
    {
        void CleanDatabase();
        void CleanKafkaMessages();
        void SetupDatabase();
    }
}