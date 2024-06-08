namespace MainteXpert.Repository.Collections.Lookup.PlanGroupCollection
{
    [BsonCollection("MeasurementGroup")]
    public class MeasurementGroupCollection : Document.Document
    {
        public string MeasurementMethodName { get; set; }
    }
}
