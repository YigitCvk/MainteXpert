namespace MainteXpert.Taiga.Application.Mediator.Queries
{
    public class GetTaigaProjectByIdQuery : IRequest<ResponseModel<TaigaProjectModel>>
    {
        public string Id { get; set; }
    }

}
