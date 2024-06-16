namespace MainteXpert.ErrorCardService.Application.Mediator.Commands.ErrorCard
{
    public class UpdateErrorCardAttachmentCommand : IRequest<ResponseModel>
    {
        public string ErrorCardId { get; set; }
        public AttachmentType AttachmentType { get; set; }
        public List<AttachmentModel> Attachments { get; set; }
    }
}
