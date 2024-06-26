using MongoDB.Driver;

namespace MainteXpert.Taiga.Application.Mediator.Handler
{
    public class UpdateTaigaProjectHandler : IRequestHandler<UpdateTaigaProjectCommand, ResponseModel<TaigaProjectModel>>
    {
        private readonly IMongoRepository<TaigaProjectCollection> _collection;
        private readonly IMapper _mapper;

        public UpdateTaigaProjectHandler(IMongoRepository<TaigaProjectCollection> repository, IMapper mapper)
        {
            _collection = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<TaigaProjectModel>> Handle(UpdateTaigaProjectCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<TaigaProjectCollection>.Filter.Eq(p => p.Id, request.Id);
            var update = Builders<TaigaProjectCollection>.Update
                .Set(p => p.Name, request.Name)
                .Set(p => p.Description, request.Description);

            var project = await _collection.FindOneAndUpdateAsync(filter, update);
            if (project == null)
            {
                return new ResponseModel<TaigaProjectModel>(null, "Project not found");
            }

            var projectModel = _mapper.Map<TaigaProjectModel>(project);
            return new ResponseModel<TaigaProjectModel>(projectModel);
        }
    }
}