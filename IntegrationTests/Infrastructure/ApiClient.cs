using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Api.Controllers;

namespace IntegrationTests.Infrastructure
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<HttpResponseMessage> CreateMessageAsync(string message)
        {
            return _httpClient.PostAsJsonAsync("/api/messages", new CreateMessageRequest { Message = message });
        }
    }
}
