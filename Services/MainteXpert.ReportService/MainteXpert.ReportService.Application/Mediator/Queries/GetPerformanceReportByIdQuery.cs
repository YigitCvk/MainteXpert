namespace MainteXpert.ReportService.Application.Mediator.Queries
{
    public class GetPerformanceReportByIdQuery : IRequest<ResponseModel<PerformanceReportModel>>
    {
        public string Id { get; set; }
    }
}
