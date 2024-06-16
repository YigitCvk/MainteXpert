namespace MainteXpert.ErrorCardService.Application.Mediator.Handlers.ErrorCard
{
    public class GetAllErrorCardAttachmentsHandler : IRequestHandler<GetAllErrorCardAttachmentsQuery, ResponseModel<ErrorCardAttachmentResponseListModel>>
    {

        private readonly IMongoRepository<ErrorCardCollection> _collection;
        private readonly IMapper _mapper;

        public GetAllErrorCardAttachmentsHandler(IMongoRepository<ErrorCardCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ErrorCardAttachmentResponseListModel>> Handle(GetAllErrorCardAttachmentsQuery request, CancellationToken cancellationToken)
        {
            var builder = Builders<ErrorCardCollection>.Projection;

            try
            {
                var projection = Builders<ErrorCardCollection>.Projection
                     .Include(x => x.Id);


                if (request.IsPhoto)
                {

                    var photoProjection = builder.Include(x => x.Photos);
                    projection = Builders<ErrorCardCollection>.Projection.Combine(projection, photoProjection);
                }
                if (request.IsDocument)
                {
                    var documentProjection = builder.Include(x => x.Documents);
                    projection = Builders<ErrorCardCollection>.Projection.Combine(projection, documentProjection);
                }
                if (request.IsTechnicianPhoto)
                {

                    var techPhotoProjection = builder.Include(x => x.TechnicianPhotos);
                    projection = Builders<ErrorCardCollection>.Projection.Combine(projection, techPhotoProjection);
                }
                if (request.IsTechnicianDocument)
                {
                    var techDocumentProjection = builder.Include(x => x.TechnicianDocuments);
                    projection = Builders<ErrorCardCollection>.Projection.Combine(projection, techDocumentProjection);
                }

                FindOptions<ErrorCardCollection> findOptions = new FindOptions<ErrorCardCollection>
                {
                    Projection = projection
                };

                FilterDefinition<ErrorCardCollection> filter = new ExpressionFilterDefinition<ErrorCardCollection>
                    (x => x.Id == request.ErrorCardId);

                var result = await _collection.FindWithProjection(filter, findOptions);
                var attachmentResponseModel = new ErrorCardAttachmentResponseListModel();

                if (!request.IsPhotoWithData)
                    result.Photos.ForEach(p => p.Data = null);
                if (!request.IsTechnicianPhotoWithData)
                    result.TechnicianPhotos.ForEach(p => p.Data = null);
                else
                {
                    if (request.PhotoCompress.IsCompress)
                    {
                        result.Photos
                            .Select(x =>
                            {
                                x.Data = ImageHelper.CompressImage(x.Data, request.PhotoCompress.Width, request.PhotoCompress.Height);
                                x.ImageHeight = ImageHelper.ConvertFromBase64ToBitmap(x.Data).Height;
                                x.ImageWidth = ImageHelper.ConvertFromBase64ToBitmap(x.Data).Width;
                                return x;
                            })
                            .ToList();
                    }

                    if (request.TechnicianPhotoCompress.IsCompress)
                    {
                        result.TechnicianPhotos
                            .Select(x =>
                            {
                                x.Data = ImageHelper.CompressImage(x.Data, request.PhotoCompress.Width, request.PhotoCompress.Height);
                                x.ImageHeight = ImageHelper.ConvertFromBase64ToBitmap(x.Data).Height;
                                x.ImageWidth = ImageHelper.ConvertFromBase64ToBitmap(x.Data).Width;
                                return x;
                            })
                            .ToList();
                    }
                }
                if (!request.IsDocumentWithData)
                    result.Documents.ForEach(d => d.Data = null);
                if (!request.IsTechnicianDocumentWithData)
                    result.TechnicianDocuments.ForEach(d => d.Data = null);
                attachmentResponseModel.Photos = result.Photos;
                attachmentResponseModel.TechnicianPhotos = result.TechnicianPhotos;
                attachmentResponseModel.Documents = result.Documents;
                attachmentResponseModel.TechnicianDocuments = result.TechnicianDocuments;
                return ResponseModel<ErrorCardAttachmentResponseListModel>.Success(attachmentResponseModel);
            }
            catch (Exception ex)
            {
                return ResponseModel<ErrorCardAttachmentResponseListModel>.Fail(data: null, message: ex.Message);

            }
        }
    }
}
