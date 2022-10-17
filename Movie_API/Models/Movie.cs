using System.Text.Json.Serialization;

namespace Movie_API.Models
{
    public class Movie
    {
        [JsonPropertyName("show_id")]
        public string Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string[] Cast { get; set; }
        public string Country { get; set; }
        [JsonPropertyName("date_added")]
        public string Date_Added { get; set; }
        [JsonPropertyName("release_year")]
        public int Release_Year { get; set; }
        public string Rating { get; set; }
        public string Duration { get; set; }
        [JsonPropertyName("listed_in")]
        public string[] Listed_In { get; set; }
        public string Description { get; set; }
    }
}
