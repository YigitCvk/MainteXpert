namespace MainteXpert.WorkOrderService.Application.Mediator.Queries
{
    public class GetWorkOrderByIdQuery : IRequest<ResponseModel<WorkOrderModel>>
    {
        public string Id { get; set; }
    }
}
