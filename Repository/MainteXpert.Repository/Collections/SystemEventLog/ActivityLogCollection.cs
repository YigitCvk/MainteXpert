namespace MainteXpert.Repository.Collections.SystemEventLog
{
    [BsonCollection("ActivityLog")]
    public class ActivityLogCollection : Document.Document
    {

        public string ActivityId { get; set; }
        public string UserId { get; set; }
        public string ProcessDescription { get; set; }
        public LogTypeEnum LogType { get; set; }
        public LogCategoryEnum LogCategory { get; set; }
    }
}
