using System.Text.Json.Serialization;

namespace PodcastApp9000.Models
{
    public class Podcast
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("author")]
        public string Author { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public string ImageUrl { get; set; } = string.Empty;

        [JsonPropertyName("artwork")]
        public string ArtworkUrl { get; set; } = string.Empty;

        [JsonPropertyName("lastUpdateTime")]
        public long LastUpdateTime { get; set; }

        [JsonPropertyName("episodeCount")]
        public int EpisodeCount { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; } = string.Empty;

        [JsonPropertyName("categories")]
        public Dictionary<string, string> Categories { get; set; } = new();

        [JsonPropertyName("explicit")]
        public bool Explicit { get; set; }

        // Helper property to get the best available image URL
        public string BestImageUrl => !string.IsNullOrEmpty(ArtworkUrl)
            ? ArtworkUrl
            : !string.IsNullOrEmpty(ImageUrl)
                ? ImageUrl
                : string.Empty;
    }
}