namespace Domain.Repositories
{
    public interface IMessageRepository
    {
        Task SaveMessageAsync(string message, int occurrenceCount, string authorEmail);
    }
}