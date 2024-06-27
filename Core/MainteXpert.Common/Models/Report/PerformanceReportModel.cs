namespace MainteXpert.Common.Models.Report
{
    public class PerformanceReportModel : BaseResponseModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> Metrics { get; set; }
    }
}
