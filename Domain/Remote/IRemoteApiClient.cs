namespace Domain.Remote
{
    public interface IRemoteApiClient
    {
        Task<int> GetOccurrenceCountAsync(string message);
    }
}