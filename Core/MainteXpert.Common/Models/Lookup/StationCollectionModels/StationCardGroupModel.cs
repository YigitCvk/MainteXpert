namespace MainteXpert.Common.Models.Lookup.StationCollectionModels
{
    public class StationCardGroupModel : BaseResponseModel
    {
        public FactoryGroupModel FactoryGroupModel { get; set; }
        public StationCodeGroupModel StationCodeGroupModel { get; set; }
        public StationNameGroupModel StationNameGroupModel { get; set; }
        public StationGroupModel StationGroupModel { get; set; }
    }
}
