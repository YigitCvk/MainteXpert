namespace MainteXpert.MaintenanceSchedule.Application.Mediator.Handler
{
    public class GetAllMaintenanceTaskHandler : IRequestHandler<GetAllMaintenanceTaskQuery, ResponseModel<MaintenanceTaskModel>>
    {
        private readonly IMongoRepository<MaintenanceTaskCollection> _collection;
        private readonly IMapper _mapper;

        public GetAllMaintenanceTaskHandler(IMongoRepository<MaintenanceTaskCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<MaintenanceTaskModel>> Handle(GetAllMaintenanceTaskQuery request, CancellationToken cancellationToken)
        {
            // tüm görevler
            var filter = Builders<MaintenanceTaskCollection>.Filter.Empty;

            // yalnızca belirli alanları seçme
            var projection = Builders<MaintenanceTaskCollection>.Projection
                .Include(task => task.Id)
                .Include(task => task.Name)
                .Include(task => task.Description)
                .Include(task => task.ScheduledDate)
                .Include(task => task.Status);

            // MongoDB sorgusu
            var maintenanceTasks = await _collection.GetCollection()
                .Find(filter)
                .Project<MaintenanceTaskCollection>(projection)
                .ToListAsync(cancellationToken);

            // Mapping işlemi
            var maintenanceTaskModels = _mapper.Map<List<MaintenanceTaskModel>>(maintenanceTasks);

            // ResponseModel oluşturulması
            var response = new ResponseModel<MaintenanceTaskModel>(maintenanceTaskModels);

            return response;
        }
    }
}
