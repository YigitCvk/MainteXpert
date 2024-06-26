namespace MainteXpert.Taiga.Application.Mediator.Commands
{
    public class UpdateTaigaProjectCommand : IRequest<ResponseModel<TaigaProjectModel>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
