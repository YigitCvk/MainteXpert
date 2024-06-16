namespace MainteXpert.ErrorCardService.Application.Mediator.Queries.ErrorCard
{
    public class GetAllErrorCardsByStationCardGroupIdQuery : IRequest<ResponseModel<List<GetAllErrorCardsByErrorCardGroupModel>>>
    {
        public string StationCardGroupId { get; set; } = string.Empty;

    }
}
