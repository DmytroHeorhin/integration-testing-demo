using System.Net.Http;
using System.Threading.Tasks;

namespace Domain.Remote
{
    public class RemoteApiService : IRemoteApiService
    {
        private readonly HttpClient _httpClient;

        public RemoteApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> FetchDataAsync(string message)
        {
            var response = await _httpClient.GetStringAsync($"https://example.com/api?message={message}");
            return response;
        }
    }
}