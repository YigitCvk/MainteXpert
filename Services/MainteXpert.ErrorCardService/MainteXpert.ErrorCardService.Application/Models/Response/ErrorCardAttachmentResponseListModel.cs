namespace MainteXpert.ErrorCardService.Application.Models.Response
{
    public class ErrorCardAttachmentResponseListModel
    {
        public List<AttachmentModel> Photos { get; set; } = new List<AttachmentModel>();
        public List<AttachmentModel> Documents { get; set; } = new List<AttachmentModel>();
        public List<AttachmentModel> TechnicianPhotos { get; set; } = new List<AttachmentModel>();
        public List<AttachmentModel> TechnicianDocuments { get; set; } = new List<AttachmentModel>();
    }
}
