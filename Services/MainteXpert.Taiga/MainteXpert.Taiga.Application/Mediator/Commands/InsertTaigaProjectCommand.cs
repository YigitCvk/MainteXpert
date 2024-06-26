

namespace MainteXpert.Taiga.Application.Mediator.Commands
{
    public class InsertTaigaProjectCommand : IRequest<ResponseModel<TaigaProjectModel>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
