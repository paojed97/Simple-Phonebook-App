using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace PhonebookAPI.Models
{
    public class Phonebook
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public List<Entries>? Entries { get; set; }
    }

    public class Entries
    {
        public string? Name { get; set; }
        
        [BsonElement("Phone_Number")]
        [JsonPropertyName("Phone_Number")]
        public string? PhoneNumber { get; set; }
    }
}
