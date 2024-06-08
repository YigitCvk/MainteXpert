namespace MainteXpert.Repository.Collections.Lookup
{
    [BsonCollection("ErrorCardGroup")]
    public class ErrorCardGroupCollection : Document.Document
    {
        public string ErrorCardGroupName { get; set; }

    }
}
