using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class ToDoItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public bool IsCompleted { get; set; }
    }
}
