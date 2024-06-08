namespace MainteXpert.Common.Models.Lookup.PlanGroupCollectionModels
{
    public class ControlTypeGroupModel : BaseResponseModel
    {
        public string ControlName { get; set; }

        public LookupValue ControlType { get; set; }
    }
}
