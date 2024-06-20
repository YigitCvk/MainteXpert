namespace MainteXpert.MaintenanceSchedule.Application.Mediator.Handler
{
    public class UpdateMaintenanceTaskHandler : IRequestHandler<UpdateMaintenanceTaskCommand, ResponseModel<MaintenanceTaskModel>>
    {
        private readonly IMongoRepository<MaintenanceTaskCollection> _collection;
        private readonly IMapper _mapper;

        public UpdateMaintenanceTaskHandler(IMongoRepository<MaintenanceTaskCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<MaintenanceTaskModel>> Handle(UpdateMaintenanceTaskCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<MaintenanceTaskCollection>.Filter.Eq(task => task.Id, request.MaintenanceTaskModel.Id);
            var update = Builders<MaintenanceTaskCollection>.Update
                .Set(task => task.Name, request.MaintenanceTaskModel.Name)
                .Set(task => task.Description, request.MaintenanceTaskModel.Description)
                .Set(task => task.ScheduledDate, request.MaintenanceTaskModel.ScheduledDate)
                .Set(task => task.Status, request.MaintenanceTaskModel.Status);

            var result = await _collection.GetCollection().UpdateOneAsync(filter, update, null, cancellationToken);

            if (result.MatchedCount == 0)
            {
                return new ResponseModel<MaintenanceTaskModel>(null, "Task not found");
            }

            var maintenanceTask = await _collection.GetCollection().Find(filter).FirstOrDefaultAsync(cancellationToken);
            var maintenanceTaskModel = _mapper.Map<MaintenanceTaskModel>(maintenanceTask);
            return new ResponseModel<MaintenanceTaskModel>(maintenanceTaskModel);
        }
    }
}