
namespace MainteXpert.UserService.Application.Mediator.Handlers.UserRole
{
    public class UpsertUserRolePermissionHandler : IRequestHandler<UpsertUserRolePermissionCommand, ResponseModel>
    {
        private readonly IMongoRepository<UserRoleCollection> _collection;
        private readonly IMapper _mapper;

        public UpsertUserRolePermissionHandler(IMediator mediator, IMongoRepository<UserRoleCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }


        public async Task<ResponseModel> Handle(UpsertUserRolePermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _collection.GetAsync(x => x.Id.Equals(request.RoleId) && x.Status != Common.Enums.DocumentStatus.Deleted);
                if (role != null)
                {
                    var permissionColls = _mapper.Map<List<UserRolePermissionModel>>(request.Permissions);
                    role.Permissions = permissionColls;
                    if (role.Permissions == null)
                    {
                        await _collection.InsertOneAsync(role);
                    }
                    else
                    {
                        await _collection.ReplaceOneAsync(role);
                    }
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
