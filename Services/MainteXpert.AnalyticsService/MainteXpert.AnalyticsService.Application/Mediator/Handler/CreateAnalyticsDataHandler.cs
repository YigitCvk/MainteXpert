namespace MainteXpert.AnalyticsService.Application.Mediator.Handler
{
    public class CreateAnalyticsDataHandler : IRequestHandler<CreateAnalyticsDataCommand, ResponseModel<AnalyticsDataModel>>
    {
        private readonly IMongoRepository<AnalyticsDataCollection> _repository;
        private readonly IMapper _mapper;

        public CreateAnalyticsDataHandler(IMongoRepository<AnalyticsDataCollection> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<AnalyticsDataModel>> Handle(CreateAnalyticsDataCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<AnalyticsDataCollection>(request);
            await _repository.InsertOneAsync(data);
            var result = _mapper.Map<AnalyticsDataModel>(data);
            return new ResponseModel<AnalyticsDataModel>(result);
        }
    }
}
