namespace MainteXpert.KaizenService.Application.Mapper
{
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            CreateMap<KaizenImprovementCollection, KaizenImprovementModel>().ReverseMap();
        }
    }
}
