namespace MainteXpert.Common.Models.Activity
{
    public class ChildCreatedDateModel
    {
        private PeriodTypeEnum periodType;
        private DateTime now;

        public ChildCreatedDateModel(string periodId, PeriodTypeEnum periodType, DateTime now)
        {
            PeriodId = periodId;
            this.periodType = periodType;
            this.now = now;
        }

        public string PeriodId { get; set; } = string.Empty;
        public PeriodTypeEnum PeriodType { get; set; } = PeriodTypeEnum.None;
        public DateTime CreatedDate { get; set; }
    }
}
