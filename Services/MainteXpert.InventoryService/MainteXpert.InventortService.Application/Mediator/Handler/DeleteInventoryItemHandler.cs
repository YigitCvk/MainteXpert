
namespace MainteXpert.InventortService.Application.Mediator.Handler
{
    public class DeleteInventoryItemHandler : IRequestHandler<DeleteInventoryItemCommand, ResponseModel<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<InventoryItemCollection> _repository;

        public DeleteInventoryItemHandler(IMapper mapper, IMongoRepository<InventoryItemCollection> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        async Task<ResponseModel<bool>> IRequestHandler<DeleteInventoryItemCommand, ResponseModel<bool>>.Handle(DeleteInventoryItemCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<InventoryItemCollection>.Filter.Eq(x => x.Id, request.Id);
            var result = await _repository.GetCollection().DeleteOneAsync(filter, cancellationToken);

            if (result.DeletedCount == 0)
            {
                return new ResponseModel<bool>(false, "Task not found");
            }

            return new ResponseModel<bool>(true);
        }
    }
}
