namespace MaineXpert.EquipmentService.Application.Mediator.Commands
{
    public class DeleteEquipmentCommand : IRequest<ResponseModel<bool>>
    {
        public string Id { get; set; }
    }
}
