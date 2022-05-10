namespace SLBFE_Employement_API.Models.RequestResponseModels
{
    public class UpdateCitizenRequest
    {
        public string QuolificationName { get; set; } = string.Empty;
        public byte[] Image { get; set; }
    }
}
