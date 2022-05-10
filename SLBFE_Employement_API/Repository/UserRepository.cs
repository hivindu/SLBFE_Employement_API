using MongoDB.Driver;
using SLBFE_Employement_API.Data.Interfaces;
using SLBFE_Employement_API.Extensions;
using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;
using SLBFE_Employement_API.Repository.Interface;

namespace SLBFE_Employement_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ISLBFContext _context;

        public UserRepository(ISLBFContext context)
        {
            _context = context;
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _context.users.Find(p => true).ToListAsync();
        }

        public async Task<Users> GetUSerByIdAsync(string id)
        {
            return await _context.users.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Users> CreateUser(CreateUserRequest request)
        {
            var reques = request.ToModel();
            await _context.users.InsertOneAsync(reques).ConfigureAwait(false);

            return await GetUSerByIdAsync(reques.Id).ConfigureAwait(false);
        }

        public async Task<bool> DeleteUser(string id)
        {
            FilterDefinition<Users> usersFilter = Builders<Users>.Filter.Eq(p => p.Id, id);

            DeleteResult userDeleteResult = await _context.users.DeleteOneAsync(usersFilter).ConfigureAwait(false);

            return userDeleteResult.IsAcknowledged && userDeleteResult.DeletedCount > 0;
        }

        public async Task<Users> VerifyUser(string email, string password)
        {
            return await _context.users.Find(p => p.Email == email && p.Password == password).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<bool> UserActivationStatusChange(string id, bool isActive)
        {
            var user = await GetUSerByIdAsync(id).ConfigureAwait(false);
            if (user != null)
            {
                user.IsActive = isActive;

                var result = await _context.users.ReplaceOneAsync(filter: s => s.Id == user.Id, replacement: user).ConfigureAwait(false);

                return result.IsAcknowledged && result.ModifiedCount > 0;
            }

            return false;
        }

        public async Task<Users> CreateCompanyUser(CreateCompanyUserRequest request)
        {
            var reques = request.ToModel();
            await _context.users.InsertOneAsync(reques).ConfigureAwait(false);

            return await GetUSerByIdAsync(reques.Id).ConfigureAwait(false);
        }
    }
}
