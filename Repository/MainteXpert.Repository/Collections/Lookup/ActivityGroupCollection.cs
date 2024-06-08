namespace MainteXpert.Repository.Collections.Lookup
{

    [BsonCollection("ActivityGroup")]
    public class ActivityGroupCollection : Document.Document
    {
        public string ActivityGroupName { get; set; }
    }
}
