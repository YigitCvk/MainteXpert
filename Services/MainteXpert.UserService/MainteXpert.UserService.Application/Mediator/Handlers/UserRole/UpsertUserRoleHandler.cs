namespace MainteXpert.UserService.Application.Mediator.Handlers.UserRole
{

    public class UpsertUserRoleHandler : IRequestHandler<UpsertUserRoleCommand, ResponseModel>
    {

        private readonly IMongoRepository<UserRoleCollection> _collection;
        private readonly IMapper _mapper;

        public UpsertUserRoleHandler(IMediator mediator, IMongoRepository<UserRoleCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel> Handle(UpsertUserRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.Id))
                {
                    var document = await _collection.FindByIdAsync(request.Id);
                    document.RoleName = request.RoleName;
                    await _collection.ReplaceOneAsync(document);
                }
                else
                {
                    var role = _mapper.Map<UserRoleCollection>(request);
                    await _collection.InsertOneAsync(role);
                }

                return ResponseModel.Success();
            }
            catch (Exception)
            {

                return ResponseModel.Fail();
            }


        }
    }

}
