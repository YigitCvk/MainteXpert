namespace MainteXpert.WorkOrderService.Application.Mediator.Commands
{
    public class DeleteWorkOrderCommand : IRequest<ResponseModel<bool>>
    {
        public string Id { get; set; }
    }
}
