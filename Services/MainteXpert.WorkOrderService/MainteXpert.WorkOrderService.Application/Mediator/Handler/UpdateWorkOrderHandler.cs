namespace MainteXpert.WorkOrderService.Application.Mediator.Handler
{
    public class UpdateWorkOrderHandler : IRequestHandler<UpdateWorkOrderCommand, ResponseModel<WorkOrderModel>>
    {
        private readonly IMongoRepository<WorkOrderCollection> _collection;
        private readonly IMapper _mapper;

        public UpdateWorkOrderHandler(IMongoRepository<WorkOrderCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<WorkOrderModel>> Handle(UpdateWorkOrderCommand request, CancellationToken cancellationToken)
        {
            var workOrder = await _collection.FindByIdAsync(request.Id);
            if (workOrder == null)
            {
                return new ResponseModel<WorkOrderModel>(null, "Work order not found");
            }

            workOrder.Name = request.Name;
            workOrder.Description = request.Description;
            workOrder.DueDate = request.DueDate;
            workOrder.Status = request.Status;

            await _collection.ReplaceOneAsync(workOrder);

            var workOrderModel = _mapper.Map<WorkOrderModel>(workOrder);
            return new ResponseModel<WorkOrderModel>(workOrderModel);

        }
    }
}
