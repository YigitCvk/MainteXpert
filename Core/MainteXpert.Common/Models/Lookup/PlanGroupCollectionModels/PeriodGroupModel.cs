namespace MainteXpert.Common.Models.Lookup.PlanGroupCollectionModels
{
    public class PeriodGroupModel : BaseResponseModel
    {
        public string PeriodName { get; set; }
        public PeriodTypeEnum PeriodType { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }

    }

}
