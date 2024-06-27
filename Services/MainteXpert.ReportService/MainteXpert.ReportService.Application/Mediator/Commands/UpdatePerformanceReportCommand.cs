namespace MainteXpert.ReportService.Application.Mediator.Commands
{
    public class UpdatePerformanceReportCommand : IRequest<ResponseModel<PerformanceReportModel>>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Metrics { get; set; }
    }
}
