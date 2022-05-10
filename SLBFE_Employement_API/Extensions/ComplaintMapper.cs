using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;

namespace SLBFE_Employement_API.Extensions
{
    public static class ComplaintMapper
    {
        public static Complaint ToModel(this CreateComplaintrequest request) => new() {
            UserId = request.UserId,
            ComplaintMessage = request.ComplaintMessage,
        };
    }
}
