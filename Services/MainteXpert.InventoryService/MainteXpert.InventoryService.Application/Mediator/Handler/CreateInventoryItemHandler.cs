namespace MainteXpert.InventoryService.Application.Mediator.Handler
{
    public class CreateInventoryItemHandler : IRequestHandler<CreateInventoryItemCommand, ResponseModel<InventoryItemModel>>
    {
        private readonly IMongoRepository<InventoryItemCollection> _collection;
        private readonly IMapper _mapper;

        public CreateInventoryItemHandler(IMongoRepository<InventoryItemCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<InventoryItemModel>> Handle(CreateInventoryItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var item = _mapper.Map<InventoryItemCollection>(request);

                await _collection.InsertOneAsync(item);

                var inventoryItem = _mapper.Map<InventoryItemModel>(item);
                return new ResponseModel<InventoryItemModel>(inventoryItem);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
