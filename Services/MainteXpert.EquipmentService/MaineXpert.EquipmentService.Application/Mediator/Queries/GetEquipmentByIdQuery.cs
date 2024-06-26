namespace MaineXpert.EquipmentService.Application.Mediator.Queries
{
    public class GetEquipmentByIdQuery : IRequest<ResponseModel<EquipmentModel>>
    {
        public string? Id { get; set; }

        public GetEquipmentByIdQuery(string? id)
        {
            Id = id;
        }
    }
}
