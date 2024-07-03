namespace MainteXpert.InventortService.Application.Mediator.Handler
{
    public class GetInventoryItemByIdHandler : IRequestHandler<GetInventoryItemByIdQuery, ResponseModel<InventoryItemModel>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<InventoryItemCollection> _repository;

        public GetInventoryItemByIdHandler(IMapper mapper, IMongoRepository<InventoryItemCollection> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseModel<InventoryItemModel>> Handle(GetInventoryItemByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<InventoryItemCollection>.Filter.Eq(task => task.Id, request.Id);
            var result = await _repository.GetCollection().Find(filter).FirstOrDefaultAsync(cancellationToken);

            if (result == null)
            {
                return new ResponseModel<InventoryItemModel>(null, "Inventory not found");
            }

            var inventoryItem = _mapper.Map<InventoryItemModel>(result);
            return new ResponseModel<InventoryItemModel>(inventoryItem);
        }
    }
}
