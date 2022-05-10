namespace SLBFE_Employement_API.Models.RequestResponseModels
{
    public class StatusChangeRequest
    {
        public string Id { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
    }
}
