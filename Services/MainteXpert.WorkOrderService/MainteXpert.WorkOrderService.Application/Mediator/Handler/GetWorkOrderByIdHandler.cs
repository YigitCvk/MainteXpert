
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
            var workOrder = await _collection.FindByIdAsync(request.Id);
            if (workOrder == null)
            {
                return new ResponseModel<WorkOrderModel>(null,"Work Order not found");
            }
            var workOrderModel = _mapper.Map<WorkOrderModel>(workOrder);
            return new ResponseModel<WorkOrderModel> ( workOrderModel );
        }
    }
}
