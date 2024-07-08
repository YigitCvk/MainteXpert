
namespace MainteXpert.MaintenanceService.Application.Mediator.Handler
{
    public class GetAllMaintenanceSchedulesHandler : IRequestHandler<GetAllMaintenanceSchedulesQuery, ResponseModel<MaintenanceTaskModel>>
    {
        private readonly IMongoRepository<MaintenanceTaskCollection> _collection;
        private readonly IMapper _mapper;

        public GetAllMaintenanceSchedulesHandler(IMongoRepository<MaintenanceTaskCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<MaintenanceTaskModel>> Handle(GetAllMaintenanceSchedulesQuery request, CancellationToken cancellationToken)
        {
            // tüm görevler
            var filter = Builders<MaintenanceTaskCollection>.Filter.Empty;

            // yalnızca belirli alanları seçme
            var projection = Builders<MaintenanceTaskCollection>.Projection
                .Include(task => task.Id)
                .Include(task => task.Name)
                .Include(task => task.Description)
                .Include(task => task.ScheduledDate)
                .Include(task => task.AssignedTo)
                .Include(task => task.EquipmentId)
                .Include(task => task.Status);

            // MongoDB sorgusu
            var inventoryItems = await _collection.GetCollection()
                .Find(filter)
                .Project<MaintenanceTaskCollection>(projection)
                .ToListAsync(cancellationToken);

            // Mapping işlemi
            var inventoryItemModels = _mapper.Map<List<MaintenanceTaskModel>>(inventoryItems);

            // ResponseModel oluşturulması
            var response = new ResponseModel<MaintenanceTaskModel>(inventoryItemModels);

            return response;
        }
    }
}
