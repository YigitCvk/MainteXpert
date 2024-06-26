namespace MainteXpert.EquipmentService.Application.Mediator.Handler
{
    public class GetEquipmentByIdHandler : IRequestHandler<GetEquipmentByIdQuery, ResponseModel<EquipmentModel>>
    {
        private readonly IMongoRepository<EquipmentCollection> _collection;
        private readonly IMapper _mapper;

        public GetEquipmentByIdHandler(IMongoRepository<EquipmentCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<EquipmentModel>> Handle(GetEquipmentByIdQuery request, CancellationToken cancellationToken)
        {
            var equipment = await _collection.FindByIdAsync(request.Id);
            if (equipment == null)
            {
                return new ResponseModel<EquipmentModel>(null, "Equipment not found");
            }

            var equipmentModel = _mapper.Map<EquipmentModel>(equipment);
            return new ResponseModel<EquipmentModel>(equipmentModel);
        }
    }
}
