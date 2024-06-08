namespace Authentication.Application.Mapper
{
    public class AutoMapperMappingProfile : Profile
    {

        public AutoMapperMappingProfile()
        {
            CreateMap<UserCollection, AuthUserModel>();
            CreateMap<UserRoleCollection, UserRoleModel>();

        }
    }
}
