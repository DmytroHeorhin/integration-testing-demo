namespace IntegrationTests.Infrastructure
{
    internal class BaseTest<TApplicationFixture>
        : IClassFixture<TApplicationFixture>, IDisposable where TApplicationFixture : class, IApplicationFixture
    {
        protected TApplicationFixture Application { get; }

        public BaseTest(TApplicationFixture application)
        {
            Application = application;
            Application.SetupDatabase();
        }

        public void Dispose()
        {
            Application.CleanDatabase();
            Application.CleanKafkaMessages();
        }
    }
}
