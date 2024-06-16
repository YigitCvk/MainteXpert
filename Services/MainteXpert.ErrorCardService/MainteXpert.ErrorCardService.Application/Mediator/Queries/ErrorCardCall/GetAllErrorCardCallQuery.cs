namespace MainteXpert.ErrorCardService.Application.Mediator.Queries.ErrorCardCall
{
    public class GetAllErrorCardCallQuery : IRequest<ResponseModel<PaginationDocument<ErrorCardCallResponseModel>>>
    {
        public string? StationCardId { get; set; }
        public string? TecnicianId { get; set; }
        public int? ErrorCardCallStatus { get; set; }
        public int? DocumentStatus { get; set; }
        public PaginationQuery Pagination { get; set; } = new PaginationQuery();
    }
}
