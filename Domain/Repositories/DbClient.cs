using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using Dapper;

namespace Domain.Repositories
{
    public class DbClient : IDbClient
    {
        private readonly string _connectionString;

        public DbClient(IOptions<ConnectionStrings> options)
        {
            _connectionString = options.Value.Database
                                ?? throw new ArgumentNullException(nameof(options), "Connection string cannot be null.");
        }

        public T? QuerySingleOrDefault<T>(string query)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QuerySingleOrDefault<T>(query);
        }
    }
}