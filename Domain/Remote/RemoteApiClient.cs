namespace Domain.Remote
{
    public class RemoteApiClient : IRemoteApiClient
    {
        private readonly HttpClient _httpClient;

        public RemoteApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetOccurrenceCountAsync(string message)
        {
            var response = await _httpClient.GetStringAsync($"https://example.com/api?message={message}");
            return int.Parse(response);
        }
    }
}