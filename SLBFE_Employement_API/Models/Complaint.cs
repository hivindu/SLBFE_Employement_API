using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SLBFE_Employement_API.Models
{
    public class Complaint
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string ComplaintMessage { get; set; } = string.Empty;
        public string? RespondedUserId { get; set; }
        public string? ResponseMessage { get; set; }
    }
}
