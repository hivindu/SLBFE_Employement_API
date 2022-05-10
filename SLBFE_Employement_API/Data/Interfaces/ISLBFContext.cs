using MongoDB.Driver;
using SLBFE_Employement_API.Models;

namespace SLBFE_Employement_API.Data.Interfaces
{
    public interface ISLBFContext
    {
        IMongoCollection<Citizen> citizen { get; }
        IMongoCollection<Qualification> quolifications { get; }
        IMongoCollection<Complaint> complaints { get; }
        IMongoCollection<Users> users { get; }
        IMongoCollection<JobVacencies> vacancies { get; }
    }
}
