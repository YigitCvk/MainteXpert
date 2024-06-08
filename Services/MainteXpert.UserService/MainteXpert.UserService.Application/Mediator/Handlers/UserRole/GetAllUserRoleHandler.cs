namespace MainteXpert.UserService.Application.Mediator.Handlers.UserRole
{

    public class GetAllUserRoleHandler : IRequestHandler<GetAllUserRoleQuery, ResponseModel<List<UserRoleModel>>>
    {
        private readonly IMongoRepository<UserRoleCollection> _collection;
        private readonly IMapper _mapper;
        public GetAllUserRoleHandler(IMongoRepository<UserRoleCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }
        public async Task<ResponseModel<List<UserRoleModel>>> Handle(GetAllUserRoleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _collection.GetListAsync();
                var response = _mapper.Map<List<UserRoleModel>>(result);
                response = response.OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate).ToList();
                return ResponseModel<List<UserRoleModel>>.Success(response);
            }
            catch (Exception ex)
            {
                return ResponseModel<List<UserRoleModel>>.Fail(data: null, message: ex.Message);
            }
        }
    }

}
