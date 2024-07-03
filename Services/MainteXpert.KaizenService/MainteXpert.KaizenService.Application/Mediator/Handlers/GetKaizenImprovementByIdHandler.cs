namespace MainteXpert.KaizenService.Application.Mediator.Handler
{
    public class GetKaizenImprovementByIdHandler : IRequestHandler<GetKaizenImprovementByIdQuery, ResponseModel<KaizenImprovementModel>>
    {
        private readonly IMongoRepository<KaizenImprovementCollection> _collection;
        private readonly IMapper _mapper;

        public GetKaizenImprovementByIdHandler(IMongoRepository<KaizenImprovementCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<KaizenImprovementModel>> Handle(GetKaizenImprovementByIdQuery request, CancellationToken cancellationToken)
        {
            var kaizenImprovement = await _collection.FindByIdAsync(request.Id);

            if (kaizenImprovement == null)
            {
                return new ResponseModel<KaizenImprovementModel>(null, "Kaizen Improvement not found");
            }

            var kaizenImprovementModel = _mapper.Map<KaizenImprovementModel>(kaizenImprovement);

            return new ResponseModel<KaizenImprovementModel>(kaizenImprovementModel);
        }
    }
}
