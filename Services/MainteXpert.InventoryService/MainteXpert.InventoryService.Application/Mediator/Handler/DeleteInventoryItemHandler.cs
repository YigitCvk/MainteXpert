
namespace MainteXpert.InventoryService.Application.Mediator.Handler
{
    public class DeleteInventoryItemHandler : IRequestHandler<DeleteInventoryItemCommand, ResponseModel<bool>>
    {
        private readonly IMongoRepository<InventoryItemCollection> _collection;
        private readonly IMapper _mapper;

        public DeleteInventoryItemHandler(IMongoRepository<InventoryItemCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<bool>> Handle(DeleteInventoryItemCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<InventoryItemCollection>.Filter.Eq(task => task.Id, request.Id);
            var result = await _collection.GetCollection().DeleteOneAsync(filter, cancellationToken);

            if (result.DeletedCount == 0)
            {
                return new ResponseModel<bool>(false, "Task not found");
            }

            return new ResponseModel<bool>(true);
        }
    }
}
