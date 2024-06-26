
namespace MaineXpert.EquipmentService.Application.Mediator.Handler
{
    public class GetAllEquipmentsHandler : IRequestHandler<GetAllEquipmentsQuery, ResponseModel<List<EquipmentModel>>>
    {
        private readonly IMongoRepository<EquipmentCollection> _collection;
        private readonly IMapper _mapper;

        public GetAllEquipmentsHandler(IMongoRepository<EquipmentCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<EquipmentModel>>> Handle(GetAllEquipmentsQuery request, CancellationToken cancellationToken)
        {
            var equipments = await _collection.GetAll();
            var equipmentModels = _mapper.Map<List<EquipmentModel>>(equipments);

            return new ResponseModel<List<EquipmentModel>>(equipmentModels);
        }
    }
}
