using MongoDB.Driver;
using SLBFE_Employement_API.Data.Interfaces;
using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Settings;

namespace SLBFE_Employement_API.Data
{
    public class SLBFContext : ISLBFContext
    {
        public SLBFContext(ISLBFDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            citizen = database.GetCollection<Citizen>(settings.CitizensCollection);
            quolifications = database.GetCollection<Qualification>(settings.QuolificationsCollection);
            complaints = database.GetCollection<Complaint>(settings.ComplaintCollection);
            users = database.GetCollection<Users>(settings.UsersCollection);
            vacancies = database.GetCollection<JobVacencies>(settings.JobVacanciesCollection);
        }

        public IMongoCollection<Citizen> citizen { get; }

        public IMongoCollection<Qualification> quolifications { get; }

        public IMongoCollection<Complaint> complaints { get; }

        public IMongoCollection<Users> users { get; }

        public IMongoCollection<JobVacencies> vacancies { get; }
    }
}
