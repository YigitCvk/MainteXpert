using MainteXpert.Repository.Collections.WorkOrder;

namespace MainteXpert.WorkOrderService.Application.Mediator.Handler
{
    public class CreateWorkOrderHandler : IRequestHandler<CreateWorkOrderCommand, ResponseModel<WorkOrderModel>>
    {
        private readonly IMongoRepository<WorkOrderCollection> _collection;
        private readonly IMapper _mapper;

        public CreateWorkOrderHandler(IMongoRepository<WorkOrderCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<WorkOrderModel>> Handle(CreateWorkOrderCommand request, CancellationToken cancellationToken)
        {
            var workOrder = new WorkOrderCollection
            {
                AssignedTo = request.AssignedTo,
                Title = request.Title,
                CreatedById = request.Id,
                Description = request.Description,
                DueDate = request.DueDate,
                Status = request.Status,
            };

            await _collection.InsertOneAsync(workOrder);
            var orderModel = _mapper.Map<WorkOrderModel>(workOrder);
            return new ResponseModel<WorkOrderModel>(orderModel);  
        }
    }
}
