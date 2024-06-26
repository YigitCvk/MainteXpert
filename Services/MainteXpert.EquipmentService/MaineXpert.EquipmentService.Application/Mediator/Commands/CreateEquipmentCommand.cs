
namespace MainteXpert.EquipmentService.Application.Mediator.Commands
{
    public class CreateEquipmentCommand : IRequest<ResponseModel<EquipmentModel>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Status { get; set; }
    }
}
