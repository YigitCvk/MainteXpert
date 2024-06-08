namespace MainteXpert.Repository.Collections.Lookup.StationCollections
{
    [BsonCollection("StationNameGroup")]

    public class StationNameGroupCollection : Document.Document
    {
        public string StationNameGroupName { get; set; }
    }
}
