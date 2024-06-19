namespace MainteXpert.MaintenanceSchedule.Application.Mediator.Handler
{
    public class GetMaintenanceTaskByIdHandler : IRequestHandler<GetMaintenanceTaskByIdQuery, ResponseModel<MaintenanceTaskModel>>
    {
        private readonly IMongoRepository<MaintenanceTaskCollection> _collection;
        private readonly IMapper _mapper;

        public GetMaintenanceTaskByIdHandler(IMongoRepository<MaintenanceTaskCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<MaintenanceTaskModel>> Handle(GetMaintenanceTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<MaintenanceTaskCollection>.Filter.Eq(task => task.Id, request.Id);
            var maintenanceTask = await _collection.GetCollection().Find(filter).FirstOrDefaultAsync(cancellationToken);

            if (maintenanceTask == null)
            {
                return new ResponseModel<MaintenanceTaskModel>(null, "Task not found");
            }

            var maintenanceTaskModel = _mapper.Map<MaintenanceTaskModel>(maintenanceTask);
            return new ResponseModel<MaintenanceTaskModel>(maintenanceTaskModel);
        }
    }
}

