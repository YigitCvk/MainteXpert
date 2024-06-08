namespace MainteXpert.Repository.Collections.Lookup.StationCollections
{
    [BsonCollection("StationGroup")]
    public class StationGroupCollection : Document.Document
    {
        public string StationGroupName { get; set; }
    }
}
