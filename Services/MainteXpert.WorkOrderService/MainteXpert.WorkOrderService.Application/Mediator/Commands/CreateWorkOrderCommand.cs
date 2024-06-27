namespace MainteXpert.WorkOrderService.Application.Mediator.Commands
{
    public class CreateWorkOrderCommand : IRequest<ResponseModel<WorkOrderModel>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }

        public CreateWorkOrderCommand(string name, string description, DateTime dueDate, string status)
        {
            Name = name;
            Description = description;
            DueDate = dueDate;
            Status = status;
        }
    }
}
