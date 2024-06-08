namespace MainteXpert.Repository.Collections.Lookup.StationCollections
{

    [BsonCollection("StationCardGroup")]

    public class StationCardGroupCollection : Document.Document
    {
        public FactoryGroupCollection FactoryGroupCollection { get; set; }
        public StationCodeGroupCollection StationCodeGroupCollection { get; set; }
        public StationNameGroupCollection StationNameGroupCollection { get; set; }
        public StationGroupCollection StationGroupCollection { get; set; }

    }
}
