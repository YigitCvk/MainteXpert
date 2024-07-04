namespace MainteXpert.Repository.Collections.TPM
{
    [BsonCollection("TPMMetrics")]
    public class TPMMetricsCollection : Document.Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string EquipmentId { get; set; }
        public DateTime Date { get; set; }
        public decimal OEE { get; set; }
        public decimal Availability { get; set; }
        public decimal Performance { get; set; }
        public decimal Quality { get; set; }
    }
}
