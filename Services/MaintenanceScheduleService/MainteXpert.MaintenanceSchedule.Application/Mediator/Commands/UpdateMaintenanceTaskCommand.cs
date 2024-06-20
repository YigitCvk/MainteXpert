namespace MainteXpert.MaintenanceSchedule.Application.Mediator.Commands
{
    public class UpdateMaintenanceTaskCommand : IRequest<ResponseModel<MaintenanceTaskModel>>
    {
        public MaintenanceTaskModel MaintenanceTaskModel { get; set; }

        public UpdateMaintenanceTaskCommand(MaintenanceTaskModel maintenanceTaskModel)
        {
            MaintenanceTaskModel = maintenanceTaskModel;
        }
    }
}
