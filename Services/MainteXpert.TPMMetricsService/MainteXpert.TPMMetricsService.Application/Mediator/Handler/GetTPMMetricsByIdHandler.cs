
namespace MainteXpert.TPMMetricsService.Application.Mediator.Handler
{
    public class GetTPMMetricsByIdHandler : IRequestHandler<GetTPMMetricsByIdQuery, ResponseModel<TPMMetricsModel>>
    {
        private readonly IMongoRepository<TPMMetricsCollection> _collection;
        private readonly IMapper _mapper;

        public GetTPMMetricsByIdHandler(IMongoRepository<TPMMetricsCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<TPMMetricsModel>> Handle(GetTPMMetricsByIdQuery request, CancellationToken cancellationToken)
        {
            var tpmMetrics = await _collection.FindByIdAsync(request.Id);

            if (tpmMetrics == null)
            {
                return new ResponseModel<TPMMetricsModel>(null, "TPM Metrics not found");
            }

            var tpmMetricsModel = _mapper.Map<TPMMetricsModel>(tpmMetrics);

            return new ResponseModel<TPMMetricsModel>(tpmMetricsModel);
        }
    }
}
