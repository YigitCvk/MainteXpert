using Authentication.Application.Models;
using AutoMapper;
using Common.Models.User;
using Mongo.Collections.User;

namespace Authentication.Application.Mapper
{
    public class AutoMapperMappingProfile :Profile
    {

        public AutoMapperMappingProfile()
        {
            CreateMap<UserCollection, AuthUserModel>();
            CreateMap<UserRoleCollection, UserRoleModel>();
                
        }
    }
}
