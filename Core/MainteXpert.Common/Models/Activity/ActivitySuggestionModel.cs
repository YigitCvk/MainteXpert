namespace MainteXpert.Common.Models.Activity
{
    public class ActivitySuggestionModel : BaseResponseModel
    {
        public string ActivitySuggestionName { get; set; }
        public string Station { get; set; }
        public string RecommenderName { get; set; }
    }
}
