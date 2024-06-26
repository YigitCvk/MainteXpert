namespace MainteXpert.Taiga.Application.Mediator.Handler
{
    public class InsertTaigaProjectHandler : IRequestHandler<InsertTaigaProjectCommand, ResponseModel<TaigaProjectModel>>
    {
        private readonly IMongoRepository<TaigaProjectCollection> _repository;
        private readonly IMapper _mapper;

        public InsertTaigaProjectHandler(IMongoRepository<TaigaProjectCollection> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<TaigaProjectModel>> Handle(InsertTaigaProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new TaigaProjectCollection
            {
                Name = request.Name,
                Description = request.Description
            };

            await _repository.InsertOneAsync(project);

            var projectModel = _mapper.Map<TaigaProjectModel>(project);
            return new ResponseModel<TaigaProjectModel>(projectModel);
        }
    }
}
