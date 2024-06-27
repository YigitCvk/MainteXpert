
namespace MainteXpert.WorkOrderService.Application.Mediator.Handler
{
    public class GetWorkOrderByIdHandler : IRequestHandler<GetWorkOrderByIdQuery, ResponseModel<WorkOrderModel>>
    {
        private readonly IMongoRepository<WorkOrderCollection> _collection;
        private readonly IMapper _mapper;

        public GetWorkOrderByIdHandler(IMongoRepository<WorkOrderCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<WorkOrderModel>> Handle(GetWorkOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<WorkOrderCollection>.Filter.Eq(task => task.Id, request.Id);
            var maintenanceTask = await _collection.GetCollection().Find(filter).FirstOrDefaultAsync(cancellationToken);

            if (maintenanceTask == null)
            {
                return new ResponseModel<WorkOrderModel>(null, "Task not found");
            }

            var maintenanceTaskModel = _mapper.Map<WorkOrderModel>(maintenanceTask);
            return new ResponseModel<WorkOrderModel>(maintenanceTaskModel);
        }
    }
}
