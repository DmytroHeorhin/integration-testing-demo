using System.Net.Http.Json;
using Api.Controllers;

namespace IntegrationTests.Infrastructure;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<HttpResponseMessage> CreateNoteAsync(string note)
    {
        return _httpClient.PostAsJsonAsync("/api/notes", new CreateNoteRequest { Note = note });
    }
}
