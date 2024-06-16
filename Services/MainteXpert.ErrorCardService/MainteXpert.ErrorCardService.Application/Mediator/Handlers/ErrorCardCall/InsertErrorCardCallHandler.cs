namespace MainteXpert.ErrorCardService.Application.Mediator.Handlers.ErrorCardCall
{
    public class InsertErrorCardCallHandler : IRequestHandler<InsertErrorCardCallCommand, ResponseModel>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<StationCardGroupCollection> _stationCardGroupCollection;
        private readonly IMongoRepository<UserCollection> _userCollection;
        private readonly IMongoRepository<ErrorCardCallCollection> _errorCardCollection;

        public InsertErrorCardCallHandler(
            IMongoRepository<StationCardGroupCollection> stationCardGroupCollection,
            IMongoRepository<UserCollection> userCollection,
            IMongoRepository<ErrorCardCallCollection> errorCardCollection,
            IMapper mapper)
        {
            _mapper = mapper;
            _stationCardGroupCollection = stationCardGroupCollection;
            _userCollection = userCollection;
            _errorCardCollection = errorCardCollection;
        }

        public async Task<ResponseModel> Handle(InsertErrorCardCallCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var errorCardCall = _mapper.Map<ErrorCardCallCollection>(request);
                var stationCard = await _stationCardGroupCollection.FindByIdAsync(request.StationCardId);
                var stationCardModel = _mapper.Map<StationCardGroupModel>(stationCard);
                var userId = _userCollection.GetUserId();

                var userFilterDefinition = Builders<UserCollection>.Filter;
                var userFilter = userFilterDefinition.Eq(x => x.Id, userId);
                var userProjectDefinition = Builders<UserCollection>.Projection;
                var userProjection = userProjectDefinition
                    .Include(x => x.Id)
                    .Include(x => x.Name)
                    .Include(x => x.Surname)
                    .Include(x => x.RegisterNumber)
                    .Include(x => x.RoleId);

                var userFindOption = new FindOptions<UserCollection>
                {
                    Projection = userProjection
                };

                var user = await _userCollection.FindWithProjection(userFilter, userFindOption);
                var userModel = _mapper.Map<UserModel>(user);

                errorCardCall.CallerOperator = userModel;
                errorCardCall.ErrorCardCallStatus = ErrorCardCallStatusEnum.Opened;
                errorCardCall.Station = stationCardModel;

                var response = await _errorCardCollection.InsertOneAsync(errorCardCall);

                return ResponseModel.Success();
            }
            catch (Exception ex)
            {
                return ResponseModel.Fail();
            }
        }
    }
}
