
namespace MainteXpert.ReportService.Application.Mediator.Handler
{
    public class GetAllPerformanceReportsHandler : IRequestHandler<GetAllPerformanceReportsQuery, ResponseModel<List<PerformanceReportModel>>>
    {
        private readonly IMongoRepository<PerformanceReportCollection> _collection;
        private readonly IMapper _mapper;

        public GetAllPerformanceReportsHandler(IMongoRepository<PerformanceReportCollection> repository, IMapper mapper)
        {
            _collection = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<PerformanceReportModel>>> Handle(GetAllPerformanceReportsQuery request, CancellationToken cancellationToken)
        {
            var reports = await _collection.GetAll();
            var reportModels = _mapper.Map<List<PerformanceReportModel>>(reports);
            return new ResponseModel<List<PerformanceReportModel>>(reportModels);
        }
    }

}
