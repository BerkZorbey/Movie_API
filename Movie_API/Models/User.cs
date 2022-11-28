using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Movie_API.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("User_Name")]
        public string? UserName { get; set; }
        [BsonElement("Email")]
        public string? Email { get; set; }
        [BsonElement("Password")]
        public string? Password { get; set; }
        [BsonElement("Token")]
        public Token? Token { get; set; }
    }
}
