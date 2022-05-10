namespace SLBFE_Employement_API.Models.RequestResponseModels
{
    public class AddCitizensRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Affiliation { get; set; } = string.Empty;
        public string Nic { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
