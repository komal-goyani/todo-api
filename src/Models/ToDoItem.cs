using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ToDoAPI.Models
{
    public class ToDoItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsCompleted { get; set; }
    }
}
