namespace Domain.Remote;

public class RemoteApiClient : IRemoteApiClient
{
    private readonly HttpClient _httpClient;

    public RemoteApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> GetScoreAsync(string note)
    {
        var response = await _httpClient.GetStringAsync($"https://example.com/api?note={note}");
        return int.Parse(response);
    }
}