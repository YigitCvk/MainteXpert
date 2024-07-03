namespace MainteXpert.KaizenService.Application.Mediator.Commands
{
    public class DeleteKaizenImprovementCommand : IRequest<ResponseModel<bool>>
    {
        public string Id { get; set; }

        public DeleteKaizenImprovementCommand(string id)
        {
            Id = id;
        }
    }
}
