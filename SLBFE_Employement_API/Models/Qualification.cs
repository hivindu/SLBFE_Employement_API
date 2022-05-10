using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SLBFE_Employement_API.Models
{
    public class Qualification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string QualificationName { get; set; } = string.Empty;
        public byte[] CertificateImage { get; set; } = null!;
        public string CitizenId { get; set; } = string.Empty;
    }
}
