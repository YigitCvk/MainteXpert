namespace MainteXpert.WorkOrderService.Application.Mediator.Commands
{
    public class CreateWorkOrderCommand : IRequest<ResponseModel<WorkOrderModel>>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } // Created, In Progress, Completed, etc.
        public string AssignedTo { get; set; } // User ID

        public CreateWorkOrderCommand(string id, string title, string description, DateTime createdDate, DateTime dueDate, string status, string assignedTo)
        {
            Id = id;
            Title = title;
            Description = description;
            CreatedDate = createdDate;
            DueDate = dueDate;
            Status = status;
            AssignedTo = assignedTo;
        }
    }
}
