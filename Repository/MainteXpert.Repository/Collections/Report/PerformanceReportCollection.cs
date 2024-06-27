namespace MainteXpert.Repository.Collections.Report
{
    [BsonCollection("performanceReports")]
    public class PerformanceReportCollection : Document.Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> Metrics { get; set; }  // List of metrics included in the report
    }
}
