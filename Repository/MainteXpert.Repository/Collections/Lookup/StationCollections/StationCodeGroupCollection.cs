namespace MainteXpert.Repository.Collections.Lookup.StationCollections
{
    [BsonCollection("StationCode")]
    public class StationCodeGroupCollection : Document.Document
    {
        public string StationCode { get; set; }
    }
}
