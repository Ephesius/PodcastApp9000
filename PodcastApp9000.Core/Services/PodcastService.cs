using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using PodcastApp9000.Models;

namespace PodcastApp9000.PodcastApp9000.Core.Services
{
    public class PodcastService : IPodcastService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiSecret;
        private readonly SemaphoreSlim _throttle;
        private DateTime _lastRequestTime = DateTime.MinValue;
        private const int MIN_REQUEST_INTERVAL_MS = 500;
        private const int MAX_RESULTS = 20;

        public PodcastService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.podcastindex.org/api/1.0/")
            };
            _apiKey = "XVESFAQZ5AH2KCXBYTJC";
            _apiSecret = "xxARYLW2bhdhZwK#AUNNtvwncp$7Y#fk$dHqV^nx";
            _throttle = new SemaphoreSlim(1, 1);

            // Set a default User-Agent (matching their sample's style)
            //_httpClient.DefaultRequestHeaders.Add("User-Agent", "PodcastApp9000/1.0");
        }

        private async Task ThrottleRequestAsync()
        {
            await _throttle.WaitAsync();
            try
            {
                var timeSinceLastRequest = DateTime.UtcNow - _lastRequestTime;
                if (timeSinceLastRequest.TotalMilliseconds < MIN_REQUEST_INTERVAL_MS)
                {
                    await Task.Delay(MIN_REQUEST_INTERVAL_MS - (int)timeSinceLastRequest.TotalMilliseconds);
                }
                _lastRequestTime = DateTime.UtcNow;
            }
            finally
            {
                _throttle.Release();
            }
        }

        private void SetRequestHeaders()
        {
            var epochTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var hashString = _apiKey + _apiSecret + epochTimestamp;
            var hash = SHA1.HashData(Encoding.UTF8.GetBytes(hashString));
            var hashHex = BitConverter.ToString(hash).Replace("-", "").ToLower();

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Date", epochTimestamp.ToString());
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Key", _apiKey);
            _httpClient.DefaultRequestHeaders.Add("Authorization", hashHex);

            // Add a proper User-Agent
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "PodcastApp9000/1.0 (justin.hardin@snhu.edu)");
        }

        public async Task<IEnumerable<Podcast>> SearchPodcastsAsync(string searchTerm)
        {
            try
            {
                await ThrottleRequestAsync();
                SetRequestHeaders();

                var searchUrl = $"search/byterm?q={Uri.EscapeDataString(searchTerm)}&max={MAX_RESULTS}";
                Debug.WriteLine($"Calling API: {searchUrl}");

                var response = await _httpClient.GetAsync(searchUrl);
                Debug.WriteLine($"Response Status: {response.StatusCode}");

                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Response Content: {content}");

                var searchResult = JsonSerializer.Deserialize<PodcastSearchResponse>(content);
                Debug.WriteLine($"Deserialized Count: {searchResult?.Feeds?.Count ?? 0}");

                return searchResult?.Feeds ?? [];
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Search error: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }
    }

    internal class PodcastSearchResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;  // Changed from bool to string

        [JsonPropertyName("feeds")]
        public List<Podcast> Feeds { get; set; } = [];

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }
}