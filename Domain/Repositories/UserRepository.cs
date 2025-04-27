namespace Domain.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbClient _dbClient;

    public UserRepository(IDbClient dbClient)
    {
        _dbClient = dbClient ?? throw new ArgumentNullException(nameof(dbClient));
    }

    public async Task<int> GetUserIdByEmailAsync(string email)
    {
        var query = "SELECT Id FROM users WHERE Email = @Email";
        var userId = await _dbClient.QuerySingleOrDefaultAsync<int?>(query, new { Email = email });

        if (!userId.HasValue)
        {
            throw new Exception("User not found.");
        }

        return userId.Value;
    }
}