namespace MainteXpert.MaintenanceSchedule.Application.Mapper
{
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            // MaintenanceTaskCollection ve MaintenanceTaskModel arasında mapping
            CreateMap<MaintenanceTaskCollection, MaintenanceTaskModel>().ReverseMap();

            // Command ve Query sınıflarına göre mapping işlemleri
            CreateMap<MaintenanceTaskCollection, CreateMaintenanceTaskCommand>().ReverseMap();
            CreateMap<MaintenanceTaskCollection, UpdateMaintenanceTaskCommand>().ReverseMap();
            CreateMap<MaintenanceTaskCollection, DeleteMaintenanceTaskCommand>().ReverseMap();
            CreateMap<MaintenanceTaskCollection, GetAllMaintenanceTaskQuery>().ReverseMap();
            CreateMap<MaintenanceTaskCollection, GetMaintenanceTaskByIdQuery>().ReverseMap();

            // MaintenanceTaskModel için mapping
            CreateMap<MaintenanceTaskModel, CreateMaintenanceTaskCommand>().ReverseMap();
            CreateMap<MaintenanceTaskModel, UpdateMaintenanceTaskCommand>().ReverseMap();
            CreateMap<MaintenanceTaskModel, DeleteMaintenanceTaskCommand>().ReverseMap();
            CreateMap<MaintenanceTaskModel, GetAllMaintenanceTaskQuery>().ReverseMap();
            CreateMap<MaintenanceTaskModel, GetMaintenanceTaskByIdQuery>().ReverseMap();
        }
    }
}
