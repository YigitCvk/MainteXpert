namespace MainteXpert.Taiga.Application.Mediator.Handler
{
    public class GetTaigaProjectByIdHandler : IRequestHandler<GetTaigaProjectByIdQuery, ResponseModel<TaigaProjectModel>>
    {
        private readonly IMongoRepository<TaigaProjectCollection> _repository;
        private readonly IMapper _mapper;

        public GetTaigaProjectByIdHandler(IMongoRepository<TaigaProjectCollection> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<TaigaProjectModel>> Handle(GetTaigaProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _repository.FindByIdAsync(request.Id);
            if (project == null)
            {
                return new ResponseModel<TaigaProjectModel>(null, "Project not found");
            }

            var projectModel = _mapper.Map<TaigaProjectModel>(project);
            return new ResponseModel<TaigaProjectModel>(projectModel);
        }
    }
}
