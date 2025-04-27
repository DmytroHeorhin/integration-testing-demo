namespace Domain.Repositories
{
    public interface IDbClient
    {
        Task ExecuteAsync(string query, object parameters);
        Task<T> QuerySingleOrDefaultAsync<T>(string query, object parameters);
    }
}