

namespace MainteXpert.ErrorCardService.Application.Mediator.Commands.ErrorCard
{
    public class UpsertErrorCardCommad : IRequest<ResponseModel<ErrorCardResponseModel>>
    {
        public string Id { get; set; } = string.Empty;
        public string ErrorCardGroupId { get; set; }
        public string StationCardGroupId { get; set; }
        public PriorityEnum Priority { get; set; } = PriorityEnum.Normal;
        public string Description { get; set; }
        public List<AttachmentModel> Photos { get; set; } = new List<AttachmentModel>();
        public List<AttachmentModel> Documents { get; set; } = new List<AttachmentModel>();


    }
}
