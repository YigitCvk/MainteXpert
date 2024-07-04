namespace MainteXpert.TPMMetricsService.Application.Mediator.Queries
{
    public class GetTPMMetricsByIdQuery : IRequest<ResponseModel<TPMMetricsModel>>
    {
        public string Id { get; set; }

        public GetTPMMetricsByIdQuery(string id)
        {
            Id = id;
        }
    }
}
