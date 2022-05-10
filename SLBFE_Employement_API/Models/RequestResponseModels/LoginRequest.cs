namespace SLBFE_Employement_API.Models.RequestResponseModels
{
    public class LoginRequest
    {
        public string NIC { get; set; } = string.Empty;
        public string Passsword { get; set; } = string.Empty;
    }
}
