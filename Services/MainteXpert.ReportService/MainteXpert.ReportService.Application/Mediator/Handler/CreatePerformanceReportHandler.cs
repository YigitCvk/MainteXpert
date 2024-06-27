namespace MainteXpert.ReportService.Application.Mediator.Handler
{
    public class CreatePerformanceReportHandler : IRequestHandler<CreatePerformanceReportCommand, ResponseModel<PerformanceReportModel>>
    {
        private readonly IMongoRepository<PerformanceReportCollection> _repository;
        private readonly IMapper _mapper;

        public CreatePerformanceReportHandler(IMongoRepository<PerformanceReportCollection> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<PerformanceReportModel>> Handle(CreatePerformanceReportCommand request, CancellationToken cancellationToken)
        {
            var report = _mapper.Map<PerformanceReportCollection>(request);
            await _repository.InsertOneAsync(report);

            var reportModel = _mapper.Map<PerformanceReportModel>(report);
            return new ResponseModel<PerformanceReportModel>(reportModel);
        }
    }
}
