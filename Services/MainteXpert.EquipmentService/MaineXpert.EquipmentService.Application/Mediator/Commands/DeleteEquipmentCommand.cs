namespace MainteXpert.EquipmentService.Application.Mediator.Commands
{
    public class DeleteEquipmentCommand : IRequest<ResponseModel<bool>>
    {
        public int Id { get; set; }

        public DeleteEquipmentCommand(int id)
        {
            Id = id;
        }
    }
}
