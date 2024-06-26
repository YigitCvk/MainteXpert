namespace MainteXpert.EquipmentService.Application.Mapper
{
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            CreateMap<CreateEquipmentCommand, EquipmentCollection>();
            CreateMap<UpdateEquipmentCommand, EquipmentCollection>();
            CreateMap<EquipmentCollection, MainteXpert.Common.Models.Equipment.EquipmentModel>().ReverseMap();
        }
    }
}
