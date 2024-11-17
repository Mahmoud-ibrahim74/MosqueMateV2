using System.Text;
using System.Text.Json;

namespace MosqueMateV2.Domain.APIService
{
    public static class ApiClient
    {
        private static readonly HttpClient _httpClient = new();
        private static string _baseUrl;

        static ApiClient()
        {
            // Configure HttpClient
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ApiClient");
        }

        public static void Configure(string city, string country, int method)
        {
            _baseUrl = $"https://api.aladhan.com/v1/timingsByCity/" +
                       $"{DateTime.Now:dd-MM-yyyy}?" +
                       $"city={city}" +
                       $"&country={country}" +
                       $"&method={method}";
        }

        public static async Task<string> GetAsync(string endpoint = "")
        {
            if (string.IsNullOrEmpty(_baseUrl))
                throw new InvalidOperationException("ApiClient is not configured. Call Configure() first.");

            var response = await _httpClient.GetAsync(_baseUrl + endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> PostAsync<T>(string endpoint, T payload)
        {
            if (string.IsNullOrEmpty(_baseUrl))
                throw new InvalidOperationException("ApiClient is not configured. Call Configure() first.");

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl + endpoint, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> PutAsync<T>(string endpoint, T payload)
        {
            if (string.IsNullOrEmpty(_baseUrl))
                throw new InvalidOperationException("ApiClient is not configured. Call Configure() first.");

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_baseUrl + endpoint, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> DeleteAsync(string endpoint)
        {
            if (string.IsNullOrEmpty(_baseUrl))
                throw new InvalidOperationException("ApiClient is not configured. Call Configure() first.");

            var response = await _httpClient.DeleteAsync(_baseUrl + endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> SendAsync(HttpMethod method, string endpoint, object? payload = null)
        {
            if (string.IsNullOrEmpty(_baseUrl))
                throw new InvalidOperationException("ApiClient is not configured. Call Configure() first.");

            var request = new HttpRequestMessage(method, _baseUrl + endpoint);

            if (payload != null)
            {
                request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
