namespace MainteXpert.TPMMetricsService.Application.Mediator.Commands
{
    public class DeleteTPMMetricsCommand : IRequest<ResponseModel<bool>>
    {
        public string Id { get; set; }

        public DeleteTPMMetricsCommand(string id)
        {
            Id = id;
        }
    }
}
