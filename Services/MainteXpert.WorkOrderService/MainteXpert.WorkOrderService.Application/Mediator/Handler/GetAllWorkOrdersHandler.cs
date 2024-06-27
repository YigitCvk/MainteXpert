
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
            var workOrders = await _collection.GetAll();
            var workOrderModels = _mapper.Map<List<WorkOrderModel>>(workOrders);
            return new ResponseModel<List<WorkOrderModel>>(workOrderModels);
        }
    }
}