namespace MainteXpert.Repository.Collections.Activity
{
    [BsonCollection("Activity")]
    public class ActivityCollection : Document.Document
    {
        public Guid ActivityCommonId { get; set; }
        public string ReActivityId { get; set; }
        public string NameOfActivity { get; set; }
        public ActivityTypeEnum ActivityType { get; set; }
        public long NumberChildCreated { get; set; } = 0;
        public ActivityNameGroupCollection ActivityName { get; set; }
        public List<PeriodGroupCollection> Period { get; set; }
        public WorkerModel CurrentOperator { get; set; }
        public DateTime ActivityUpdateTime { get; set; }
        public ControlTypeGroupCollection ControlType { get; set; }
        public ActivityGroupCollection ActivityGroupName { get; set; }
        public MeasurementGroupCollection MeasurementMethod { get; set; }
        public StationGroupCollection StationGroup { get; set; }
        public List<StationCardGroupCollection> Stations { get; set; } = new List<StationCardGroupCollection>();
        public StationCardGroupCollection CurrentStation { get; set; }
        public FactoryGroupCollection FactoryGroup { get; set; }
        public List<AttachmentModel> Photos { get; set; } = new List<AttachmentModel>();
        public int NumberOfPhotos { get; set; } = 0;
        public List<AttachmentModel> Documents { get; set; } = new List<AttachmentModel>();
        public int NumberOfDocuments { get; set; } = 0;
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public string ControlValue { get; set; }
        public bool IsSuggested { get; set; }
        public List<ActivityHistoryModel> ActivityHistory { get; set; } = new List<ActivityHistoryModel> { };
        public ActivitySuggestionModel ActivitySuggestion { get; set; }
        public UserModel ActivityCreater { get; set; }
        public UserModel ActivityUpdater { get; set; }
        public UserModel ActivityDeleter { get; set; }
        public UserModel ActivityScorer { get; set; }
        public UserModel ActivityCompletor { get; set; }
        public int? ActivityOtonomScore { get; set; }
        public ActivityStatus ActivityStatus { get; set; }
        public DateTime LastChildCreatedDate { get; set; } = DateTime.MinValue;
        public List<ChildCreatedDateModel> ChildCreatedDates { get; set; } = new List<ChildCreatedDateModel>();

    }
}
