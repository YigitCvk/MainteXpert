
namespace MainteXpert.AnalyticsService.Application.Mediator.Commands
{
    public class CreateAnalyticsDataCommand : IRequest<ResponseModel<AnalyticsDataModel>>
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string Category { get; set; }
    }
}
