namespace SLBFE_Employement_API.Settings
{
    public class SLBFDatabaseSettings : ISLBFDatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string ComplaintCollection { get; set; } = string.Empty;
        public string CitizensCollection { get; set; } = string.Empty;
        public string QuolificationsCollection { get; set; } = string.Empty;
        public string UsersCollection { get; set; } = string.Empty;
        public string JobVacanciesCollection { get; set; } = string.Empty;
    }
}
