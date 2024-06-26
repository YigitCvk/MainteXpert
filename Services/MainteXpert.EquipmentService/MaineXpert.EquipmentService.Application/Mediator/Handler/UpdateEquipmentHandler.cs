namespace MainteXpert.EquipmentService.Application.Mediator.Handler
{
    public class UpdateEquipmentHandler : IRequestHandler<UpdateEquipmentCommand, ResponseModel<EquipmentModel>>
    {
        private readonly IMongoRepository<EquipmentCollection> _repository;
        private readonly IMapper _mapper;

        public UpdateEquipmentHandler(IMongoRepository<EquipmentCollection> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<EquipmentModel>> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
        {
            var equipment = await _repository.FindByIdAsync(request.Id);
            if (equipment == null)
            {
                return new ResponseModel<EquipmentModel>(null, "Equipment not found");
            }

            _mapper.Map(request, equipment);
            await _repository.ReplaceOneAsync(equipment);

            var equipmentModel = _mapper.Map<EquipmentModel>(equipment);
            return new ResponseModel<EquipmentModel>(equipmentModel);
        }
    }
}
