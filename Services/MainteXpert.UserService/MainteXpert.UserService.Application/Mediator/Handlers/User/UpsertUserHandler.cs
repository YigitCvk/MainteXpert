namespace MainteXpert.UserService.Application.Mediator.Handlers.User
{
    public class UpsertUserHandler : IRequestHandler<UpsertUserCommand, ResponseModel<UserModel>>
    {
        private readonly IMongoRepository<UserCollection> _collection;
        private readonly IMongoRepository<UserRoleCollection> _roleCollection;
        private readonly IMapper _mapper;

        public UpsertUserHandler(IMongoRepository<UserCollection> collection,
            IMongoRepository<UserRoleCollection> roleCollection,
            IMapper mapper)
        {
            _collection = collection;
            _roleCollection = roleCollection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<UserModel>> Handle(UpsertUserCommand request, CancellationToken cancellationToken)
        {
            UserModel responseModel = null;

            try
            {
                if (!string.IsNullOrEmpty(request.Id))
                {
                    var document = await _collection.FindOneAsync(x => x.Id.Equals(request.Id));
                    document.Surname = request.Surname;
                    document.Name = request.Name;
                    document.Photo = request.Photo;
                    document.CitizenNumber = request.CitizenNumber;
                    document.RegisterNumber = request.RegisterNumber;
                    document.Title = request.Title;
                    document.RoleId = request.RoleId;
                    document.Password = request.Password;
                    document.Email = request.Email;
                    document.IsActive = request.IsActive;
                    var dbResponse = await _collection.ReplaceOneAsync(document);
                    responseModel = _mapper.Map<UserModel>(dbResponse);
                }
                else
                {
                    var user = _mapper.Map<UserCollection>(request);
                    var dbResponse = await _collection.InsertOneAsync(user);
                    responseModel = _mapper.Map<UserModel>(dbResponse);
                }
                var roleDocument = await _roleCollection.FindByIdAsync(request.RoleId);
                var mappedRole = _mapper.Map<UserRoleModel>(roleDocument);
                responseModel.Role = mappedRole;
                return ResponseModel<UserModel>.Success(data: responseModel);
            }
            catch (Exception e)
            {
                return ResponseModel<UserModel>.Fail(data: responseModel);

            }



        }
    }
}
