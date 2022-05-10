namespace SLBFE_Employement_API.Models.RequestResponseModels
{
    public class CreateVacancyRequest
    {
        public string JobTitle { get; set; } = string.Empty;
        public string JobDescription { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Deadline { get; set; } = string.Empty;
    }
}
