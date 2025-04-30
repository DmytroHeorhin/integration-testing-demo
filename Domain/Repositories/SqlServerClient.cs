using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using Dapper;

namespace Domain.Repositories;

public class SqlServerClient : ISqlServerClient
{
    private readonly string _connectionString;

    public SqlServerClient(IOptions<ConnectionStrings> options)
    {
        _connectionString = options.Value.Database
                            ?? throw new ArgumentNullException(nameof(options), "Connection string cannot be null.");
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T>(string query, object parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QuerySingleOrDefaultAsync<T>(query, parameters);
    }
}