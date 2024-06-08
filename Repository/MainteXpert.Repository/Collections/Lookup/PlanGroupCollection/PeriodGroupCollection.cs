namespace MainteXpert.Repository.Collections.Lookup.PlanGroupCollection
{
    [BsonCollection("PeriodGroup")]
    public class PeriodGroupCollection : Document.Document
    {
        public string PeriodName { get; set; }
        public PeriodTypeEnum PeriodType { get; set; }

        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }

        [BsonIgnore]
        public TimeOnly StartDate
        {
            get
            {
                return new TimeOnly(hour: StartHour, minute: StartMinute);
            }
            set
            {
            }
        }
        [BsonIgnore]
        public TimeOnly EndDate
        {
            get
            {
                return new TimeOnly(hour: EndHour, minute: EndMinute);
            }
            set
            {
            }
        }



    }
}
