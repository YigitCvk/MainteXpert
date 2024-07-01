namespace MainteXpert.InventoryService.Application.Mediator.Handler
{
    public class UpdateInventoryItemHandler : IRequestHandler<UpdateInventoryItemCommand, ResponseModel<InventoryItemModel>>
    {
        private readonly IMongoRepository<InventoryItemCollection> _collection;
        private readonly IMapper _mapper;

        public UpdateInventoryItemHandler(IMongoRepository<InventoryItemCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<InventoryItemModel>> Handle(UpdateInventoryItemCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<InventoryItemCollection>.Filter.Eq(p => p.Id, request.Id);
            var update = Builders<InventoryItemCollection>.Update
                .Set(p => p.Name, request.Name)
                .Set(p => p.Description, request.Description)
                .Set(p => p.Quantity, request.Quantity)
                .Set(p => p.Price, request.Price);

            var result = await _collection.FindOneAndUpdateAsync(filter, update);
            if (result == null)
            {
                return new ResponseModel<InventoryItemModel>(null, "Item not found");
            }

            var inventoryItem = _mapper.Map<InventoryItemModel>(result);
            return new ResponseModel<InventoryItemModel>(inventoryItem);
        }
    }
}
