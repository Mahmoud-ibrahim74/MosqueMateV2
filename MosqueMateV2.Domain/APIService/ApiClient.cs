namespace MosqueMateV2.Domain.APIService
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        public string PrayerLinkApi { get; private set; }
        public ApiClient(string _baseUrl = "", string Country = "", string City = "", int method = 1)
        {
            PrayerLinkApi =
                       $"https://api.aladhan.com/v1/timingsByCity/" +
                       $"{DateTime.Now:dd-MM-yyyy}?" +
                       $"city={City}" +
                       $"&country={Country}" +
                       $"&method={method}";


            _httpClient = new()
            {
                Timeout = TimeSpan.FromSeconds(30),
                BaseAddress = string.IsNullOrEmpty(_baseUrl) ? new Uri(PrayerLinkApi) : new Uri(_baseUrl),
            };
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ApiClient");


        }

        public async Task<string> GetAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_httpClient.BaseAddress);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
