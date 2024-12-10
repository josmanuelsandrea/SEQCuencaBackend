using System.Text.Json;
using System.Text;

namespace ScaneqCuencaBackend.Services
{
    public class PdfService
    {
        private readonly HttpClient _httpClient;

        public PdfService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Stream> PostDataAsync<T>(string requestUri, T data)
        {
            var jsonContent = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestUri, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStreamAsync();
        }
    }
}
