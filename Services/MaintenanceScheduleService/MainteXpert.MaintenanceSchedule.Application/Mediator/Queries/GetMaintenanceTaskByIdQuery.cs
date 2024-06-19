namespace MainteXpert.MaintenanceSchedule.Application.Mediator.Queries
{
    public class GetMaintenanceTaskByIdQuery : IRequest<ResponseModel<MaintenanceTaskModel>>
    {
        public int Id { get; set; }

        public GetMaintenanceTaskByIdQuery(int id)
        {
            Id = id;
        }
    }
}
