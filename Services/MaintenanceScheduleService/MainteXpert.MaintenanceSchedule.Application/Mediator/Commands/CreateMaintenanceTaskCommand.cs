namespace MainteXpert.MaintenanceSchedule.Application.Mediator.Commands
{
    public class CreateMaintenanceTaskCommand : IRequest<ResponseModel<MaintenanceTaskModel>>
    {
        public MaintenanceTaskModel MaintenanceTaskModel { get; set; }

        public CreateMaintenanceTaskCommand(MaintenanceTaskModel maintenanceTaskModel)
        {
            MaintenanceTaskModel = maintenanceTaskModel;
        }
    }
}
