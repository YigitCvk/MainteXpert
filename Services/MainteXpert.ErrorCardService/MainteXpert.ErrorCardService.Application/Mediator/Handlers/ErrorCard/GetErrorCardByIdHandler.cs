
namespace MainteXpert.ErrorCardService.Application.Mediator.Handlers.ErrorCard
{
    public class GetErrorCardByIdHandler : IRequestHandler<GetErrorCardByIdQuery, ResponseModel<ErrorCardResponseModel>>
    {
        private readonly IMongoRepository<ErrorCardCollection> _collection;
        private readonly IMapper _mapper;

        public GetErrorCardByIdHandler(IMongoRepository<ErrorCardCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ErrorCardResponseModel>> Handle(GetErrorCardByIdQuery command, CancellationToken cancellationToken)
        {
            var builder = Builders<ErrorCardCollection>.Filter;
            var projectionBuilder = Builders<ErrorCardCollection>.Projection;
            var filter = builder.Empty;
            ProjectionDefinition<ErrorCardCollection> projection;
            try
            {
                var act = builder.Eq(x => x.Id, command.Id);
                filter &= act;

                var del = builder.Ne(x => x.Status, DocumentStatus.Deleted);
                filter &= del;

                if (command.WithTechnicianData)
                    projection = projectionBuilder
                    .Exclude(x => x.ErrorCardHistoryModels);
                else
                    projection = projectionBuilder
                   .Exclude(x => x.ErrorCardHistoryModels)
                   .Exclude(x => x.TechnicianPhotos)
                   .Exclude(x => x.TechnicianDocuments)
                   .Exclude(x => x.TechnicianDescription);

                FindOptions<ErrorCardCollection> findOptions = new FindOptions<ErrorCardCollection>
                {
                    Projection = projection
                };

                var result = await _collection.FindWithProjection(filter, findOptions);
                var response = _mapper.Map<ErrorCardResponseModel>(result);
                if (response is not null && response.CurrentTechnician is not null)
                    response.IsAuthUserWorkingOn = _collection.GetUserId().Equals(response.CurrentTechnician.Id);

                return ResponseModel<ErrorCardResponseModel>.Success(response);
            }
            catch (Exception ex)
            {
                return ResponseModel<ErrorCardResponseModel>.Fail(data: null, message: ex.Message);

            }
        }
    }
}
