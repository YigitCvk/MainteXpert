namespace MainteXpert.Repository.Collections
{
    [BsonCollection("AnalyticsData")]
    public class AnalyticsDataCollection : Document.Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string Category { get; set; }
    }
}
