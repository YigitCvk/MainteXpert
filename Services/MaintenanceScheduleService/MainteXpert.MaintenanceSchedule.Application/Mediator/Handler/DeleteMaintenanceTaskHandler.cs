namespace MainteXpert.MaintenanceSchedule.Application.Mediator.Handler
{
    public class DeleteMaintenanceTaskHandler : IRequestHandler<DeleteMaintenanceTaskCommand, ResponseModel<bool>>
    {
        private readonly IMongoRepository<MaintenanceTaskCollection> _collection;

        public DeleteMaintenanceTaskHandler(IMongoRepository<MaintenanceTaskCollection> collection)
        {
            _collection = collection;
        }

        public async Task<ResponseModel<bool>> Handle(DeleteMaintenanceTaskCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<MaintenanceTaskCollection>.Filter.Eq(task => task.Id, request.Id);
            var result = await _collection.GetCollection().DeleteOneAsync(filter, cancellationToken);

            if (result.DeletedCount == 0)
            {
                return new ResponseModel<bool>(false, "Task not found");
            }

            return new ResponseModel<bool>(true);
        }
    }
}
