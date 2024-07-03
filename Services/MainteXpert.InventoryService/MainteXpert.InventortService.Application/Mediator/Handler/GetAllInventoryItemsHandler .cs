namespace MainteXpert.InventortService.Application.Mediator.Handler
{
    public class GetAllInventoryItemsHandler : IRequestHandler<GetAllInventoryItemsQuery, ResponseModel<InventoryItemModel>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<InventoryItemCollection> _repository;

        public GetAllInventoryItemsHandler(IMapper mapper, IMongoRepository<InventoryItemCollection> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseModel<InventoryItemModel>> Handle(GetAllInventoryItemsQuery request, CancellationToken cancellationToken)
        {
            // tüm görevler
            var filter = Builders<InventoryItemCollection>.Filter.Empty;

            // yalnızca belirli alanları seçme
            var projection = Builders<InventoryItemCollection>.Projection
                .Include(task => task.Id)
                .Include(task => task.Name)
                .Include(task => task.Description)
                .Include(task => task.Status);

            // MongoDB sorgusu
            var inventoryItems = await _repository.GetCollection()
                .Find(filter)
                .Project<InventoryItemCollection>(projection)
                .ToListAsync(cancellationToken);

            // Mapping işlemi
            var inventoryItemModels = _mapper.Map<List<InventoryItemModel>>(inventoryItems);

            // ResponseModel oluşturulması
            var response = new ResponseModel<InventoryItemModel>(inventoryItemModels);

            return response;
        }
    }
}
