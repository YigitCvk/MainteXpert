namespace MainteXpert.UserService.Application.Mediator.Handlers.User
{
    public class UpdateUserIsActiveCommandHandler : IRequestHandler<UpdateUserIsActiveCommand, ResponseModel<UserModel>>
    {
        private readonly IMongoRepository<UserCollection> _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserIsActiveCommandHandler(IMongoRepository<UserCollection> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<UserModel>> Handle(UpdateUserIsActiveCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var filter = Builders<UserCollection>.Filter
                                    .Eq(x => x.Id, request.UserId);

                var updateDefinition = Builders<UserCollection>.Update
                                    .Set(x => x.IsActive, request.IsActive);

                var dbResult = await _userRepository.UpdateDocumentWithSelectedFieldsAsync(filter, updateDefinition);
                var result = _mapper.Map<UserModel>(dbResult);
                return ResponseModel<UserModel>.Success(result);

            }
            catch (Exception ex)
            {

                return ResponseModel<UserModel>.Fail(data: null, message: ex.Message);
            }
        }
    }
}
