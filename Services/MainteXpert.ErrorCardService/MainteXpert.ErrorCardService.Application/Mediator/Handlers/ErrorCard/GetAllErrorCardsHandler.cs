namespace MainteXpert.ErrorCardService.Application.Mediator.Handlers.ErrorCard
{
    public class GetAllErrorCardHandler : IRequestHandler<GetAllErrorCardsQuery, ResponseModel<PaginationDocument<ErrorCardResponseModel>>>
    {
        private readonly IMongoRepository<ErrorCardCollection> _collection;
        private readonly IMapper _mapper;
        public GetAllErrorCardHandler(IMongoRepository<ErrorCardCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<PaginationDocument<ErrorCardResponseModel>>> Handle(GetAllErrorCardsQuery request, CancellationToken cancellationToken)
        {
            var builder = Builders<ErrorCardCollection>.Filter;
            var filter = builder.Empty;

            try
            {
                if (!string.IsNullOrEmpty(request.StationCardId))
                {

                    var stationCodeFilter = Builders<ErrorCardCollection>.Filter
                        .Eq(t => t.StationCardGroup.Id, request.StationCardId);

                    filter &= stationCodeFilter;
                }

                if (!string.IsNullOrEmpty(request.ErrorCardGroupId))
                {
                    var ecg = builder.Eq(x => x.ErrorCardGroup.Id, request.ErrorCardGroupId);
                    filter &= ecg;
                }

                if (!string.IsNullOrEmpty(request.CreatorOperatorId))
                {
                    var eco = builder.Eq(x => x.CreatedById, request.CreatorOperatorId);
                    filter &= eco;
                }

                if (request.DocumentStatus.HasValue)
                {
                    var dcs = builder.Eq(x => x.Status, (DocumentStatus)request.DocumentStatus.Value);
                    filter &= dcs;
                }

                if (request.ErrorCardStatus.HasValue)
                {
                    var ecs = builder.Eq(x => x.ErrorCardStatus, (ErrorCardStatus)request.ErrorCardStatus.Value);
                    filter &= ecs;
                }

                if (request.Priority.HasValue)
                {
                    var pri = builder.Eq(x => x.Priority, (PriorityEnum)request.Priority.Value);
                    filter &= pri;
                }


                ProjectionDefinition<ErrorCardCollection> projection;


                if (request.WithPhoto)
                {
                    if (request.WithTechnicianData)
                        projection = Builders<ErrorCardCollection>.Projection.Include(x => x.Id)
                         .Exclude(x => x.Documents)
                         //.Exclude(x => x.TechnicianDocuments)
                         .Exclude(x => x.ErrorCardHistoryModels);
                    else
                        projection = Builders<ErrorCardCollection>.Projection.Include(x => x.Id)
                         .Exclude(x => x.Documents)
                         .Exclude(x => x.TechnicianDocuments)
                         .Exclude(x => x.TechnicianPhotos)
                         .Exclude(x => x.TechnicianDescription)
                         .Exclude(x => x.ErrorCardHistoryModels);
                }
                else
                {
                    projection = Builders<ErrorCardCollection>.Projection.Include(x => x.Id)
                     .Exclude(x => x.Documents)
                     .Exclude(x => x.TechnicianDocuments)
                     .Exclude(x => x.TechnicianPhotos)
                     .Exclude(x => x.TechnicianDescription)
                     .Exclude(x => x.ErrorCardHistoryModels)
                     .Exclude(x => x.Photos);
                }


                FindOptions<ErrorCardCollection> findOptions = new FindOptions<ErrorCardCollection>
                {
                    Projection = projection
                };



                SortDefinition<ErrorCardCollection> sortDefinition = Builders<ErrorCardCollection>
                    .Sort.Descending(x => x.CreatedDate).Ascending(x => x.ErrorCardStatus);

                var result = _collection.FindAllWithProjection(filter, findOptions, request.Pagination, sortDefinition);
                var documents = result.Document.ToList();
                documents.ForEach(document =>
                {
                    var takenPhotos = document.Photos.Take(request.NumberOfPhotoData).ToList();
                    if (request.PhotoCompress.IsCompress)
                    {
                        var compressedTakenPhotos = takenPhotos
                        .Select(x =>
                        {
                            x.Data = ImageHelper.CompressImage(x.Data, request.PhotoCompress.Width, request.PhotoCompress.Height);
                            x.ImageHeight = ImageHelper.ConvertFromBase64ToBitmap(x.Data).Height;
                            x.ImageWidth = ImageHelper.ConvertFromBase64ToBitmap(x.Data).Width;
                            return x;
                        }
                        )
                        .ToList();
                    }
                    else
                        document.Photos = takenPhotos;

                    if (document.TechnicianPhotos is not null)
                    {
                        var takenTechPhotos = document.TechnicianPhotos.Take(request.NumberOfPhotoData).ToList();
                        if (request.PhotoCompress.IsCompress)
                        {
                            var compressedTakenPhotos = takenPhotos
                            .Select(x =>
                            {
                                x.Data = ImageHelper.CompressImage(x.Data, request.PhotoCompress.Width, request.PhotoCompress.Height);
                                x.ImageHeight = ImageHelper.ConvertFromBase64ToBitmap(x.Data).Height;
                                x.ImageWidth = ImageHelper.ConvertFromBase64ToBitmap(x.Data).Width;
                                return x;
                            }
                            )
                            .ToList();
                        }
                        else
                            document.TechnicianPhotos = takenTechPhotos;
                    }

                });
                result.Document = documents;
                var response = _mapper.Map<PaginationDocument<ErrorCardResponseModel>>(result);
                foreach (var resp in response.Document)
                {
                    if (resp.CurrentTechnician is not null)
                    {
                        resp.IsAuthUserWorkingOn = _collection.GetUserId().Equals(resp.CurrentTechnician.Id);

                    }
                }
                return ResponseModel<PaginationDocument<ErrorCardResponseModel>>.Success(response);
            }
            catch (Exception ex)
            {
                return ResponseModel<PaginationDocument<ErrorCardResponseModel>>.Fail(data: null, message: ex.Message);

            }

        }
    }
}
