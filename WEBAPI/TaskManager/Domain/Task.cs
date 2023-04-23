using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TaskManager.CrossCutting.Enums;

namespace TaskManager.Domain
{
    public class Task
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public State State { get; set; }
        public string? AssignedTo { get; set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? ActivedDate { get; set;}
        public DateTime? ResolvedDate { get; set;}
        public DateTime? ClosedDate { get; set; }

        public Task(string title, string description, State state, string? assignedTo)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Description = description;
            State = state;
            AssignedTo = assignedTo;
            CreatedDate = DateTime.UtcNow;
        }

        public void SetActivedDate()
        {
            ActivedDate = DateTime.UtcNow;
        }

        public void SetResolvedDate()
        {
            ResolvedDate = DateTime.UtcNow;
        }

        public void SetClosedDate()
        {
            ClosedDate = DateTime.UtcNow;
        }
    }
}
