using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Movie_API.Models
{
    public class Movie
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 
        [BsonElement("type")]
        public string? Type { get; set; }
        [BsonElement("title")]
        public string? Title { get; set; }
        [BsonElement("director")]
        public string? Director { get; set; }
        [BsonElement("cast")] 
        public string? Cast { get; set; }
        [BsonElement("country")]
        public string? Country { get; set; }
        [BsonElement("date_added")]
        public string? Date_Added { get; set; }
        [BsonElement("release_year")]
        public int? Release_Year { get; set; }
        [BsonElement("rating")]
        public string? Rating { get; set; }
        [BsonElement("duration")]
        public string? Duration { get; set; }
        [BsonElement("listed_in")]
        public string? Listed_In { get; set; }
        [BsonElement("description")]
        public string? Description { get; set; }

        
    }
}
