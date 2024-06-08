namespace MainteXpert.Repository.Collections.Lookup.PlanGroupCollection
{
    [BsonCollection("ActivityGroup")]
    public class PlanActivityGroupCollection : Document.Document
    {
        public string ActivityName { get; set; }

    }
}
