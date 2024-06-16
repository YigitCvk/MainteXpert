namespace MainteXpert.ErrorCardService.Application.Mediator.Handlers.ErrorCard
{
    public class UpdateErrorCardAttachmentHandler : IRequestHandler<UpdateErrorCardAttachmentCommand, ResponseModel>
    {
        private readonly IMongoRepository<ErrorCardCollection> _collection;
        private readonly IMapper _mapper;
        public UpdateErrorCardAttachmentHandler(IMongoRepository<ErrorCardCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        // TODO: Resim güncelleme ve silme işlemleri güncellenmeli. Tüm dosyalar yerine ilgili dosyalar eklenmeli veya silinmeli.
        public async Task<ResponseModel> Handle(UpdateErrorCardAttachmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                FilterDefinition<ErrorCardCollection> filter = new ExpressionFilterDefinition<ErrorCardCollection>
                        (x => x.Id == request.ErrorCardId);


                ProjectionDefinition<ErrorCardCollection> projection;

                switch (request.AttachmentType)
                {
                    case AttachmentType.Photo:

                        projection = Builders<ErrorCardCollection>.Projection.Include(x => x.Id)
                                    .Include(x => x.Photos);
                        break;

                    case AttachmentType.Document:
                        projection = Builders<ErrorCardCollection>.Projection.Include(x => x.Id)
                             .Include(x => x.Documents);
                        break;

                    default:
                        projection = Builders<ErrorCardCollection>.Projection.Include(x => x.Id);

                        break;
                }

                FindOptions<ErrorCardCollection> findOptions = new FindOptions<ErrorCardCollection>
                {
                    Projection = projection
                };


                var result = await _collection.FindWithProjection(filter, findOptions);
                UpdateDefinition<ErrorCardCollection> updateDefinition;
                switch (request.AttachmentType)
                {
                    case AttachmentType.Photo:
                        request.Attachments.ForEach(x =>
                        {
                            x.UpdatedDate = DateTime.Now;
                            if (string.IsNullOrEmpty(x.Id))
                            {
                                x.Id = Guid.NewGuid().ToString();
                            }
                        });

                        updateDefinition = Builders<ErrorCardCollection>.Update
                            .Set(x => x.Photos, request.Attachments)
                            .Set(x => x.NumberOfPhotos, request.Attachments.Count);

                        break;

                    case AttachmentType.Document:
                        request.Attachments.ForEach(x =>
                        {
                            x.UpdatedDate = DateTime.Now;
                            if (string.IsNullOrEmpty(x.Id))
                            {
                                x.Id = Guid.NewGuid().ToString();
                            }
                        });
                        updateDefinition = Builders<ErrorCardCollection>.Update
                            .Set(x => x.Documents, request.Attachments)
                            .Set(x => x.NumberOfDocuments, request.Attachments.Count);
                        break;

                    default:
                        updateDefinition = Builders<ErrorCardCollection>.Update.Set(x => x.Description, result.Description);
                        break;
                }

                var updateResult = await _collection.UpdateDocumentWithSelectedFieldsAsync(filter, updateDefinition);

                return ResponseModel.Success();
            }
            catch (Exception ex)
            {
                return ResponseModel.Fail(ex.Message);
            }
        }
    }

}
