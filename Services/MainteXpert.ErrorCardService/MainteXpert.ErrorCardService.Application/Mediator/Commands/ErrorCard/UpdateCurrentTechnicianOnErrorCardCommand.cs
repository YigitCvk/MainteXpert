using MainteXpert.Common.Enums;
using MainteXpert.Common.Models.Base;

namespace MainteXpert.ErrorCardService.Application.Mediator.Commands.ErrorCard
{
    public class UpdateCurrentTechnicianOnErrorCardCommand : IRequest<ResponseModel>
    {
        public string ErrorCardId { get; set; }
        public string? Description { get; set; }
        public List<AttachmentModel> Photos { get; set; } = new List<AttachmentModel>();
        public List<AttachmentModel> Documents { get; set; } = new List<AttachmentModel>();
        public ErrorCardAddDropEnum ErrorCardAddDrop { get; set; }

    }
}
