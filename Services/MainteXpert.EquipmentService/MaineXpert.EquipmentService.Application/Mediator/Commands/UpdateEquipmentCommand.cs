namespace MainteXpert.EquipmentService.Application.Mediator.Commands
{
    public class UpdateEquipmentCommand : IRequest<ResponseModel<EquipmentModel>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int StockQuantity { get; set; }
        public string Status { get; set; }
    }
}
