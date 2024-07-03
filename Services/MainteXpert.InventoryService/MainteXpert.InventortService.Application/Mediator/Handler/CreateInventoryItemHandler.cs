namespace MainteXpert.InventortService.Application.Mediator.Handler
{
    public class CreateInventoryItemHandler : IRequestHandler<CreateInventoryItemCommand, ResponseModel<InventoryItemModel>>
    {
        private readonly IMongoRepository<InventoryItemCollection> _repository;
        private readonly IMapper _mapper;

        public CreateInventoryItemHandler(IMongoRepository<InventoryItemCollection> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<InventoryItemModel>> Handle(CreateInventoryItemCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<InventoryItemCollection>(request);

            await _repository.InsertOneAsync(item);

            var inventoryItem = _mapper.Map<InventoryItemModel>(item);
            return new ResponseModel<InventoryItemModel>(inventoryItem);
        }
    }
}
