namespace MainteXpert.ReportService.Application.Mediator.Handler
{
    public class UpdatePerformanceReportHandler : IRequestHandler<UpdatePerformanceReportCommand, ResponseModel<PerformanceReportModel>>
    {
        private readonly IMongoRepository<PerformanceReportCollection> _repository;
        private readonly IMapper _mapper;

        public UpdatePerformanceReportHandler(IMongoRepository<PerformanceReportCollection> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<PerformanceReportModel>> Handle(UpdatePerformanceReportCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<PerformanceReportCollection>.Filter.Eq(p => p.Id, request.Id);
            var update = Builders<PerformanceReportCollection>.Update
                .Set(p => p.Title, request.Title)
                .Set(p => p.Description, request.Description)
                .Set(p => p.Metrics, request.Metrics);

            var updatedReport = await _repository.UpdateDocumentWithSelectedFieldsAsync(filter, update);
            if (updatedReport == null)
            {
                return new ResponseModel<PerformanceReportModel>(null, "Report not found");
            }

            var reportModel = _mapper.Map<PerformanceReportModel>(updatedReport);
            return new ResponseModel<PerformanceReportModel>(reportModel);
        }
    }
}
