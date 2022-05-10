namespace SLBFE_Employement_API.Settings
{
    public interface ISLBFDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ComplaintCollection { get; set; }
        string CitizensCollection { get; set; }
        string QuolificationsCollection { get; set; }
        string UsersCollection { get; set; }
        string JobVacanciesCollection { get; set; }
    }
}
