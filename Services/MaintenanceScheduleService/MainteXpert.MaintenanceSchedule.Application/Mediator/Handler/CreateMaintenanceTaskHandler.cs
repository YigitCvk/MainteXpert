
namespace MainteXpert.MaintenanceSchedule.Application.Mediator.Handler
{
    public class CreateMaintenanceTaskHandler : IRequestHandler<CreateMaintenanceTaskCommand, ResponseModel<MaintenanceTaskModel>>
    {
        private readonly IMongoRepository<MaintenanceTaskCollection> _collection;
        private readonly IMapper _mapper;

        public CreateMaintenanceTaskHandler(IMongoRepository<MaintenanceTaskCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<MaintenanceTaskModel>> Handle(CreateMaintenanceTaskCommand request, CancellationToken cancellationToken)
        {
            var maintenanceTask = _mapper.Map<MaintenanceTaskCollection>(request.MaintenanceTaskModel);
            await _collection.GetCollection().InsertOneAsync(maintenanceTask, null, cancellationToken);

            var maintenanceTaskModel = _mapper.Map<MaintenanceTaskModel>(maintenanceTask);
            return new ResponseModel<MaintenanceTaskModel>(maintenanceTaskModel);
        }
    }
}
