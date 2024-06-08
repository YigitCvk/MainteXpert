namespace MainteXpert.Common.Models.ErrorCard
{
    public class ErrorCardHistoryModel : BaseHistoryModel
    {

        public ErrorCardStatus ErrorCardStatus { get; set; }
        public string Description { get; set; }
        public List<AttachmentModel> Photos { get; set; } = new List<AttachmentModel>();
        public int NumberOfPhotos
        {
            get
            {
                return this.Photos.Count;
            }
            set { }
        }
        public List<AttachmentModel> Documents { get; set; } = new List<AttachmentModel>();
        public int NumberOfDocuments
        {
            get
            {
                return this.Documents.Count;
            }
            set { }
        }

    }
}
