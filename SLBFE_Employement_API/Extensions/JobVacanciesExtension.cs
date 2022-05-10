using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;

namespace SLBFE_Employement_API.Extensions
{
    public static class JobVacanciesExtension
    {
        public static JobVacencies ToModel(this CreateVacancyRequest request) => new()
        {
            JobTitle = request.JobTitle,
            JobDescription = request.JobDescription,
            UserId = request.UserId,
            Deadline = request.Deadline,
        };

        public static JobVacencies ToModel(this UpdateVacancyRequest request) => new()
        {
            Id = request.Id,
            JobTitle = request.JobTitle,
            JobDescription = request.JobDescription,
            UserId = request.UserId,
            Deadline = request.Deadline,
            IsApproved = request.IsApproved,
        };
    }
}
