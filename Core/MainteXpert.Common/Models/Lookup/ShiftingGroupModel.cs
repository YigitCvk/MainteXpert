namespace MainteXpert.Common.Models.Lookup
{
    public class ShiftingGroupModel : BaseResponseModel
    {
        public string ShiftName { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        // public TimeSpan TimeSpan { get; set; }

    }
}
