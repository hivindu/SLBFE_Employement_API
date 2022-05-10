using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;

namespace SLBFE_Employement_API.Extensions
{
    public static class CitizensMapper
    {
        public static Citizen ToModel(this AddCitizensRequest data) => new()
        {
            Name = data.Name,
            Address = data.Address,
            Affiliation = data.Affiliation,
            Age = data.Age,
            Email = data.Email,
            Latitude = data.Latitude,
            Longitude = data.Longitude,
            Nic = data.Nic,
            Password = data.Password,
            Profession = data.Profession,
        };
    }
}
