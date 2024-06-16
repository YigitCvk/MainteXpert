namespace MainteXpert.ErrorCardService.Application.Mediator.Handlers.ErrorCard
{
    public class GetAllErrorCardsByStationCardGroupIdHandler : IRequestHandler<GetAllErrorCardsByStationCardGroupIdQuery, ResponseModel<List<GetAllErrorCardsByErrorCardGroupModel>>>
    {
        private readonly IMongoRepository<ErrorCardCollection> _errorCardCollection;
        private readonly IMongoRepository<ErrorCardGroupCollection> _errorCardGroupCollection;
        private readonly IMapper _mapper;

        public GetAllErrorCardsByStationCardGroupIdHandler(IMongoRepository<ErrorCardCollection> errorCardCollection, IMongoRepository<ErrorCardGroupCollection> errorCardGroupCollection, IMapper mapper)
        {
            _errorCardCollection = errorCardCollection;
            _errorCardGroupCollection = errorCardGroupCollection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<GetAllErrorCardsByErrorCardGroupModel>>> Handle(GetAllErrorCardsByStationCardGroupIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var builder = Builders<ErrorCardCollection>.Filter;
                var filter = builder.Empty;

                if (!string.IsNullOrEmpty(request.StationCardGroupId))
                {

                    var stationCodeFilter = Builders<ErrorCardCollection>.Filter
                      .Eq(x => x.StationCardGroup.Id, request.StationCardGroupId);

                    filter &= stationCodeFilter;

                }

                var del = builder.Ne(x => x.Status, DocumentStatus.Deleted);
                filter &= del;


                ProjectionDefinition<ErrorCardCollection> projection = Builders<ErrorCardCollection>.Projection
                    .Include(x => x.Id)
                    .Include(x => x.ErrorCardGroup);


                FindOptions<ErrorCardCollection> findOptions = new FindOptions<ErrorCardCollection>
                {
                    Projection = projection
                };

                var result = _errorCardCollection.FindAllWithProjection(filter, findOptions);
                var response = _mapper.Map<List<ErrorCardResponseModel>>(result);

                var errorCards = response.DistinctBy(x => x.ErrorCardGroup.Id).ToList();
                var responseModel = new List<GetAllErrorCardsByErrorCardGroupModel>();


                foreach (var errorCardGroup in errorCards)
                {
                    var model = new GetAllErrorCardsByErrorCardGroupModel();
                    model.Name = errorCardGroup.ErrorCardGroup.ErrorCardGroupName;
                    model.Id = errorCardGroup.ErrorCardGroup.Id;
                    model.Count = response.Count(x => x.ErrorCardGroup.Id == errorCardGroup.ErrorCardGroup.Id);
                    responseModel.Add(model);
                }
                return ResponseModel<List<GetAllErrorCardsByErrorCardGroupModel>>.Success(data: responseModel);

            }
            catch (Exception ex)
            {

                return ResponseModel<List<GetAllErrorCardsByErrorCardGroupModel>>.Fail(data: null, message: ex.Message);
            }

        }
    }
}
