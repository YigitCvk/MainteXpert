
namespace MainteXpert.Repository.Collections.ErrorCard
{
    [BsonCollection("ErrorCard")]
    public class ErrorCardCollection : Document.Document
    {
        public ErrorCardGroupCollection ErrorCardGroup { get; set; }
        public StationCardGroupCollection StationCardGroup { get; set; }
        public WorkerModel CurrentTechnician { get; set; }
        public PriorityEnum Priority { get; set; }
        public ErrorCardStatus ErrorCardStatus { get; set; }
        public string Description { get; set; }
        public string TechnicianDescription { get; set; }
        public List<AttachmentModel> Photos { get; set; } = new List<AttachmentModel>();
        public List<AttachmentModel> TechnicianPhotos { get; set; } = new List<AttachmentModel>();
        public int NumberOfPhotos { get; set; } = 0;
        public List<AttachmentModel> Documents { get; set; } = new List<AttachmentModel>();
        public List<AttachmentModel> TechnicianDocuments { get; set; } = new List<AttachmentModel>();
        public int NumberOfDocuments { get; set; } = 0;
        public List<ErrorCardHistoryModel> ErrorCardHistoryModels { get; set; } = new List<ErrorCardHistoryModel>();
        public UserModel ErrorCardCreater { get; set; }
        public UserModel ErrorCardUpdater { get; set; }
        public UserModel ErrorCardDeleter { get; set; }
        public DateTimeModel ErrorCardActivityTime { get; set; } = new DateTimeModel();
        public DateTimeModel ErrorCardProcessingTime { get; set; } = new DateTimeModel();

    }
}
