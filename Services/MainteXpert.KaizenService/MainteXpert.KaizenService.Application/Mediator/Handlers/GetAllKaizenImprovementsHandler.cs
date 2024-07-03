namespace MainteXpert.KaizenService.Application.Mediator.Handler
{
    public class GetAllKaizenImprovementsHandler : IRequestHandler<GetAllKaizenImprovementsQuery, ResponseModel<List<KaizenImprovementModel>>>
    {
        private readonly IMongoRepository<KaizenImprovementCollection> _collection;
        private readonly IMapper _mapper;

        public GetAllKaizenImprovementsHandler(IMongoRepository<KaizenImprovementCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<KaizenImprovementModel>>> Handle(GetAllKaizenImprovementsQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<KaizenImprovementCollection>.Filter.Empty;
            var projection = Builders<KaizenImprovementCollection>.Projection
                .Include(p => p.Id)
                .Include(p => p.Title)
                .Include(p => p.Description)
                .Include(p => p.Status)
                .Include(p => p.CreatedBy)
                .Include(p => p.CreatedDate)
                .Include(p => p.AssignedTo)
                .Include(p => p.CompletedDate);

            var kaizenImprovements = await _collection.FindWithProjection(filter, projection);
            var kaizenImprovementModels = _mapper.Map<List<KaizenImprovementModel>>(kaizenImprovements);

            return new ResponseModel<List<KaizenImprovementModel>>(kaizenImprovementModels);
        }
    }
}
