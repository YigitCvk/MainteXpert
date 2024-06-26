namespace MainteXpert.EquipmentService.Application.Mediator.Handler
{
    public class CheckStockHandler : IRequestHandler<CheckStockCommand, ResponseModel<EquipmentStockModel>>
    {
        private readonly IMongoRepository<EquipmentCollection> _collection;
        private readonly IMapper _mapper;

        public CheckStockHandler(IMongoRepository<EquipmentCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel<EquipmentStockModel>> Handle(CheckStockCommand request, CancellationToken cancellationToken)
        {
            var equipment = await _collection.FindByIdAsync(request.EquipmentId);

            if (equipment == null)
            {
                return new ResponseModel<EquipmentStockModel>(null, "Equipment not found");
            }

            var stockModel = new EquipmentStockModel
            {
                EquipmentId = equipment.Id,
                AvailableStock = equipment.StockQuantity, // Assuming you have a StockQuantity property
                IsStockSufficient = equipment.StockQuantity >= request.RequiredQuantity
            };

            return new ResponseModel<EquipmentStockModel>(stockModel);
        }
    }
}
