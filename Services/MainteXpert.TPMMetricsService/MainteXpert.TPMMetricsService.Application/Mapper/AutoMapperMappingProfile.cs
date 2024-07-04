
namespace MainteXpert.TPMMetricsService.Application.Mapper
{
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            CreateMap<TPMMetricsCollection, TPMMetricsModel>().ReverseMap();
            CreateMap<CreateTPMMetricsCommand, TPMMetricsCollection>();
            CreateMap<UpdateTPMMetricsCommand, TPMMetricsCollection>();
        }
    }
}
