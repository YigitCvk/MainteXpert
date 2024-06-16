
namespace MainteXpert.ErrorCardService.Application.Validation
{
    public class UpdateCurrentTechnicianOnErrorCardValidator : AbstractValidator<UpdateCurrentTechnicianOnErrorCardCommand>
    {
        private readonly IMongoRepository<ErrorCardCollection> _errorCardCollection;
        private readonly IMongoRepository<UserCollection> _userCollection;

        public UpdateCurrentTechnicianOnErrorCardValidator(IMongoRepository<ErrorCardCollection> errorCardCollection, IMongoRepository<UserCollection> userCollection)
        {
            _errorCardCollection = errorCardCollection;
            _userCollection = userCollection;

            RuleFor(x => x.ErrorCardId)
                .Must(CheckErrorCardIsClosed).WithMessage("Kapanmış hata kartı üzerinde çalışılamaz").When(x => x.ErrorCardAddDrop == ErrorCardAddDropEnum.Add);

            RuleFor(x => x.ErrorCardId)
                .Must(CheckId).WithMessage("Id ile ilişkili hata kartı bulunamadı");

            RuleFor(x => x)
               .MustAsync(CheckAnyOperatorIsWorkingOn)
               .WithMessage(x =>
               {
                   string currentWorkerInfoMsg = GetCurrentWorkerInformation(x.ErrorCardId).Result;
                   return $"Bu hata üzerinde {currentWorkerInfoMsg} operatör çalışıyor";
               })
               .When(x => x.ErrorCardAddDrop == ErrorCardAddDropEnum.Add);

            RuleFor(x => x)
                .MustAsync(CheckUserAlreadyWorking)
                .WithMessage(x =>
                {
                    string checkUserAlreadyWorkingMsg = CheckUserAlreadyWorkingMsg().Result;
                    return $"Şuan başka bir hata kartı üzerinde çalışıyorsunuz.{checkUserAlreadyWorkingMsg}";
                })
                .When(x => x.ErrorCardAddDrop == ErrorCardAddDropEnum.Add);


        }

        private async Task<string> GetCurrentWorkerInformation(string errorCardId)
        {
            var errorCard = await _errorCardCollection.GetAsync(x => x.Id.Equals(errorCardId)
                                                    && x.Status != DocumentStatus.Deleted
                                                    && x.ErrorCardStatus != ErrorCardStatus.Closed);
            if (errorCard?.CurrentTechnician == null)
                return string.Empty;
            else
                return $"{errorCard.CurrentTechnician.Name} {errorCard.CurrentTechnician.Surname}";

        }

        private bool CheckErrorCardIsClosed(string id)
        {
            var errorcardStatus = _errorCardCollection.AsQueryable().FirstOrDefault(x => x.Id == id).ErrorCardStatus;
            return errorcardStatus != ErrorCardStatus.Closed;
        }

        private async Task<string> CheckUserAlreadyWorkingMsg()
        {
            var currentUserId = _errorCardCollection.GetUserId();


            var errorCard = await _errorCardCollection.GetAsync(x => x.CurrentTechnician.Id == currentUserId
                                                    && x.Status != DocumentStatus.Deleted
                                                    && x.ErrorCardStatus != ErrorCardStatus.Closed);

            if (errorCard?.CurrentTechnician is null)
            {
                return string.Empty;
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"İstasyon adı: {errorCard.StationCardGroup.StationNameGroupCollection.StationNameGroupName}");
                stringBuilder.AppendLine($"İstasyon grubu: {errorCard.StationCardGroup.StationNameGroupCollection.StationNameGroupName}");
                stringBuilder.AppendLine($"Hata kartı grup adı: {errorCard.ErrorCardGroup.ErrorCardGroupName}");
                stringBuilder.AppendLine($"Hata kartı önceliği: {errorCard.Priority.GetDisplayName().Name}");

                return stringBuilder.ToString();
            }

        }

        private async Task<bool> CheckUserAlreadyWorking(UpdateCurrentTechnicianOnErrorCardCommand command, CancellationToken cts)
        {
            var currentUserId = _errorCardCollection.GetUserId();

            var errorCard = await _errorCardCollection.GetAsync(x => x.CurrentTechnician.Id == currentUserId
                                                    && x.Status != DocumentStatus.Deleted
                                                    && x.ErrorCardStatus != ErrorCardStatus.Closed);

            if (errorCard is null)
                return true;
            else
                return false;

        }

        private bool CheckId(string id)
        {
            var result = _errorCardCollection.AsQueryable().Any(x => x.Id.Equals(id));
            return result;
        }

        private async Task<bool> CheckAnyOperatorIsWorkingOn(UpdateCurrentTechnicianOnErrorCardCommand command, CancellationToken cancellationToken)
        {
            var result = await _errorCardCollection.FindByIdAsync(command.ErrorCardId);
            var currentTechnician = result.CurrentTechnician;
            if (currentTechnician != null)
            {
                var currentUserId = _userCollection.GetUserId();
                if (currentTechnician.Id.Equals(currentUserId))
                    return true;
                else
                    return false;
            }
            else
                return true;

        }


    }
}
