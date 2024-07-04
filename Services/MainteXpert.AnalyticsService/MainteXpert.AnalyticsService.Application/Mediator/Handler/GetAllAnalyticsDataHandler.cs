namespace MainteXpert.AnalyticsService.Application.Mediator.Handler
{
    public class GetAllAnalyticsDataHandler : IRequestHandler<GetAllAnalyticsDataQuery, ResponseModel<AnalyticsDataModel>>
    {
        private readonly IMongoRepository<AnalyticsDataCollection> _repository;
        private readonly IMapper _mapper;

        public GetAllAnalyticsDataHandler(IMongoRepository<AnalyticsDataCollection> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<AnalyticsDataModel>> Handle(GetAllAnalyticsDataQuery request, CancellationToken cancellationToken)
        {
            // tüm görevler
            var filter = Builders<AnalyticsDataCollection>.Filter.Empty;

            // yalnızca belirli alanları seçme
            var projection = Builders<AnalyticsDataCollection>.Projection
                .Include(task => task.Id)
                .Include(task => task.Status);

            // MongoDB sorgusu
            var analyticsItems = await _repository.GetCollection()
                .Find(filter)
                .Project<AnalyticsDataCollection>(projection)
                .ToListAsync(cancellationToken);

            // Mapping işlemi
            var analyticsDatas = _mapper.Map<List<AnalyticsDataModel>>(analyticsItems);

            // ResponseModel oluşturulması
            var response = new ResponseModel<AnalyticsDataModel>(analyticsDatas);

            return response;
        }
    }
}
