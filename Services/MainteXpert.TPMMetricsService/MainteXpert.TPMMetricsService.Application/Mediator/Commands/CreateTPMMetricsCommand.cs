
namespace MainteXpert.TPMMetricsService.Application.Mediator.Commands
{
    public class CreateTPMMetricsCommand : IRequest<ResponseModel<TPMMetricsModel>>
    {
        public string EquipmentId { get; set; }
        public DateTime Date { get; set; }
        public decimal OEE { get; set; }
        public decimal Availability { get; set; }
        public decimal Performance { get; set; }
        public decimal Quality { get; set; }
    }
}
