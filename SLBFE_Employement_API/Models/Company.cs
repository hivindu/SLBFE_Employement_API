using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SLBFE_Employement_API.Models
{
    public class Company
    {
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
    }
}
