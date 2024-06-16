

namespace MainteXpert.ErrorCardService.Application.Validation
{

    public class InsertErrorCardCallValidator : AbstractValidator<InsertErrorCardCallCommand>
    {
        private readonly IMongoRepository<ErrorCardCallCollection> _collection;
        private readonly IMongoRepository<StationCardGroupCollection> _stationCardCollection;

        public InsertErrorCardCallValidator(
            IMongoRepository<ErrorCardCallCollection> collection,
            IMongoRepository<StationCardGroupCollection> stationCardCollection)
        {
            _collection = collection;
            _stationCardCollection = stationCardCollection;

            RuleFor(x => x.StationCardId)
                .Must(CheckStationCard).WithMessage($"Id ile ilişkili istasyon kartı bulunamadı");

            RuleFor(x => x.StationCardId)
                .Must(CheckHasActiveCall).WithMessage($"İlgili istasyonda mevcut bir çağrı bulunmaktadır");

        }

        private bool CheckStationCard(string stationCardId)
        {
            var result = _stationCardCollection.AsQueryable().Any(x => x.Id.Equals(stationCardId));
            return result;
        }

        private bool CheckHasActiveCall(string stationCardId)
        {
            var result = _collection.AsQueryable().Any(x => x.Station.Id.Equals(stationCardId)
                                    && x.Status != Common.Enums.DocumentStatus.Deleted
                                    && x.ErrorCardCallStatus != Common.Enums.ErrorCardCallStatusEnum.Closed);
            return !result;
        }


    }
}
