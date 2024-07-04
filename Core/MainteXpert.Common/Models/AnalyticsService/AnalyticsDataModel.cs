namespace MainteXpert.Common.Models.AnalyticsService
{
    public class AnalyticsDataModel : BaseResponseModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string Category { get; set; }
    }
}
