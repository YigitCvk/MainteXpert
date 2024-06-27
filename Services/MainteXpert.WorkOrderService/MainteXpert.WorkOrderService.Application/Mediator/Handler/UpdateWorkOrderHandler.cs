using MainteXpert.MaintenanceSchedule.Application.Models;
using MongoDB.Driver;

namespace MainteXpert.WorkOrderService.Application.Mediator.Handler
{
    public class UpdateWorkOrderHandler : IRequestHandler<UpdateWorkOrderCommand, ResponseModel<WorkOrderModel>>
    {
        private readonly IMongoRepository<WorkOrderCollection> _collection;
        private readonly IMapper _mapper;

        public UpdateWorkOrderHandler(IMongoRepository<WorkOrderCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<WorkOrderModel>> Handle(UpdateWorkOrderCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<WorkOrderCollection>.Filter.Eq(task => task.Id, request.Id);
            var update = Builders<WorkOrderCollection>.Update
                .Set(task => task.Name, request.Name)
                .Set(task => task.Description, request.Description)                
                .Set(task => task.Status, request.Status);

            var result = await _collection.GetCollection().UpdateOneAsync(filter, update, null, cancellationToken);

            if (result.MatchedCount == 0)
            {
                return new ResponseModel<WorkOrderModel>(null, "Task not found");
            }

            var maintenanceTask = await _collection.GetCollection().Find(filter).FirstOrDefaultAsync(cancellationToken);
            var maintenanceTaskModel = _mapper.Map<WorkOrderModel>(maintenanceTask);
            return new ResponseModel<WorkOrderModel>(maintenanceTaskModel);

        }
    }
}
