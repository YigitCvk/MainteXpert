namespace MainteXpert.ErrorCardService.Application.Mediator.Queries.ErrorCard
{
    public class GetErrorCardByIdQuery : IRequest<ResponseModel<ErrorCardResponseModel>>
    {
        public string Id { get; set; }
        public bool WithTechnicianData { get; set; }
    }
}
