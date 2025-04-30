namespace Domain.Repositories;

public interface ISqlServerClient
{
    Task<T?> QuerySingleOrDefaultAsync<T>(string query, object parameters);
}