namespace MainteXpert.ErrorCardService.Application.Mediator.Commands.ErrorCardCall
{
    public class UpdateCurrentTechnicianOnErrorCardCallCommand : IRequest<ResponseModel>
    {
        public string ErrorCardCallId { get; set; }
        public int ErrorCardCallAddDrop { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
