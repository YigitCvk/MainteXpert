namespace MainteXpert.ErrorCardService.Application.Mapper
{
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            CreateMap<FactoryGroupCollection, FactoryGroupModel>().ReverseMap();
            CreateMap<StationCodeGroupCollection, StationCodeGroupModel>().ReverseMap();
            CreateMap<StationGroupCollection, StationGroupModel>().ReverseMap();
            CreateMap<StationNameGroupCollection, StationNameGroupModel>().ReverseMap();
            CreateMap<UserRoleCollection, UserRoleModel>().ReverseMap();

            CreateMap<ErrorCardGroupModel, ErrorCardGroupCollection>().ReverseMap();
            CreateMap<UserCollection, WorkerModel>().ReverseMap();
            CreateMap<UserCollection, UserModel>().ReverseMap();

            CreateMap<StationCardGroupCollection, StationCardGroupModel>()
                 .ForMember(x => x.FactoryGroupModel, y => y.MapFrom(src => src.FactoryGroupCollection))
                 .ForMember(x => x.StationCodeGroupModel, y => y.MapFrom(src => src.StationCodeGroupCollection))
                 .ForMember(x => x.StationGroupModel, y => y.MapFrom(src => src.StationGroupCollection))
                 .ForMember(x => x.StationNameGroupModel, y => y.MapFrom(src => src.StationNameGroupCollection));

            CreateMap<UpsertErrorCardCommad, ErrorCardCollection>()
                .ForMember(x => x.NumberOfPhotos, y => y.MapFrom(src => src.Photos.Count))
                .ForMember(x => x.NumberOfDocuments, y => y.MapFrom(src => src.Documents.Count))
                .AfterMap((src, dst, context) =>
                {
                    dst.Photos.ForEach(p =>
                    {
                        if (string.IsNullOrEmpty(p.Id))
                        {
                            p.Id = ObjectId.GenerateNewId().ToString();
                        }
                        p.ImageHeight = ImageHelper.ConvertFromBase64ToBitmap(p.Data).Height;
                        p.ImageWidth = ImageHelper.ConvertFromBase64ToBitmap(p.Data).Width;
                    });

                    dst.Documents.ForEach(d =>
                    {
                        if (string.IsNullOrEmpty(d.Id))
                        {
                            d.Id = ObjectId.GenerateNewId().ToString();
                        }
                    });

                    dst.TechnicianPhotos.ForEach(p =>
                    {
                        if (string.IsNullOrEmpty(p.Id))
                        {
                            p.Id = ObjectId.GenerateNewId().ToString();
                        }
                        p.ImageHeight = ImageHelper.ConvertFromBase64ToBitmap(p.Data).Height;
                        p.ImageWidth = ImageHelper.ConvertFromBase64ToBitmap(p.Data).Width;
                    });

                    dst.TechnicianDocuments.ForEach(d =>
                    {
                        if (string.IsNullOrEmpty(d.Id))
                        {
                            d.Id = ObjectId.GenerateNewId().ToString();
                        }
                    });
                });

            CreateMap<ErrorCardCollection, ErrorCardResponseModel>()
                .AfterMap((src, dst, context) =>
                {
                  if(src.ErrorCardStatus == Common.Enums.ErrorCardStatus.Opened)
                    {
                        var activityTimeDiff = DateTime.Now - src.CreatedDate.ToLocalTime();
                        dst.ErrorCardActivityTime = new Common.Models.DateTimeModel
                        {
                            Days = activityTimeDiff.Days,
                            Hours= activityTimeDiff.Hours,
                            Minutes = activityTimeDiff.Minutes,
                            Seconds = activityTimeDiff.Seconds
                        };
                    }

                    if (src.ErrorCardStatus == Common.Enums.ErrorCardStatus.InProccess)
                    {
                        var dt = DateTime.Now;
                        var activityTimeDiff = new TimeSpan(dt.Hour,dt.Minute,dt.Second) - new TimeSpan(
                            src.ErrorCardActivityTime.Hours,
                            src.ErrorCardActivityTime.Minutes,
                            src.ErrorCardActivityTime.Seconds);

                        dst.ErrorCardProcessingTime = new Common.Models.DateTimeModel
                        {
                            Days = activityTimeDiff.Days,
                            Hours = activityTimeDiff.Hours,
                            Minutes = activityTimeDiff.Minutes,
                            Seconds = activityTimeDiff.Seconds
                        };
                    }
                });
            CreateMap<ErrorCardCollection, ErrorCardHistoryModel>().ReverseMap();


            CreateMap<PaginationDocument<ErrorCardCollection>, PaginationDocument<ErrorCardResponseModel>>();
           

            CreateMap<InsertErrorCardCallCommand, ErrorCardCallCollection>();
            CreateMap<ErrorCardCallCollection, ErrorCardCallResponseModel>()
                .ForMember(x => x.ErrorCardCallStatus, y => y.MapFrom(src => new LookupValue
                {
                    Name = src.ErrorCardCallStatus.GetDisplayName().Name,
                    Value = (int)src.ErrorCardCallStatus
                }))
            .AfterMap((src, dst, context) =>
             {
                 if (src.ErrorCardCallStatus == Common.Enums.ErrorCardCallStatusEnum.Opened)
                 {
                     var activityTimeDiff = DateTime.Now - src.CreatedDate;
                     dst.CallWaitingTime = new Common.Models.DateTimeModel
                     {
                         Days = activityTimeDiff.Days,
                         Hours = activityTimeDiff.Hours,
                         Minutes = activityTimeDiff.Minutes,
                         Seconds = activityTimeDiff.Seconds
                     };
                 }

                 if (src.ErrorCardCallStatus == Common.Enums.ErrorCardCallStatusEnum.Proccessing)
                 {
                     var dt = DateTime.Now;
                     var activityTimeDiff = new TimeSpan(dt.Hour, dt.Minute, dt.Second) - new TimeSpan(
                         src.CallWaitingTime.Hours,
                         src.CallWaitingTime.Minutes,
                         src.CallWaitingTime.Seconds);

                     dst.CallInterfereTime = new Common.Models.DateTimeModel
                     {
                         Days = activityTimeDiff.Days,
                         Hours = activityTimeDiff.Hours,
                         Minutes = activityTimeDiff.Minutes,
                         Seconds = activityTimeDiff.Seconds
                     };
                 }
             });
            CreateMap<PaginationDocument<ErrorCardCallCollection>, PaginationDocument<ErrorCardCallResponseModel>>();

        }
    }
}
