namespace MainteXpert.Repository.Collections.Lookup.PlanGroupCollection
{
    [BsonCollection("ControlTypeGroup")]
    public class ControlTypeGroupCollection : Document.Document
    {
        public string ControlName { get; set; }
        public ControlTypeEnum ControlType { get; set; }

    }
}
