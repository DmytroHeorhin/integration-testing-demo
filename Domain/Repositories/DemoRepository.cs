using System.Data;

namespace Domain.Repositories
{
    public class DemoRepository(IDbClient dbClient) : IDemoRepository
    {
        private readonly IDbClient _dbClient = dbClient;

        public async Task SaveDataAsync(string message)
        {
            var query = "INSERT INTO DemoTable (Message) VALUES (@Message)";
            await _dbClient.ExecuteAsync(query, new { Message = message });
        }
    }
}