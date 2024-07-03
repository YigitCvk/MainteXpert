namespace MainteXpert.InventortService.Application.Mediator.Handler
{
    public class UpdateInventoryItemHandler : IRequestHandler<UpdateInventoryItemCommand, ResponseModel<InventoryItemModel>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<InventoryItemCollection> _repository;

        public UpdateInventoryItemHandler(IMapper mapper, IMongoRepository<InventoryItemCollection> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseModel<InventoryItemModel>> Handle(UpdateInventoryItemCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<InventoryItemCollection>.Filter.Eq(p => p.Id, request.Id);
            var update = Builders<InventoryItemCollection>.Update
                .Set(p => p.Name, request.Name)
                .Set(p => p.Description, request.Description)
                .Set(p => p.Quantity, request.Quantity)
                .Set(p => p.Price, request.Price);

            var result = await _repository.FindOneAndUpdateAsync(filter, update);
            if (result == null)
            {
                return new ResponseModel<InventoryItemModel>(null, "Item not found");
            }

            var inventoryItem = _mapper.Map<InventoryItemModel>(result);
            return new ResponseModel<InventoryItemModel>(inventoryItem);
        }
    }
}
