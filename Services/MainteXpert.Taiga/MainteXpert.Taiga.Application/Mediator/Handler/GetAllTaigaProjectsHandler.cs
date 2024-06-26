namespace MainteXpert.Taiga.Application.Mediator.Handler
{
    public class GetAllTaigaProjectsHandler : IRequestHandler<GetAllTaigaProjectsQuery, ResponseModel<List<TaigaProjectModel>>>
    {
        private readonly IMongoRepository<TaigaProjectCollection> _repository;
        private readonly IMapper _mapper;

        public GetAllTaigaProjectsHandler(IMongoRepository<TaigaProjectCollection> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<TaigaProjectModel>>> Handle(GetAllTaigaProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetAll();
            var projectModels = _mapper.Map<List<TaigaProjectModel>>(projects);

            return new ResponseModel<List<TaigaProjectModel>>(projectModels);
        }
    }
}
