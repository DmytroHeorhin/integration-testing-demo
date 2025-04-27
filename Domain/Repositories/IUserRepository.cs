namespace Domain.Repositories;

public interface IUserRepository
{
    Task<int> GetUserIdByEmailAsync(string email);
}