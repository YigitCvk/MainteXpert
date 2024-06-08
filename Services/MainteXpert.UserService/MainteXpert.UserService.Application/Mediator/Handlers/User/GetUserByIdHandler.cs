namespace MainteXpert.UserService.Application.Mediator.Handlers.User
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ResponseModel<UserModel>>
    {
        private readonly IMongoRepository<UserCollection> _collection;
        private readonly IMongoRepository<UserRoleCollection> _roleCollection;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(
            IMongoRepository<UserCollection> collection,
            IMongoRepository<UserRoleCollection> roleCollection,
            IMapper mapper)
        {
            _collection = collection;
            _roleCollection = roleCollection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<UserModel>> Handle(GetUserByIdQuery command, CancellationToken cancellationToken)
        {
            try
            {
                var document = await _collection.FindOneAsync(x => x.Id == command.Id);
                var userModel = _mapper.Map<UserModel>(document);
                var userRoleDocument = await _roleCollection.FindByIdAsync(document.RoleId);
                var userRoleModel = _mapper.Map<UserRoleModel>(userRoleDocument);
                userModel.Role = userRoleModel;
                return ResponseModel<UserModel>.Success(userModel);
            }
            catch (Exception ex)
            {
                return ResponseModel<UserModel>.Fail(data: null, message: ex.Message);
            }
        }
    }
}
