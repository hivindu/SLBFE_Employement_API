using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;

namespace SLBFE_Employement_API.Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllUsersAsync();
        Task<Users> GetUSerByIdAsync(string id);

        Task<Users> VerifyUser(string email, string password);
        Task<bool> UserActivationStatusChange(string id, bool isActive);
        Task<Users> CreateUser(CreateUserRequest request);
        Task<Users> CreateCompanyUser(CreateCompanyUserRequest request);
        Task<bool> DeleteUser(string id);
    }
}
