namespace MainteXpert.ErrorCardService.Application.Mediator.Handlers.ErrorCard
{
    public class GetErrorCardAttachmentByIdHandler : IRequestHandler<GetErrorCardAttechmentByIdQuery, ResponseModel<AttachmentModel>>
    {

        private readonly IMongoRepository<ErrorCardCollection> _collection;
        private readonly IMapper _mapper;
        public GetErrorCardAttachmentByIdHandler(IMongoRepository<ErrorCardCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }
        public async Task<ResponseModel<AttachmentModel>> Handle(GetErrorCardAttechmentByIdQuery request, CancellationToken cancellationToken)
        {
            var builder = Builders<ErrorCardCollection>.Projection;

            try
            {
                var projection = Builders<ErrorCardCollection>.Projection
                     .Include(x => x.Id);

                if (request.IsPhoto)
                {

                    var photoProjection = builder.Include(x => x.Photos).ElemMatch(x => x.Photos, x => x.Id.Equals(request.AttachmentId));
                    projection = Builders<ErrorCardCollection>.Projection.Combine(projection, photoProjection);
                }
                else
                {
                    var documentProjection = builder.Include(x => x.Documents).ElemMatch(x => x.Documents, x => x.Id.Equals(request.AttachmentId));
                    var techDocumentProjection = builder.Include(x => x.TechnicianDocuments);
                    projection = Builders<ErrorCardCollection>.Projection.Combine(projection, documentProjection);
                    projection = Builders<ErrorCardCollection>.Projection.Combine(projection, techDocumentProjection);
                }


                FindOptions<ErrorCardCollection> findOptions = new FindOptions<ErrorCardCollection>
                {
                    Projection = projection
                };

                FilterDefinition<ErrorCardCollection> filter = new ExpressionFilterDefinition<ErrorCardCollection>
                    (x => x.Id == request.ErrorCardId);

                var result = await _collection.FindWithProjection(filter, findOptions);
                var attachmentModel = new AttachmentModel();
                if (request.IsPhoto)
                {
                    if (request.PhotoCompress.IsCompress)
                    {
                        attachmentModel = result.Photos
                            .Select(x =>
                            {
                                x.Data = ImageHelper.CompressImage(x.Data, request.PhotoCompress.Width, request.PhotoCompress.Height);
                                x.ImageHeight = ImageHelper.ConvertFromBase64ToBitmap(x.Data).Height;
                                x.ImageWidth = ImageHelper.ConvertFromBase64ToBitmap(x.Data).Width;
                                return x;
                            })
                            .First();
                    }
                    else
                        attachmentModel = result.Photos.FirstOrDefault();
                }
                else
                {
                    attachmentModel = result.Documents.Any() ? result.Documents.FirstOrDefault() : result.TechnicianDocuments.FirstOrDefault();
                }
                return ResponseModel<AttachmentModel>.Success(attachmentModel);

            }
            catch (Exception ex)
            {
                return ResponseModel<AttachmentModel>.Fail(data: null, message: ex.Message);

            }
        }
    }

}
