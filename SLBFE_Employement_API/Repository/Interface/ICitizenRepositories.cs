using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;

namespace SLBFE_Employement_API.Repository.Interface
{
    public interface ICitizenRepositories
    {
        Task<List<Citizen>> GetAllCitizensAsync();
        Task<List<Citizen>> GetCitizenByQuolificationAsync(string quolification);
        Task<Citizen> GetCitizenByIdAsync(string id);
        Task<DetailedCitizenResponse> GetCitizenFullDetailsByIdAsync(string id);
        Task<Citizen> GetCitizenByNicAsync(string nic);
        Task<List<Citizen>> GetContactsByNicAsync(string nic);
        Task<bool> VerifyUserCredentialsAsync(LoginRequest data);
        Task<List<Citizen>> GetContactsOfCitizenAsync(string nic);
        Task<bool> VerifyCitizensAsync(string nic, bool verified);

        Task<bool> CitizenActivationStatusChange(string id, bool isActive);

        Task<bool> AddConnection(string connectionUserId, string userId);

        Task<bool> RemoveConnection(string connectionUserId, string userId);
        Task<Citizen> CreateCitizenAsync(AddCitizensRequest citizen);
        Task<Citizen> UpdateCitizen(string nic,Qualification qualification);
        Task<bool> DeleteCitizenAsync(string id);
    }
}
