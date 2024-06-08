
namespace MainteXpert.UserService.Application.Mediator.Handlers.UserRole
{
    public class GetUserRoleByIdHandler : IRequestHandler<GetUserRoleByIdQuery, ResponseModel<UserRoleModel>>
    {
        private readonly IMongoRepository<UserRoleCollection> _collection;
        private readonly IMapper _mapper;

        public GetUserRoleByIdHandler(IMongoRepository<UserRoleCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<UserRoleModel>> Handle(GetUserRoleByIdQuery command, CancellationToken cancellationToken)
        {
            try
            {
                var document = await _collection.FindOneAsync(x => x.Id == command.Id);
                var model = _mapper.Map<UserRoleModel>(document);
                return ResponseModel<UserRoleModel>.Success(model);

            }
            catch (Exception ex)
            {

                return ResponseModel<UserRoleModel>.Fail(data: null, message: ex.Message);

            }
        }
    }
}
