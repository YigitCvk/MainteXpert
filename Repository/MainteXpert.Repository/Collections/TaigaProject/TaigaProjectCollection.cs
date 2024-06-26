namespace MainteXpert.Repository.Collections.TaigaProject
{
    [BsonCollection("TaigaProjects")]
    public class TaigaProjectCollection : Document.Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }
    }
}
