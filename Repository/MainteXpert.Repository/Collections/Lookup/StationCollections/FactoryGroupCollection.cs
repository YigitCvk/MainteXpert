namespace MainteXpert.Repository.Collections.Lookup.StationCollections
{
    [BsonCollection("FactoryGroup")]
    public class FactoryGroupCollection : Document.Document
    {
        public string FactoryGroupName { get; set; }
    }
}
