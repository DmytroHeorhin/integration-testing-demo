using System.Data;

namespace Domain.Repositories
{
    public class DemoRepository(IDbClient dbClient) : IDemoRepository
    {
        private readonly IDbClient _dbClient = dbClient;

        public string? GetData()
        {
            var query = "SELECT TOP 1 Message FROM DemoTable";
            return _dbClient.QuerySingleOrDefault<string?>(query);
        }
    }
}