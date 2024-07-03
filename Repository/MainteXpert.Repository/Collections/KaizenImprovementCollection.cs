namespace MainteXpert.Repository.Collections.Kaizen
{
    [BsonCollection("KaizenImprovements")]
    public class KaizenImprovementCollection : Document.Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
