
namespace MainteXpert.TPMMetricsService.Application.Mediator.Handler
{
    public class UpdateTPMMetricsHandler : IRequestHandler<UpdateTPMMetricsCommand, ResponseModel<TPMMetricsModel>>
    {
        private readonly IMongoRepository<TPMMetricsCollection> _collection;
        private readonly IMapper _mapper;

        public UpdateTPMMetricsHandler(IMongoRepository<TPMMetricsCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<TPMMetricsModel>> Handle(UpdateTPMMetricsCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<TPMMetricsCollection>.Filter.Eq(p => p.Id, request.Id);
            var update = Builders<TPMMetricsCollection>.Update
                .Set(p => p.OEE, request.OEE)
                .Set(p => p.Availability, request.Availability)
                .Set(p => p.Performance, request.Performance)
                .Set(p => p.Quality, request.Quality);

            var updatedTPMMetrics = await _collection.FindOneAndUpdateAsync(filter, update);

            if (updatedTPMMetrics == null)
            {
                return new ResponseModel<TPMMetricsModel>(null, "TPM Metrics not found");
            }

            var tpmMetricsModel = _mapper.Map<TPMMetricsModel>(updatedTPMMetrics);

            return new ResponseModel<TPMMetricsModel>(tpmMetricsModel);
        }
    }
}
