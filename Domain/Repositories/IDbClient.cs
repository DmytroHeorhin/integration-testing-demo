namespace Domain.Repositories
{
    public interface IDbClient
    {
        Task ExecuteAsync(string query, object parameters);
    }
}