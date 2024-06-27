namespace MainteXpert.WorkOrderService.Application.Mediator.Handler
{
    public class GetAllWorkOrdersHandler : IRequestHandler<GetAllWorkOrdersQuery, ResponseModel<List<WorkOrderModel>>>
    {
        private readonly IMongoRepository<WorkOrderCollection> _collection;
        private readonly IMapper _mapper;

        public GetAllWorkOrdersHandler(IMongoRepository<WorkOrderCollection> repository, IMapper mapper)
        {
            _collection = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<WorkOrderModel>>> Handle(GetAllWorkOrdersQuery request, CancellationToken cancellationToken)
        {
            // Tüm görevler
            var filter = Builders<WorkOrderCollection>.Filter.Empty;

            // Yalnızca belirli alanları seçme
            var projection = Builders<WorkOrderCollection>.Projection
                .Include(task => task.Id)
                .Include(task => task.Title)
                .Include(task => task.Description)
                .Include(task => task.Status)
                .Include(task => task.CreatedDate)
                .Include(task => task.DueDate)
                .Include(task => task.AssignedTo);

            // MongoDB sorgusu
            var workOrders = await _collection.GetCollection()
                .Find(filter)
                .Project<WorkOrderCollection>(projection)
                .ToListAsync(cancellationToken);

            // Mapping işlemi
            var workOrderModels = _mapper.Map<List<WorkOrderModel>>(workOrders);

            // ResponseModel oluşturulması
            var response = new ResponseModel<List<WorkOrderModel>>(workOrderModels);

            return response;
        }
    }
}
