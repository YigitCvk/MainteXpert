namespace MainteXpert.AnalyticsService.Application.Mapper
{
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            CreateMap<AnalyticsDataCollection, AnalyticsDataModel>().ReverseMap();
            CreateMap<CreateAnalyticsDataCommand, AnalyticsDataCollection>();
        }
    }
}
