namespace MainteXpert.ErrorCardService.Application.Models.Response
{
    public class ErrorCardCallResponseModel : BaseResponseModel
    {
        public DateTimeModel CallWaitingTime { get; set; }
        public LookupValue ErrorCardCallStatus { get; set; }
        public DateTimeModel CallInterfereTime { get; set; }
        public string Description { get; set; }
        public UserModel CallerOperator { get; set; } = new UserModel();
        public UserModel InterferingOperator { get; set; } = new UserModel();
        public StationCardGroupModel Station { get; set; } = new StationCardGroupModel();
    }
}
