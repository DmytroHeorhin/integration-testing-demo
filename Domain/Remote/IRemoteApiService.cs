namespace Domain.Remote
{
    public interface IRemoteApiService
    {
        Task<string> FetchDataAsync(string message);
    }
}