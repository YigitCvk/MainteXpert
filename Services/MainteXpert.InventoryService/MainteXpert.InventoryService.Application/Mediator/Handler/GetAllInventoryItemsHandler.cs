namespace MainteXpert.InventoryService.Application.Mediator.Handler
{
    public class GetAllInventoryItemsHandler : IRequestHandler<GetAllInventoryItemsQuery, ResponseModel<List<InventoryItemModel>>>
    {
        private readonly IMongoRepository<InventoryItemCollection> _collection;
        private readonly IMapper _mapper;

        public GetAllInventoryItemsHandler(IMongoRepository<InventoryItemCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<InventoryItemModel>>> Handle(GetAllInventoryItemsQuery request, CancellationToken cancellationToken)
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
            var inventoryItemModel = await _collection.GetCollection()
                .Find(filter)
                .Project<InventoryItemCollection>(projection)
                .ToListAsync(cancellationToken);

            // Mapping işlemi
            var maintenanceTaskModels = _mapper.Map<List<InventoryItemModel>>(inventoryItemModel);

            // ResponseModel oluşturulması
            var response = new ResponseModel<InventoryItemModel>(inventoryItemModel);

            return response;
        }
    }
}
