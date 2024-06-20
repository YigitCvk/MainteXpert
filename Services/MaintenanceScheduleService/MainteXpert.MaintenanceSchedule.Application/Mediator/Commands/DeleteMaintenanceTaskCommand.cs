namespace MainteXpert.MaintenanceSchedule.Application.Mediator.Commands
{
    public class DeleteMaintenanceTaskCommand : IRequest<ResponseModel<bool>>
    {
        public int Id { get; set; }

        public DeleteMaintenanceTaskCommand(int id)
        {
            Id = id;
        }
    }
}
