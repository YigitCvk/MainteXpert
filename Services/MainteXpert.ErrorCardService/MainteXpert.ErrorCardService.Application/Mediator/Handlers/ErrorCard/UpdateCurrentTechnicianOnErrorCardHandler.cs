namespace MainteXpert.ErrorCardService.Application.Mediator.Handlers.ErrorCard
{

    public class UpdateCurrentTechnicianOnErrorCardHandler : IRequestHandler<UpdateCurrentTechnicianOnErrorCardCommand, ResponseModel>
    {

        private readonly IMongoRepository<ErrorCardCollection> _errorCardCollection;
        private readonly IMongoRepository<UserCollection> _userCollection;
        private readonly IMongoRepository<UserRoleCollection> _userRoleCollection;
        private readonly IMapper _mapper;
        public UpdateCurrentTechnicianOnErrorCardHandler(
                                IMongoRepository<ErrorCardCollection> errorCardCollection,
                                IMongoRepository<UserRoleCollection> userRoleCollection,
                                IMongoRepository<UserCollection> userCollection,
                                IMapper mapper)
        {
            _errorCardCollection = errorCardCollection;
            _userCollection = userCollection;
            _userRoleCollection = userRoleCollection;
            _mapper = mapper;
        }


        public async Task<ResponseModel> Handle(UpdateCurrentTechnicianOnErrorCardCommand request, CancellationToken cancellationToken)
        {
            try
            {
                FilterDefinition<ErrorCardCollection> errorCardFilter = new ExpressionFilterDefinition<ErrorCardCollection>
                     (x => x.Id == request.ErrorCardId);

                FilterDefinition<UserCollection> userFilter = new ExpressionFilterDefinition<UserCollection>
                     (x => x.Id == _errorCardCollection.GetUserId());


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
                WorkerModel workerModel = _mapper.Map<WorkerModel>(user);
                workerModel.UserRole = _mapper.Map<UserRoleModel>(role);

                switch (request.ErrorCardAddDrop)
                {
                    case ErrorCardAddDropEnum.Add:

                        ProjectionDefinition<ErrorCardCollection> errorCardProjection = Builders<ErrorCardCollection>.Projection
                                .Include(x => x.Id)
                                .Include(x => x.ErrorCardStatus)
                                .Include(x => x.Status)
                                .Include(x => x.UpdatedDate)
                                .Include(x => x.CreatedDate)
                                .Include(x => x.UpdatedById)
                                .Include(x => x.ErrorCardUpdater)
                                .Include(x => x.ErrorCardActivityTime)
                                .Include(x => x.CurrentTechnician);


                        FindOptions<ErrorCardCollection> errorCardFindOption = new FindOptions<ErrorCardCollection>
                        {
                            Projection = errorCardProjection,
                        };

                        ErrorCardCollection errorCard = await _errorCardCollection.FindWithProjection(errorCardFilter, errorCardFindOption);

                        workerModel.StartJobTime = DateTime.Now;

                        var activityTimeDiff = DateTime.Now - errorCard.CreatedDate.ToLocalTime();
                        var activityTime = new DateTimeModel
                        {
                            Days = activityTimeDiff.Days,
                            Hours = activityTimeDiff.Hours,
                            Minutes = activityTimeDiff.Minutes,
                            Seconds = activityTimeDiff.Seconds,

                        };
                        var addUpdate = Builders<ErrorCardCollection>.Update
                            .Set(x => x.CurrentTechnician, workerModel)
                            .Set(x => x.Status, DocumentStatus.Updated)
                            .Set(x => x.UpdatedById, user.Id)
                            .Set(x => x.ErrorCardUpdater, userModel)
                            .Set(x => x.UpdatedDate, DateTime.Now)
                            .Set(x => x.ErrorCardActivityTime, activityTime)
                            .Set(x => x.ErrorCardStatus, ErrorCardStatus.InProccess);


                        var updateResult = await _errorCardCollection.UpdateDocumentWithSelectedFieldsAsync(errorCardFilter, addUpdate);
                        break;

                    case ErrorCardAddDropEnum.Complete:

                        ProjectionDefinition<ErrorCardCollection> errorCardCompleteProjection = Builders<ErrorCardCollection>.Projection
                            .Include(x => x.Id)
                            .Include(x => x.ErrorCardStatus)
                            .Include(x => x.Status)
                            .Include(x => x.UpdatedDate)
                            .Include(x => x.UpdatedById)
                            .Include(x => x.ErrorCardUpdater)
                            .Include(x => x.CurrentTechnician)
                            .Include(x => x.ErrorCardProcessingTime)
                            .Include(x => x.ErrorCardActivityTime)
                            .Include(x => x.ErrorCardHistoryModels)
                            .Include(x => x.TechnicianPhotos)
                            .Include(x => x.TechnicianDocuments)
                            .Include(x => x.TechnicianDescription);

                        FindOptions<ErrorCardCollection> errorCardCompleteFindOption = new FindOptions<ErrorCardCollection>
                        {
                            Projection = errorCardCompleteProjection,
                        };


                        ErrorCardCollection errorCardComplete = await _errorCardCollection.FindWithProjection(errorCardFilter, errorCardCompleteFindOption);

                        errorCardComplete.CurrentTechnician.EndJobTime = DateTime.Now;

                        ErrorCardHistoryModel errorCardCompleteHistory = new ErrorCardHistoryModel
                        {
                            Description = request.Description,
                            Documents = request.Documents,
                            Photos = request.Photos,
                            WorkerModel = errorCardComplete.CurrentTechnician,
                            ErrorCardStatus = ErrorCardStatus.Closed
                        };
                        var list = errorCardComplete.ErrorCardHistoryModels;
                        list.Add(errorCardCompleteHistory);

                        var dt = DateTime.Now;

                        var processingTimeDiff = dt - errorCardComplete.CurrentTechnician.StartJobTime.ToLocalTime();
                        var processingTime = new DateTimeModel
                        {
                            Days = processingTimeDiff.Days,
                            Hours = processingTimeDiff.Hours,
                            Minutes = processingTimeDiff.Minutes,
                            Seconds = processingTimeDiff.Seconds
                        };

                        if (request.Documents.Any())
                        {
                            foreach (var doc in request.Documents)
                            {
                                if (string.IsNullOrEmpty(doc.Id))
                                {
                                    doc.Id = ObjectId.GenerateNewId().ToString();
                                }
                            }
                        }

                        if (request.Photos.Any())
                        {
                            foreach (var doc in request.Photos)
                            {
                                if (string.IsNullOrEmpty(doc.Id))
                                {
                                    doc.Id = ObjectId.GenerateNewId().ToString();
                                }
                            }
                        }


                        var CompleteUpdate = Builders<ErrorCardCollection>.Update
                            .Set(x => x.CurrentTechnician, null)
                            .Set(x => x.UpdatedDate, DateTime.Now)
                            .Set(x => x.UpdatedById, user.Id)
                            .Set(x => x.ErrorCardUpdater, userModel)
                            .Set(x => x.Status, DocumentStatus.Updated)
                            .Set(x => x.ErrorCardHistoryModels, list)
                            .Set(x => x.ErrorCardProcessingTime, processingTime)
                            .Set(x => x.ErrorCardStatus, ErrorCardStatus.Closed)
                            .Set(x => x.TechnicianDescription, request.Description)
                            .Set(x => x.TechnicianDocuments, request.Documents)
                            .Set(x => x.TechnicianPhotos, request.Photos);

                        var addResult = await _errorCardCollection.UpdateDocumentWithSelectedFieldsAsync(errorCardFilter, CompleteUpdate);
                        break;


                    case ErrorCardAddDropEnum.Drop:

                        ProjectionDefinition<ErrorCardCollection> errorCardDropProjection = Builders<ErrorCardCollection>.Projection
                            .Include(x => x.Id)
                            .Include(x => x.ErrorCardStatus)
                            .Include(x => x.Status)
                            .Include(x => x.UpdatedDate)
                            .Include(x => x.UpdatedById)
                            .Include(x => x.ErrorCardUpdater)
                            .Include(x => x.CurrentTechnician)
                            .Include(x => x.ErrorCardHistoryModels);

                        FindOptions<ErrorCardCollection> errorCardDropFindOption = new FindOptions<ErrorCardCollection>
                        {
                            Projection = errorCardDropProjection,
                        };

                        ErrorCardCollection errorCardDrop = await _errorCardCollection.FindWithProjection(errorCardFilter, errorCardDropFindOption);
                        errorCardDrop.CurrentTechnician.EndJobTime = DateTime.Now;
                        ErrorCardHistoryModel errorCardHistory = new ErrorCardHistoryModel
                        {
                            Description = request.Description,
                            Documents = request.Documents,
                            Photos = request.Photos,
                            WorkerModel = errorCardDrop.CurrentTechnician,
                            ErrorCardStatus = ErrorCardStatus.Opened
                        };
                        var dropList = errorCardDrop.ErrorCardHistoryModels;
                        dropList.Add(errorCardHistory);

                        var dropUpdate = Builders<ErrorCardCollection>.Update
                            .Set(x => x.CurrentTechnician, null)
                            .Set(x => x.UpdatedDate, DateTime.Now)
                            .Set(x => x.UpdatedById, user.Id)
                            .Set(x => x.ErrorCardUpdater, userModel)
                            .Set(x => x.Status, DocumentStatus.Updated)
                            .Set(x => x.ErrorCardHistoryModels, dropList)
                            .Set(x => x.ErrorCardStatus, ErrorCardStatus.Opened)
                            .Set(x => x.TechnicianDescription, string.Empty)
                            .Set(x => x.TechnicianDocuments, null)
                            .Set(x => x.TechnicianPhotos, null);

                        var addDropResult = await _errorCardCollection.UpdateDocumentWithSelectedFieldsAsync(errorCardFilter, dropUpdate);
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
