namespace MainteXpert.ErrorCardService.Application.Mediator.Queries.ErrorCard
{
    public class GetErrorCardAttechmentByIdQuery : IRequest<ResponseModel<AttachmentModel>>
    {
        public string ErrorCardId { get; set; }
        public string AttachmentId { get; set; }
        public bool IsPhoto { get; set; }
        public ImageCompressModel PhotoCompress { get; set; } = new ImageCompressModel();
    }


}

