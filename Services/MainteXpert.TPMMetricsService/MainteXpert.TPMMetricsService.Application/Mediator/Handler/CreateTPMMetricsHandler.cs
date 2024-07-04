

namespace MainteXpert.TPMMetricsService.Application.Mediator.Handler
{
    public class CreateTPMMetricsHandler : IRequestHandler<CreateTPMMetricsCommand, ResponseModel<TPMMetricsModel>>
    {
        private readonly IMongoRepository<TPMMetricsCollection> _collection;
        private readonly IMapper _mapper;

        public CreateTPMMetricsHandler(IMongoRepository<TPMMetricsCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<TPMMetricsModel>> Handle(CreateTPMMetricsCommand request, CancellationToken cancellationToken)
        {
            var tpmMetrics = new TPMMetricsCollection
            {
                EquipmentId = request.EquipmentId,
                Date = request.Date,
                OEE = request.OEE,
                Availability = request.Availability,
                Performance = request.Performance,
                Quality = request.Quality
            };

            await _collection.InsertOneAsync(tpmMetrics);

            var tpmMetricsModel = _mapper.Map<TPMMetricsModel>(tpmMetrics);

            return new ResponseModel<TPMMetricsModel>(tpmMetricsModel);
        }
    }
}
