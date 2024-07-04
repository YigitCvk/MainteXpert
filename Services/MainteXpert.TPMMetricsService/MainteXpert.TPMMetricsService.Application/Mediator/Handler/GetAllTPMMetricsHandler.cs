namespace MainteXpert.TPMMetricsService.Application.Mediator.Handler
{
    public class GetAllTPMMetricsHandler : IRequestHandler<GetAllTPMMetricsQuery, ResponseModel<TPMMetricsModel>>
    {
        private readonly IMongoRepository<TPMMetricsCollection> _collection;
        private readonly IMapper _mapper;

        public GetAllTPMMetricsHandler(IMongoRepository<TPMMetricsCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<TPMMetricsModel>> Handle(GetAllTPMMetricsQuery request, CancellationToken cancellationToken)
        {
            // tüm görevler
            var filter = Builders<TPMMetricsCollection>.Filter.Empty;

            // yalnızca belirli alanları seçme
            var projection = Builders<TPMMetricsCollection>.Projection
                .Include(task => task.Id)
                .Include(task => task.Status);

            // MongoDB sorgusu
            var tpmItems = await _collection.GetCollection()
                .Find(filter)
                .Project<TPMMetricsCollection>(projection)
                .ToListAsync(cancellationToken);

            // Mapping işlemi
            var tpmMetricsModel = _mapper.Map<List<TPMMetricsModel>>(tpmItems);

            // ResponseModel oluşturulması
            var response = new ResponseModel<TPMMetricsModel>(tpmMetricsModel);

            return response;
        }
    }
}
