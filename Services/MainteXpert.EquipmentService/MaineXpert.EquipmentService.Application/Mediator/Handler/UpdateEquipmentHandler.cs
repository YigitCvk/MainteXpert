using MaineXpert.EquipmentService.Application.Mediator.Commands;
using MainteXpert.MaintenanceSchedule.Application.Models;
using MongoDB.Driver;

namespace MaineXpert.EquipmentService.Application.Mediator.Handler
{
    public class UpdateEquipmentHandler : IRequestHandler<UpdateEquipmentCommand, ResponseModel<EquipmentModel>>
    {
        private readonly IMongoRepository<EquipmentCollection> _collection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateEquipmentHandler(IMongoRepository<EquipmentCollection> collection, IMediator mediator, IMapper mapper)
        {
            _collection = collection;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ResponseModel<EquipmentModel>> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<EquipmentCollection>.Filter.Eq(task => task.Id, request.Id);
            var update = Builders<EquipmentCollection>.Update
                .Set(task => task.Name, request.Name)
                .Set(task => task.Description, request.Description)
                .Set(task => task.Status, request.Status)
                .Set(task => task.PurchaseDate, request.PurchaseDate);
            var result = await _collection.GetCollection().UpdateOneAsync(filter, update, null, cancellationToken);

            if (result.MatchedCount != null)
            {
                return new ResponseModel<EquipmentModel>(null, "Task not found");
            }

            var equipment = await _collection.GetCollection().Find(filter).FirstOrDefaultAsync(cancellationToken);
            var equipmentModel = _mapper.Map<EquipmentModel>(equipment);
            return new ResponseModel<EquipmentModel>(equipmentModel);
        }
    }
}
