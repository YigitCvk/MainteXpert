namespace MainteXpert.Repository.Collections.Lookup.PlanGroupCollection
{

    [BsonCollection("ActivityNameGroup")]
    public class ActivityNameGroupCollection : Document.Document
    {
        public string Name { get; set; }
    }
}
