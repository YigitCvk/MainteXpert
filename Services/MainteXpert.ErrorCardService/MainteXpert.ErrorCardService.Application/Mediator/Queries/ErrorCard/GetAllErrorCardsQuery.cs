namespace MainteXpert.ErrorCardService.Application.Mediator.Queries.ErrorCard
{
    public class GetAllErrorCardsQuery : IRequest<ResponseModel<PaginationDocument<ErrorCardResponseModel>>>
    {
        public string? StationCardId { get; set; }
        public string? ErrorCardGroupId { get; set; }
        public string? CreatorOperatorId { get; set; }
        public int? ErrorCardStatus { get; set; }
        public int? DocumentStatus { get; set; }
        public int? Priority { get; set; }
        public bool WithPhoto { get; set; }
        public int NumberOfPhotoData { get; set; } = 1;
        public bool WithTechnicianData { get; set; }
        public PaginationQuery Pagination { get; set; } = new PaginationQuery();
        public ImageCompressModel PhotoCompress { get; set; } = new ImageCompressModel();
    }
}
