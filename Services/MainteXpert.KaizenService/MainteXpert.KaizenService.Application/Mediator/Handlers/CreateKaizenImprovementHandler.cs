namespace MainteXpert.KaizenService.Application.Mediator.Handlers
{
    public class CreateKaizenImprovementHandler : IRequestHandler<CreateKaizenImprovementCommand, ResponseModel<KaizenImprovementModel>>
    {
        private readonly IMongoRepository<KaizenImprovementCollection> _collection;
        private readonly IMapper _mapper;

        public CreateKaizenImprovementHandler(IMongoRepository<KaizenImprovementCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<KaizenImprovementModel>> Handle(CreateKaizenImprovementCommand request, CancellationToken cancellationToken)
        {
            var kaizenImprovement = new KaizenImprovementCollection
            {
                Title = request.Title,
                Description = request.Description,
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.Now,
                Status = "New"
            };

            await _collection.InsertOneAsync(kaizenImprovement);

            var kaizenImprovementModel = _mapper.Map<KaizenImprovementModel>(kaizenImprovement);

            return new ResponseModel<KaizenImprovementModel>(kaizenImprovementModel);
        }
    }
}
