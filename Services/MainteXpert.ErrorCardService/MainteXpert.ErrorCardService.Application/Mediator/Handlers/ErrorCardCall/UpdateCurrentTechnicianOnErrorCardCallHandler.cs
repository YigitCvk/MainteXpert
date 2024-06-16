namespace MainteXpert.ErrorCardService.Application.Mediator.Handlers.ErrorCardCall
{
    public class UpdateCurrentTechnicianOnErrorCardCallHandler : IRequestHandler<UpdateCurrentTechnicianOnErrorCardCallCommand, ResponseModel>
    {
        private readonly IMongoRepository<ErrorCardCallCollection> _errorCardCallCollection;
        private readonly IMongoRepository<UserCollection> _userCollection;
        private readonly IMongoRepository<UserRoleCollection> _userRoleCollection;
        private readonly IMapper _mapper;

        public UpdateCurrentTechnicianOnErrorCardCallHandler(
                                IMongoRepository<ErrorCardCallCollection> errorCardCallCollection,
                                IMongoRepository<UserRoleCollection> userRoleCollection,
                                IMongoRepository<UserCollection> userCollection,
                                IMapper mapper)
        {
            _errorCardCallCollection = errorCardCallCollection;
            _userCollection = userCollection;
            _userRoleCollection = userRoleCollection;
            _mapper = mapper;
        }

        public async Task<ResponseModel> Handle(UpdateCurrentTechnicianOnErrorCardCallCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _userCollection.GetUserId();
                FilterDefinition<ErrorCardCallCollection> errorCardFilter = new ExpressionFilterDefinition<ErrorCardCallCollection>
                     (x => x.Id == request.ErrorCardCallId);

                FilterDefinition<UserCollection> userFilter = new ExpressionFilterDefinition<UserCollection>
                     (x => x.Id == userId);

                ProjectionDefinition<UserCollection> technicianProjection = Builders<UserCollection>.Projection
                    .Include(x => x.Id)
                    .Include(x => x.Name)
                    .Include(x => x.RoleId)
                    .Include(x => x.RegisterNumber)
                    .Include(x => x.Surname);

                FindOptions<UserCollection> userFindOption = new FindOptions<UserCollection>
                {
                    Projection = technicianProjection,
                };

                UserCollection user = await _userCollection.FindWithProjection(userFilter, userFindOption);
                UserModel userModel = _mapper.Map<UserModel>(user);
                var role = await _userRoleCollection.FindByIdAsync(user.RoleId);
                UserRoleModel userRoleModel = _mapper.Map<UserRoleModel>(role);
                userModel.Role = userRoleModel;

                switch ((ErrorCardCallAddDropEnum)request.ErrorCardCallAddDrop)
                {
                    case ErrorCardCallAddDropEnum.Add:

                        ProjectionDefinition<ErrorCardCallCollection> errorCardProjection = Builders<ErrorCardCallCollection>.Projection
                                .Include(x => x.Id)
                                .Include(x => x.ErrorCardCallStatus)
                                .Include(x => x.Status)
                                .Include(x => x.UpdatedDate)
                                .Include(x => x.CreatedDate)
                                .Include(x => x.UpdatedById)
                                .Include(x => x.CallWaitingTime)
                                .Include(x => x.InterferingOperator);


                        FindOptions<ErrorCardCallCollection> errorCardFindOption = new FindOptions<ErrorCardCallCollection>
                        {
                            Projection = errorCardProjection,
                        };

                        ErrorCardCallCollection errorCardCall = await _errorCardCallCollection.FindWithProjection(errorCardFilter, errorCardFindOption);

                        var errorCardCallTimeDiff = DateTime.Now - errorCardCall.CreatedDate.ToLocalTime();
                        var errorCardCallTime = new DateTimeModel
                        {
                            Days = errorCardCallTimeDiff.Days,
                            Hours = errorCardCallTimeDiff.Hours,
                            Minutes = errorCardCallTimeDiff.Minutes,
                            Seconds = errorCardCallTimeDiff.Seconds
                        };

                        var addUpdate = Builders<ErrorCardCallCollection>.Update
                            .Set(x => x.InterferingOperator, userModel)
                            .Set(x => x.Status, DocumentStatus.Updated)
                            .Set(x => x.UpdatedById, user.Id)
                            .Set(x => x.UpdatedDate, DateTime.Now)
                            .Set(x => x.ErrorCardCallStatus, ErrorCardCallStatusEnum.Proccessing)
                            .Set(x => x.CallWaitingTime, errorCardCallTime);


                        var updateResult = await _errorCardCallCollection.UpdateDocumentWithSelectedFieldsAsync(errorCardFilter, addUpdate);
                        break;

                    case ErrorCardCallAddDropEnum.Complete:

                        ProjectionDefinition<ErrorCardCallCollection> errorCardCallProjection = Builders<ErrorCardCallCollection>.Projection
                                .Include(x => x.CallWaitingTime)
                                .Include(x => x.CreatedDate);


                        FindOptions<ErrorCardCallCollection> errorCardCallFindOptionComplete = new FindOptions<ErrorCardCallCollection>
                        {
                            Projection = errorCardCallProjection,
                        };

                        ErrorCardCallCollection errorCardCallComplete = await _errorCardCallCollection.FindWithProjection(errorCardFilter, errorCardCallFindOptionComplete);

                        var dt = DateTime.Now;
                        var cr = errorCardCallComplete.CreatedDate.ToLocalTime();
                        var waitingTime = new TimeSpan(errorCardCallComplete.CallWaitingTime.Days, errorCardCallComplete.CallWaitingTime.Hours, errorCardCallComplete.CallWaitingTime.Minutes, errorCardCallComplete.CallWaitingTime.Seconds);
                        var startDate = cr + waitingTime;
                        var complatedDate = dt - startDate;
                        var processingTimeDiff = new TimeSpan(dt.Day, dt.Hour, dt.Minute, dt.Second) - new TimeSpan(
                            errorCardCallComplete.CallWaitingTime.Days,
                            errorCardCallComplete.CallWaitingTime.Hours,
                            errorCardCallComplete.CallWaitingTime.Minutes,
                            errorCardCallComplete.CallWaitingTime.Seconds);



                        var processingTime = new DateTimeModel
                        {
                            Days = complatedDate.Days,
                            Hours = complatedDate.Hours,
                            Minutes = complatedDate.Minutes,
                            Seconds = complatedDate.Seconds
                        };

                        var CompleteUpdate = Builders<ErrorCardCallCollection>.Update
                            .Set(x => x.UpdatedDate, DateTime.Now)
                            .Set(x => x.UpdatedById, user.Id)
                            .Set(x => x.Status, DocumentStatus.Updated)
                            .Set(x => x.CallInterfereTime, processingTime)
                            .Set(x => x.InterferingOperator, userModel)
                            .Set(x => x.Description, request.Description)
                            .Set(x => x.ErrorCardCallStatus, ErrorCardCallStatusEnum.Closed);

                        var addResult = await _errorCardCallCollection.UpdateDocumentWithSelectedFieldsAsync(errorCardFilter, CompleteUpdate);
                        break;


                    case ErrorCardCallAddDropEnum.Drop:

                        ProjectionDefinition<ErrorCardCallCollection> errorCardCallProjectionDrop = Builders<ErrorCardCallCollection>.Projection
                                .Include(x => x.CallWaitingTime);


                        FindOptions<ErrorCardCallCollection> errorCardCallFindOptionDrop = new FindOptions<ErrorCardCallCollection>
                        {
                            Projection = errorCardCallProjectionDrop,
                        };

                        ErrorCardCallCollection errorCardCallDrop = await _errorCardCallCollection.FindWithProjection(errorCardFilter, errorCardCallFindOptionDrop);

                        var dtDrop = DateTime.Now;
                        var processingTimeDiffDrop = new TimeSpan(dtDrop.Hour, dtDrop.Minute, dtDrop.Second) - new TimeSpan(
                            errorCardCallDrop.CallWaitingTime.Hours,
                            errorCardCallDrop.CallWaitingTime.Minutes,
                            errorCardCallDrop.CallWaitingTime.Seconds);

                        var processingTimeDrop = new DateTimeModel
                        {
                            Days = processingTimeDiffDrop.Days,
                            Hours = processingTimeDiffDrop.Hours,
                            Minutes = processingTimeDiffDrop.Minutes,
                            Seconds = processingTimeDiffDrop.Seconds
                        };

                        var dropUpdate = Builders<ErrorCardCallCollection>.Update
                            .Set(x => x.UpdatedDate, DateTime.Now)
                            .Set(x => x.UpdatedById, user.Id)
                            .Set(x => x.Status, DocumentStatus.Updated)
                            .Set(x => x.CallInterfereTime, processingTimeDrop)
                            .Set(x => x.InterferingOperator, null)
                            .Set(x => x.Description, string.Empty)
                            .Set(x => x.ErrorCardCallStatus, ErrorCardCallStatusEnum.Opened);

                        var dropResult = await _errorCardCallCollection.UpdateDocumentWithSelectedFieldsAsync(errorCardFilter, dropUpdate);
                        break;
                }

                return ResponseModel.Success();
            }
            catch (Exception ex)
            {
                return ResponseModel.Fail(ex.Message);

            }
        }
    }
}
