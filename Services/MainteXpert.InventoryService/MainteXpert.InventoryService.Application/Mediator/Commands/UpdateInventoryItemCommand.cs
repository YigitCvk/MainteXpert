namespace MainteXpert.InventoryService.Application.Mediator.Commands
{
    public class UpdateInventoryItemCommand : IRequest<ResponseModel<InventoryItemModel>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
