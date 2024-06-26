﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    [BsonIgnoreExtraElements]
    public class ToDoItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        public bool IsCompleted { get; set; }
    }
}
