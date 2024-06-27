using MainteXpert.MaintenanceSchedule.Application.Models;
using MongoDB.Driver;

namespace MainteXpert.WorkOrderService.Application.Mediator.Handler
{
    public class DeleteWorkOrderHandler : IRequestHandler<DeleteWorkOrderCommand, ResponseModel<bool>>
    {
        private readonly IMongoRepository<WorkOrderCollection> _collection;
        private readonly IMediator _mediator;

        public DeleteWorkOrderHandler(IMongoRepository<WorkOrderCollection> collection, IMediator mediator)
        {
            _collection = collection;
            _mediator = mediator;
        }

        async Task<ResponseModel<bool>> IRequestHandler<DeleteWorkOrderCommand, ResponseModel<bool>>.Handle(DeleteWorkOrderCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<WorkOrderCollection>.Filter.Eq(task => task.Id, request.Id);
            var result = await _collection.GetCollection().DeleteOneAsync(filter, cancellationToken);

            if (result.DeletedCount == 0)
            {
                return new ResponseModel<bool>(false, "Task not found");
            }

            return new ResponseModel<bool>(true);
        }
    }
}
