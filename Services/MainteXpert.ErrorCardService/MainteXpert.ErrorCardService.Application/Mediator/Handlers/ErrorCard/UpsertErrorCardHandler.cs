namespace MainteXpert.ErrorCardService.Application.Mediator.Handlers.ErrorCard
{
    public class UpsertErrorCardHandler : IRequestHandler<UpsertErrorCardCommad, ResponseModel<ErrorCardResponseModel>>
    {
        private readonly IMongoRepository<ErrorCardCollection> _collection;
        private readonly IMongoRepository<ErrorCardGroupCollection> _errorCardGroupCollection;
        private readonly IMongoRepository<StationCardGroupCollection> _stationCardGroupCollection;
        private readonly IMongoRepository<UserCollection> _userCollection;
        private readonly IMapper _mapper;
        public UpsertErrorCardHandler(IMongoRepository<ErrorCardCollection> collection, IMapper mapper,
            IMongoRepository<ErrorCardGroupCollection> errorCardGroupCollection,
            IMongoRepository<UserCollection> userCollection,
            IMongoRepository<StationCardGroupCollection> stationCardGroupCollection)
        {
            _collection = collection;
            _mapper = mapper;
            _errorCardGroupCollection = errorCardGroupCollection;
            _userCollection = userCollection;
            _stationCardGroupCollection = stationCardGroupCollection;
        }

        public async Task<ResponseModel<ErrorCardResponseModel>> Handle(UpsertErrorCardCommad command, CancellationToken cancellationToken)
        {
            try
            {
                ErrorCardCollection updatedDocument = new ErrorCardCollection();

                var projection = Builders<UserCollection>.Projection
                     .Include(x => x.Id)
                     .Include(x => x.Name)
                     .Include(x => x.Surname)
                     .Include(x => x.RegisterNumber);

                FindOptions<UserCollection> findOptions = new FindOptions<UserCollection>
                {
                    Projection = projection
                };

                FilterDefinition<UserCollection> userFilter = new ExpressionFilterDefinition<UserCollection>
                        (x => x.Id == _collection.GetUserId());


                if (!string.IsNullOrEmpty(command.Id))
                {
                    var currentDocument = _mapper.Map<ErrorCardCollection>(command);
                    currentDocument.ErrorCardGroup = await _errorCardGroupCollection.FindByIdAsync(command.ErrorCardGroupId);
                    currentDocument.StationCardGroup = await _stationCardGroupCollection.FindByIdAsync(command.StationCardGroupId);



                    var user = await _userCollection.FindWithProjection(userFilter, findOptions);
                    var userResponse = _mapper.Map<UserModel>(user);


                    var update = Builders<ErrorCardCollection>.Update
                        .Set(x => x.ErrorCardGroup, currentDocument.ErrorCardGroup)
                        .Set(x => x.StationCardGroup, currentDocument.StationCardGroup)
                        .Set(x => x.Priority, currentDocument.Priority)
                        .Set(x => x.Description, currentDocument.Description)
                        .Set(x => x.Photos, currentDocument.Photos)
                        .Set(x => x.NumberOfPhotos, currentDocument.Photos.Count)
                        .Set(x => x.Documents, currentDocument.Documents)
                        .Set(x => x.NumberOfDocuments, currentDocument.Documents.Count)
                        .Set(x => x.UpdatedById, _collection.GetUserId())
                        .Set(x => x.ErrorCardUpdater, userResponse)
                        .Set(x => x.UpdatedDate, DateTime.Now)
                        .Set(x => x.Status, DocumentStatus.Updated);

                    var documentFilter = new ExpressionFilterDefinition<ErrorCardCollection>
                         (x => x.Id == command.Id);


                    updatedDocument = await _collection.UpdateDocumentWithSelectedFieldsAsync(documentFilter, update);
                }
                else
                {
                    var document = _mapper.Map<ErrorCardCollection>(command);

                    var user = await _userCollection.FindWithProjection(userFilter, findOptions);
                    var userResponse = _mapper.Map<UserModel>(user);

                    document.ErrorCardGroup = await _errorCardGroupCollection.FindByIdAsync(command.ErrorCardGroupId);
                    document.StationCardGroup = await _stationCardGroupCollection.FindByIdAsync(command.StationCardGroupId);
                    document.ErrorCardStatus = ErrorCardStatus.Opened;
                    document.ErrorCardCreater = userResponse;
                    updatedDocument = await _collection.InsertOneAsync(document);
                }
                var responseModel = _mapper.Map<ErrorCardResponseModel>(updatedDocument);
                if (responseModel != null && responseModel.CurrentTechnician != null)
                {
                    responseModel.IsAuthUserWorkingOn = _collection.GetUserId().Equals(responseModel.CurrentTechnician.Id);
                }

                return ResponseModel<ErrorCardResponseModel>.Success(data: responseModel);
            }
            catch (Exception ex)
            {
                return ResponseModel<ErrorCardResponseModel>.Fail(data: null, message: ex.Message);
            }

        }
    }

}
