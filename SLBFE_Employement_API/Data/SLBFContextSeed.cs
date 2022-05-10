using MongoDB.Driver;
using SLBFE_Employement_API.Models;

namespace SLBFE_Employement_API.Data
{
    public class SLBFContextSeed
    {
        internal static void SeedData(IMongoCollection<Citizen> citizensCollection)
        {
            bool existCitizens = citizensCollection.Find(p => true).Any();

            if (!existCitizens)
            {
                citizensCollection.InsertManyAsync(GetPreconfiguredIssues());
            }
        }

        private static IEnumerable<Citizen> GetPreconfiguredIssues()
        {
            return new List<Citizen>()
            {
               new Citizen()
               {
                   Name = "Test User",
                   Address = "118/3A, Test Rd, Test street",
                   Affiliation = "",
                   Age = "20",
                   Email = "test@mail.com",
                   Nic = "998256475V",
                   Password = "78ds12ds84ds2",
                   Profession = "Software Engineer",
                   Latitude = "",
                   Longitude = "",
               },
            };
        }
    }
}
