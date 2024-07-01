namespace MainteXpert.InventoryService.Application.Mediator.Commands
{
    public class DeleteInventoryItemCommand : IRequest<ResponseModel<bool>>
    {
        public string Id { get; set; }

        public DeleteInventoryItemCommand(string id)
        {
            Id = id;
        }
    }
}
