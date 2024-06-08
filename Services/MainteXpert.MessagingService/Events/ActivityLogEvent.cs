namespace MainteXpert.MessagingService.Events
{
    public class ActivityLogEvent : IEvent
    {
        public string ActivityId { get; set; }
        public string UserId { get; set; }
        public string ProcessDescription { get; set; }
        public LogTypeEnum LogType { get; set; }
        public LogCategoryEnum LogCategory { get; set; }


    }
}
