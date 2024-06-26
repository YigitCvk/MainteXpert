namespace MainteXpert.MaintenanceSchedule.Application.Models
{
    [BsonCollection("MaintenanceTaskCollection")]
    public class MaintenanceTaskCollection : Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; }  // Pending, Completed, etc.
    }
}
