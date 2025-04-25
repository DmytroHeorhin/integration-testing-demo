namespace Domain.Repositories
{
    public interface IDbClient
    {
        T? QuerySingleOrDefault<T>(string query);
    }
}