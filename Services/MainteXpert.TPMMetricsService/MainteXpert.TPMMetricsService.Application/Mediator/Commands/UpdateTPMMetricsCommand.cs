namespace MainteXpert.TPMMetricsService.Application.Mediator.Commands
{
    public class UpdateTPMMetricsCommand : IRequest<ResponseModel<TPMMetricsModel>>
    {
        public string Id { get; set; }
        public decimal OEE { get; set; }
        public decimal Availability { get; set; }
        public decimal Performance { get; set; }
        public decimal Quality { get; set; }
    }
}
