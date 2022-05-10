using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;

namespace SLBFE_Employement_API.Repository.Interface
{
    public interface IVacanciesRepository
    {
        Task<List<JobVacencies>> GetAllVacanciesAsync();
        Task<List<JobVacencies>> GetAllVerifiedVacanciesAsync();
        Task<List<JobVacencies>> GetAllPendingVacanciesAsync();
        Task<List<Citizen>> GetCitizensByVacancyIdAsync(string id);
        Task<List<JobVacencies>> GetVacanciesByCitizenIdAsync(string citizenId);
        Task<List<JobVacencies>> GetVacanciesByCompanyIdAsync(string userId);
        Task<JobVacencies> CreateVacancies(CreateVacancyRequest request);
        Task<bool> ApproveVacancyAsync(string vacanyId);
        Task<bool> UnApproveVacancyAsync(string vacanyId);
        Task<bool> UpdateVacancyAsync(UpdateVacancyRequest request);
        Task<bool> ApplyForJob(string vacancyId, string citizenId);
        Task<bool> DeleteVacancies(string id);
    }
}
