namespace MainteXpert.ErrorCardService.Application.Mediator.Queries.ErrorCard
{
    public class GetAllErrorCardAttachmentsQuery : IRequest<ResponseModel<ErrorCardAttachmentResponseListModel>>
    {
        public string ErrorCardId { get; set; }
        public bool IsPhoto { get; set; }
        public bool IsPhotoWithData { get; set; }
        public bool IsDocument { get; set; }
        public bool IsDocumentWithData { get; set; }
        public bool IsTechnicianPhoto { get; set; }
        public bool IsTechnicianPhotoWithData { get; set; }
        public bool IsTechnicianDocument { get; set; }
        public bool IsTechnicianDocumentWithData { get; set; }
        public ImageCompressModel PhotoCompress { get; set; } = new ImageCompressModel();
        public ImageCompressModel TechnicianPhotoCompress { get; set; } = new ImageCompressModel();
    }
}
