namespace IntegrationTests.Infrastructure
{
    internal class BaseTest<TApplicationFixture>(TApplicationFixture application)
        : IClassFixture<TApplicationFixture>, IDisposable where TApplicationFixture : class, IApplicationFixture
    {
        protected TApplicationFixture Application { get; } = application;

        public void Dispose()
        {
            Application.CleanDatabase();
            Application.CleanKafkaMessages();
        }
    }
}
