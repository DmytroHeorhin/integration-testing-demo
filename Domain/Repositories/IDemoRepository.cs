namespace Domain.Repositories
{
    public interface IDemoRepository
    {
        Task SaveDataAsync(string message);
    }
}