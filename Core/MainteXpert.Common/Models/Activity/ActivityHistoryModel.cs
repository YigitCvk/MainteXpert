namespace MainteXpert.Common.Models.Activity
{
    public class ActivityHistoryModel : BaseHistoryModel
    {
        public ActivityStatus ActivityStatus { get; set; }
        public LookupValue ControlType { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public string ControlValue { get; set; }
    }
}
