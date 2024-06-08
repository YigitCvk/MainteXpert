namespace MainteXpert.UserService.Application.Mapper
{
    public class AutoMapperMappingProfile : Profile
    {

        public AutoMapperMappingProfile()
        {
            CreateMap<UserCollection, UserModel>().ReverseMap();
            CreateMap<UpsertUserCommand, UserCollection>().ReverseMap();

            CreateMap<UpsertUserRoleCommand, UserRoleCollection>().ReverseMap();
            CreateMap<UserRoleCollection, UserRoleModel>().ReverseMap();

            CreateMap<PermissionModel, UserRolePermissionModel>().ReverseMap();
        }
    }
}
