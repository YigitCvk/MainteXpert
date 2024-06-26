
namespace MainteXpert.EquipmentService.Application.Mediator.Commands
{
    public class CheckStockCommand : IRequest<ResponseModel<EquipmentStockModel>>
    {
        public string EquipmentId { get; set; }
        public int RequiredQuantity { get; set; }

        public CheckStockCommand(string equipmentId, int requiredQuantity)
        {
            EquipmentId = equipmentId;
            RequiredQuantity = requiredQuantity;
        }
    }
}
