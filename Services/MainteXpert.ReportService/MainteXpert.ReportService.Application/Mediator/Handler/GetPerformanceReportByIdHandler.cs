namespace MainteXpert.ReportService.Application.Mediator.Handler
{
    public class GetPerformanceReportByIdHandler : IRequestHandler<GetPerformanceReportByIdQuery, ResponseModel<PerformanceReportModel>>
    {
        private readonly IMongoRepository<PerformanceReportCollection> _collection;
        private readonly IMapper _mapper;

        public GetPerformanceReportByIdHandler(IMongoRepository<PerformanceReportCollection> repository, IMapper mapper)
        {
            _collection = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<PerformanceReportModel>> Handle(GetPerformanceReportByIdQuery request, CancellationToken cancellationToken)
        {
            var report = await _collection.FindByIdAsync(request.Id);
            if (report == null)
            {
                return new ResponseModel<PerformanceReportModel>(null, "Report not found");
            }

            var reportModel = _mapper.Map<PerformanceReportModel>(report);
            return new ResponseModel<PerformanceReportModel>(reportModel);
        }
    }
}
