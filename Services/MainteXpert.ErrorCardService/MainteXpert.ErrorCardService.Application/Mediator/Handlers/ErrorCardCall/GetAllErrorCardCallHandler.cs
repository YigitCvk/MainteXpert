

namespace MainteXpert.ErrorCardService.Application.Mediator.Handlers.ErrorCard
{
    public class GetAllErrorCardCallHandler : IRequestHandler<GetAllErrorCardCallQuery, ResponseModel<PaginationDocument<ErrorCardCallResponseModel>>>
    {
        private readonly IMongoRepository<ErrorCardCallCollection> _collection;
        private readonly IMapper _mapper;

        public GetAllErrorCardCallHandler(IMongoRepository<ErrorCardCallCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<PaginationDocument<ErrorCardCallResponseModel>>> Handle(GetAllErrorCardCallQuery request, CancellationToken cancellationToken)
        {
            var builder = Builders<ErrorCardCallCollection>.Filter;
            var filter = builder.Empty;

            try
            {
                if (!string.IsNullOrEmpty(request.StationCardId))
                {

                    var stationCodeFilter = Builders<ErrorCardCallCollection>.Filter
                        .Eq(t => t.Station.Id, request.StationCardId);

                    filter &= stationCodeFilter;
                }

                if (!string.IsNullOrEmpty(request.TecnicianId))
                {
                    var eco = builder.Eq(x => x.InterferingOperator.Id, request.TecnicianId);
                    filter &= eco;
                }

                if (request.DocumentStatus.HasValue)
                {
                    var dcs = builder.Eq(x => x.Status, (DocumentStatus)request.DocumentStatus.Value);
                    filter &= dcs;
                }

                if (request.ErrorCardCallStatus.HasValue)
                {
                    var ecs = builder.Eq(x => x.ErrorCardCallStatus, (ErrorCardCallStatusEnum)request.ErrorCardCallStatus.Value);
                    filter &= ecs;
                }


                FindOptions<ErrorCardCallCollection> findOptions = new FindOptions<ErrorCardCallCollection>
                {

                };

                SortDefinition<ErrorCardCallCollection> sortDefinition = Builders<ErrorCardCallCollection>
                    .Sort.Descending(x => x.CreatedDate).Ascending(x => x.ErrorCardCallStatus);

                var result = _collection.FindAllWithProjection(filter, findOptions, request.Pagination, sortDefinition);
                var response = _mapper.Map<PaginationDocument<ErrorCardCallResponseModel>>(result);

                return ResponseModel<PaginationDocument<ErrorCardCallResponseModel>>.Success(response);
            }
            catch (Exception ex)
            {
                return ResponseModel<PaginationDocument<ErrorCardCallResponseModel>>.Fail(data: null, message: ex.Message);

            }

        }
    }
}
