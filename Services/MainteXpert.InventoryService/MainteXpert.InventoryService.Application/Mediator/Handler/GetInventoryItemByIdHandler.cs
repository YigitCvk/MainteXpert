namespace MainteXpert.InventoryService.Application.Mediator.Handler
{
    public class GetInventoryItemByIdHandler : IRequestHandler<GetInventoryItemByIdQuery, ResponseModel<InventoryItemModel>>
    {
        private readonly IMongoRepository<InventoryItemCollection> _collection;
        private readonly IMapper _mapper;

        public GetInventoryItemByIdHandler(IMongoRepository<InventoryItemCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<InventoryItemModel>> Handle(GetInventoryItemByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<InventoryItemCollection>.Filter.Eq(p => p.Id, request.Id);
            var projection = Builders<InventoryItemCollection>.Projection
                .Include(p => p.Id)
                .Include(p => p.Name)
                .Include(p => p.Description)
                .Include(p => p.Quantity)
                .Include(p => p.Price);

            var item = await _collection.FindWithProjection(filter, projection);
            if (item == null)
            {
                return new ResponseModel<InventoryItemModel>(null, "Item not found");
            }

            var inventoryItem = _mapper.Map<InventoryItemModel>(item);
            return new ResponseModel<InventoryItemModel>(inventoryItem);
        }
    }
}
