namespace MainteXpert.InventoryService.Application.Mediator.Queries
{
    public class GetInventoryItemByIdQuery : IRequest<ResponseModel<InventoryItemModel>>
    {
        public string Id { get; set; }

        public GetInventoryItemByIdQuery(string id)
        {
            Id = id;
        }
    }
}
