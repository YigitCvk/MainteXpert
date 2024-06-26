

using MaineXpert.EquipmentService.Application.Mediator.Commands;

namespace MaineXpert.EquipmentService.Application.Mapper
{
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            CreateMap<EquipmentCollection, EquipmentModel>().ReverseMap();
            CreateMap<CreateEquipmentCommand, EquipmentModel>();
            CreateMap<UpdateEquipmentCommand, EquipmentModel>();
        }
    }
}
