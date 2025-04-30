namespace Domain.Remote;

public interface IRemoteApiClient
{
    Task<int> GetScoreAsync(string note);
}