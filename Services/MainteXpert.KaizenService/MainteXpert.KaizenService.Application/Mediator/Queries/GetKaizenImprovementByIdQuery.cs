namespace MainteXpert.KaizenService.Application.Mediator.Queries
{
    public class GetKaizenImprovementByIdQuery : IRequest<ResponseModel<KaizenImprovementModel>>
    {
        public string Id { get; set; }

        public GetKaizenImprovementByIdQuery(string id)
        {
            Id = id;
        }
    }
}
