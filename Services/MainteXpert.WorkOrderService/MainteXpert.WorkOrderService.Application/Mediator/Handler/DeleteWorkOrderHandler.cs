namespace MainteXpert.WorkOrderService.Application.Mediator.Handler
{
    public class DeleteWorkOrderHandler : IRequestHandler<DeleteWorkOrderCommand, ResponseModel<bool>>
    {
        private readonly IMongoRepository<WorkOrderCollection> _collection;
        private readonly IMediator _mediator;

        public DeleteWorkOrderHandler(IMongoRepository<WorkOrderCollection> collection, IMediator mediator)
        {
            _collection = collection;
            _mediator = mediator;
        }

        async Task<ResponseModel<bool>> IRequestHandler<DeleteWorkOrderCommand, ResponseModel<bool>>.Handle(DeleteWorkOrderCommand request, CancellationToken cancellationToken)
        {
            var workOrder = await _collection.FindByIdAsync(request.Id);
            if (workOrder == null)
            {
                return new ResponseModel<bool>(false, "Work Order not found");
            }
            await _collection.DeleteByIdAsync(request.Id);
            return new ResponseModel<bool>(true);
        }
    }
}
