namespace SLBFE_Employement_API.Models.RequestResponseModels
{
    public class DetailedCitizenResponse
    {
        public Citizen CitizenBasicDetails { get; set; } = new ();
        public List<Qualification> QuolificationsList { get; set; } = new();
    }
}
