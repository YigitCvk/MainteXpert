namespace MainteXpert.WorkOrderService.Application.Mediator.Commands
{
    public class CreateWorkOrderCommand : IRequest<ResponseModel<WorkOrderModel>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } // e.g., "Pending", "Completed"
    }
}
