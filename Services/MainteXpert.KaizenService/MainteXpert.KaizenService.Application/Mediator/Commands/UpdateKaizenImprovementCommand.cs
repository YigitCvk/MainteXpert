namespace MainteXpert.KaizenService.Application.Mediator.Commands
{
    public class UpdateKaizenImprovementCommand : IRequest<ResponseModel<KaizenImprovementModel>>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
