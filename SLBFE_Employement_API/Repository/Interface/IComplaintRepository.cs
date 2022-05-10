using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;

namespace SLBFE_Employement_API.Repository.Interface
{
    public interface IComplaintRepository
    {
        Task<List<Complaint>> GetAllComplaintsAsync();
        Task<Complaint> GetComplaintByIdAsync(string id);
        Task<List<Complaint>> GetComplaintsByUserIdAsync(string userId);
        Task<Complaint> CreateComplaint(CreateComplaintrequest request);
        Task<bool> ReplyRoComplaint(ReplyToComplainRequest request);
        Task<bool> DeleteComplaint(string id);
    }
}
