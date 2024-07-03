namespace MainteXpert.KaizenService.Application.Mediator.Commands
{
    public class CreateKaizenImprovementCommand : IRequest<ResponseModel<KaizenImprovementModel>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
    }
}
