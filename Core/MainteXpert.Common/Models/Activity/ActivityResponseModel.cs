namespace MainteXpert.Common.Models.Activity
{
    public class ActivityResponseModel : BaseResponseModel
    {
        public string NameOfActivity { get; set; }

        public ActivityNameGroupModel ActivityName { get; set; }
        public List<PeriodGroupModel> Period { get; set; }
        public DateTime ActivityUpdateTime { get; set; }
        public int RemainingActivityUpdateTime
        {
            get
            {

                var days = (ActivityUpdateTime.Day - DateTime.Now.Day);
                if (days < 0) return 0;
                else return days;
            }
        }
        public ControlTypeGroupModel ControlType { get; set; }
        public ActivityGroupModel ActivityGroupName { get; set; }
        public WorkerModel CurrentOperator { get; set; }
        public UserModel ActivityCreater { get; set; }
        public UserModel ActivityUpdater { get; set; }
        public UserModel ActivityDeleter { get; set; }
        public UserModel ActivityScorer { get; set; }
        public int? ActivityOtonomScore { get; set; }
        public MeasurementGroupModel MeasurementMethod { get; set; }
        public StationGroupModel StationGroup { get; set; }
        public List<StationCardGroupModel> Stations { get; set; }
        public FactoryGroupModel FactoryGroup { get; set; }
        public List<AttachmentModel> Photos { get; set; }
        public int NumberOfPhotos { get; set; } = 0;
        public List<AttachmentModel> Documents { get; set; }
        public int NumberOfDocuments { get; set; } = 0;
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public object ControlValue { get; set; }
        public bool IsSuggested { get; set; }

        public ActivityStatus ActivityStatus { get; set; }
        public DateTimeModel ActivityTotalWorkTime { get; set; }
        public List<ActivityHistoryModel> ActivityHistory { get; set; } = new List<ActivityHistoryModel>();
        public ActivitySuggestionModel ActivitySuggestion { get; set; } = new ActivitySuggestionModel();
    }
}
