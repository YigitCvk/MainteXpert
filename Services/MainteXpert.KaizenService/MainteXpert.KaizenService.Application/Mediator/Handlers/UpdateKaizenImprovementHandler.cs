namespace MainteXpert.KaizenService.Application.Mediator.Handlers
{
    public class UpdateKaizenImprovementHandler : IRequestHandler<UpdateKaizenImprovementCommand, ResponseModel<KaizenImprovementModel>>
    {
        private readonly IMongoRepository<KaizenImprovementCollection> _collection;
        private readonly IMapper _mapper;

        public UpdateKaizenImprovementHandler(IMongoRepository<KaizenImprovementCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<KaizenImprovementModel>> Handle(UpdateKaizenImprovementCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<KaizenImprovementCollection>.Filter.Eq(p => p.Id, request.Id);
            var update = Builders<KaizenImprovementCollection>.Update
                .Set(p => p.Title, request.Title)
                .Set(p => p.Description, request.Description)
                .Set(p => p.Status, request.Status)
                .Set(p => p.AssignedTo, request.AssignedTo)
                .Set(p => p.CompletedDate, request.CompletedDate);

            var updatedKaizenImprovement = await _collection.FindOneAndUpdateAsync(filter, update);

            if (updatedKaizenImprovement == null)
            {
                return new ResponseModel<KaizenImprovementModel>(null, "Kaizen Improvement not found");
            }

            var kaizenImprovementModel = _mapper.Map<KaizenImprovementModel>(updatedKaizenImprovement);

            return new ResponseModel<KaizenImprovementModel>(kaizenImprovementModel);
        }
    }
}
