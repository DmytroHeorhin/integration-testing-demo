namespace Domain.Remote
{
    public class RemoteApiService : IRemoteApiService
    {
        private readonly HttpClient _httpClient;

        public RemoteApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> FetchDataAsync()
        {
            return await _httpClient.GetStringAsync("https://example.com");
        }
    }
}