
namespace MainteXpert.ErrorCardService.Application.Mediator.Commands.ErrorCardCall
{
    public class InsertErrorCardCallCommand : IRequest<ResponseModel>
    {
        public string StationCardId { get; set; }
    }
}
