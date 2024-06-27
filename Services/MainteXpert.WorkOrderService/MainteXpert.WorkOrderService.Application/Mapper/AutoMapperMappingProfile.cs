using MainteXpert.Common.Models.WorkOrder;
using MainteXpert.Repository.Collections.WorkOrder;

namespace MainteXpert.WorkOrderService.Application.Mapper
{
    public class AutoMapperMappingProfile : Profile
    {

        public AutoMapperMappingProfile()
        {
            CreateMap<WorkOrderCollection, WorkOrderModel>().ReverseMap();

        }
    }
}
