
namespace MainteXpert.Repository.Collections.ErrorCard
{
    [BsonCollection("ErrorCardCall")]
    public class ErrorCardCallCollection : Document.Document
    {
        public DateTimeModel CallWaitingTime { get; set; }
        public ErrorCardCallStatusEnum ErrorCardCallStatus { get; set; }
        public DateTimeModel CallInterfereTime { get; set; }
        public string Description { get; set; }
        public UserModel CallerOperator { get; set; }
        public UserModel InterferingOperator { get; set; }
        public StationCardGroupModel Station { get; set; }
    }
}
