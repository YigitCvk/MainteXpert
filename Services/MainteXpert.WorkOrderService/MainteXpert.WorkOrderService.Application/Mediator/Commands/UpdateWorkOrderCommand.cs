namespace MainteXpert.WorkOrderService.Application.Mediator.Commands
{
    public class UpdateWorkOrderCommand : IRequest<ResponseModel<WorkOrderModel>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }
}
