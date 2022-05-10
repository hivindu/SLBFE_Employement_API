using MongoDB.Driver;
using SLBFE_Employement_API.Data.Interfaces;
using SLBFE_Employement_API.Extensions;
using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;
using SLBFE_Employement_API.Repository.Interface;

namespace SLBFE_Employement_API.Repository
{
    public class CitizensRepository : ICitizenRepositories
    {
        private readonly ISLBFContext _context;

        public CitizensRepository(ISLBFContext context)
        {
            _context = context;
        }

        public async Task<List<Citizen>> GetContactsByNicAsync(string nic)
        {
            var connectionsList = new List<Citizen>();

            var citizen = await GetCitizenByNicAsync(nic).ConfigureAwait(false);
            if (citizen != null)
            {
                var connections = citizen.Connections;
                if (connections != null && connections.Any())
                {
                    for (int count = 0; count < connections.Count; count++)
                    {
                        var citizenInConnection = await GetCitizenByIdAsync(connections[count]).ConfigureAwait(false);
                        if(citizenInConnection != null)
                            connectionsList.Add(citizenInConnection);
                    }
                    
                }
                
            }

            return connectionsList;
        }

        public async Task<List<Citizen>> GetAllCitizensAsync()
        {
            return await _context.citizen.Find(p => true).ToListAsync();
        }

        public async Task<Citizen> GetCitizenByIdAsync(string id)
        {
            return await _context.citizen.Find(c => c.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<Citizen> GetCitizenByNicAsync(string nic)
        {

            return await _context.citizen.Find(c => c.Nic == nic).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<bool> VerifyUserCredentialsAsync(LoginRequest data)
        {
            var result = await _context.citizen.Find(c => c.Nic == data.NIC && c.Password == data.Passsword).FirstOrDefaultAsync().ConfigureAwait(false);

            if(result != null)
                return true;

            return false;
        }

        public async Task<List<Citizen>> GetCitizenByQuolificationAsync(string quolification)
        {
            var citizensIdList = await _context.quolifications.Find(q => q.QualificationName.Contains(quolification)).ToListAsync().ConfigureAwait(false);

            if (citizensIdList is null || !citizensIdList.Any())
            {
                return new List<Citizen>();
            }

            var citizensList = new List<Citizen>();

            foreach (var citizenId in citizensIdList)
            {
                var citizenDetails = await _context.citizen.Find(p => p.Id == citizenId.CitizenId).FirstOrDefaultAsync().ConfigureAwait(false);

                citizensList.Add(citizenDetails);
            }

           return citizensList;
        }

        public async Task<List<Citizen>> GetContactsOfCitizenAsync(string nic)
        {
            var citizen = await _context.citizen.Find(c => c.Nic == nic).FirstOrDefaultAsync().ConfigureAwait(false);

            var listOfCitizens = new List<Citizen>();

            if (citizen != null && citizen.Connections != null && citizen.Connections.Any())
            {
                foreach (var connection in citizen.Connections)
                {
                    var connectedCitizen = await _context.citizen.Find(c => c.Id == connection).FirstOrDefaultAsync().ConfigureAwait(false);

                    if(connectedCitizen != null)
                        listOfCitizens.Add(connectedCitizen);
                }
            }
            return listOfCitizens;
        }

        public async Task<Citizen> CreateCitizenAsync(AddCitizensRequest citizen)
        {
            var dataSet = citizen.ToModel();
            await _context.citizen.InsertOneAsync(dataSet).ConfigureAwait(false);

            return await GetCitizenByIdAsync(dataSet.Id).ConfigureAwait(false);
        }

        public async Task<Citizen> UpdateCitizen(string nic, Qualification qualification)
        {
           var citizen = await GetCitizenByNicAsync(nic).ConfigureAwait(false);
            if (citizen != null)
            {
                qualification.CitizenId = citizen.Id;

                await _context.quolifications.InsertOneAsync(qualification).ConfigureAwait(false);
            }

            return citizen;
        }

        public async Task<bool> DeleteCitizenAsync(string id)
        {
            FilterDefinition<Citizen> citizensFilter = Builders<Citizen>.Filter.Eq(p => p.Id, id);
            FilterDefinition<Qualification> quolificationFilter = Builders<Qualification>.Filter.Eq(p => p.CitizenId, id);

            DeleteResult citizenDeleteResult = await _context.citizen.DeleteOneAsync(citizensFilter).ConfigureAwait(false);

            if (citizenDeleteResult.IsAcknowledged && citizenDeleteResult.DeletedCount > 0)
            {
                DeleteResult qualificationDeleteResult = await _context.quolifications.DeleteOneAsync(quolificationFilter).ConfigureAwait(false);

                return qualificationDeleteResult.IsAcknowledged && qualificationDeleteResult.DeletedCount > 0;
            }

            return false;
        }

        public async Task<bool> VerifyCitizensAsync(string nic, bool verified)
        {
            var citizen = await GetCitizenByNicAsync(nic).ConfigureAwait(false);
            if (citizen != null)
            {
                citizen.Verified = verified;

                var result = await _context.citizen.ReplaceOneAsync(filter: s => s.Id == citizen.Id, replacement: citizen).ConfigureAwait(false);

                return result.IsAcknowledged && result.ModifiedCount > 0;
            }

            return false;
        }

        public async Task<bool> AddConnection(string connectionUserId, string userId)
        {
            var citizen = await GetCitizenByIdAsync(userId).ConfigureAwait(false);
            if (citizen != null)
            {
                if (citizen.Connections == null || !citizen.Connections.Any())
                {
                    citizen.Connections = new List<string>();
                }

                citizen.Connections.Add(connectionUserId);

                var result = await _context.citizen.ReplaceOneAsync(filter: s => s.Id == citizen.Id, replacement: citizen).ConfigureAwait(false);

                return result.IsAcknowledged && result.ModifiedCount > 0;
            }

            return false;
        }

        public async Task<bool> RemoveConnection(string connectionUserId, string userId)
        {
            var citizen = await GetCitizenByIdAsync(userId).ConfigureAwait(false);
            if (citizen != null)
            {
                if (citizen.Connections == null || !citizen.Connections.Any())
                {
                    return false;
                }

                citizen.Connections.Remove(connectionUserId);

                var result = await _context.citizen.ReplaceOneAsync(filter: s => s.Id == citizen.Id, replacement: citizen).ConfigureAwait(false);

                return result.IsAcknowledged && result.ModifiedCount > 0;
            }

            return false;
        }

        public async Task<DetailedCitizenResponse> GetCitizenFullDetailsByIdAsync(string id)
        {
            var response = new DetailedCitizenResponse();
            var citizen = await GetCitizenByIdAsync(id).ConfigureAwait(false);
            if (citizen != null)
            {
                response.CitizenBasicDetails = citizen;
                var qualifications = _context.quolifications.Find(p => p.CitizenId == id).ToList();

                if (qualifications != null && qualifications.Any())
                {
                    response.QuolificationsList = qualifications;
                }
            }

            return response;
        }

        public async Task<bool> CitizenActivationStatusChange(string id, bool isActive)
        {
            var citizen = await GetCitizenByIdAsync(id).ConfigureAwait(false);
            if (citizen != null)
            {
                citizen.IsActive = isActive;

                var result = await _context.citizen.ReplaceOneAsync(filter: s => s.Id == citizen.Id, replacement: citizen).ConfigureAwait(false);

                return result.IsAcknowledged && result.ModifiedCount > 0;
            }

            return false;
        }
    }
}
