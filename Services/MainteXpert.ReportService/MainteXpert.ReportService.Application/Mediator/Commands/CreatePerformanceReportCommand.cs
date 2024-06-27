namespace MainteXpert.ReportService.Application.Mediator.Commands
{
    public class CreatePerformanceReportCommand : IRequest<ResponseModel<PerformanceReportModel>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Metrics { get; set; }
    }
}
