using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Expenses.Core.Entities
{
    public class BaseBEntity : BaseAEntity
    {
        public DateTime DateRegistration { get; set; } = DateTime.Now;
    }

    public class BaseAEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public bool IsActive { get; set; } = true;
    }    
}