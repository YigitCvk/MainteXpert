namespace MainteXpert.UserService.Application.Mediator.Handlers.User
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, ResponseModel<List<UserModel>>>
    {

        private readonly IMongoRepository<UserCollection> _collection;
        private readonly IMongoRepository<UserRoleCollection> _roleCollection;
        private readonly IMapper _mapper;
        public GetAllUserHandler(
            IMongoRepository<UserCollection> collection,
            IMongoRepository<UserRoleCollection> roleCollection,
            IMapper mapper)
        {
            _collection = collection;
            _roleCollection = roleCollection;
            _mapper = mapper;
        }


        public async Task<ResponseModel<List<UserModel>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            //TODO Pagination
            try
            {
                var userModelList = new List<UserModel>();
                var dbResult = await _collection.GetListAsync();

                foreach (var result in dbResult)
                {
                    var userModel = _mapper.Map<UserModel>(result);
                    var userRoleDocument = await _roleCollection.FindByIdAsync(result.RoleId);
                    var userRoleModel = _mapper.Map<UserRoleModel>(userRoleDocument);
                    userModel.Role = userRoleModel;
                    userModelList.Add(userModel);
                }
                userModelList = userModelList.OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate).ToList();
                return ResponseModel<List<UserModel>>.Success(userModelList);
            }
            catch (Exception ex)
            {

                return ResponseModel<List<UserModel>>.Fail(data: null, message: ex.Message);
            }

        }
    }
}
