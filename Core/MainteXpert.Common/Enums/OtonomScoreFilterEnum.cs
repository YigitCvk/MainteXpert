namespace MainteXpert.Common.Enums
{
    public enum OtonomScoreFilterEnum
    {
        [Display(Name = "-", Description = "-")]
        None = 0,

        [Display(Name = "Is empty", Description = "Boşta")]
        IsEmpty = 1,

        [Display(Name = "Is with errorCard", Description = "Hata kartı olanlar")]
        IsWithErrorCard = 2,

        [Display(Name = "Is with error card call", Description = "Acil çağrı açılanlar")]
        IsWithErrorCardCall = 3,

        [Display(Name = "Is completed", Description = "Tamamlananlar")]
        IsCompleted = 4,

        [Display(Name = "Not Completed", Description = "Tamamlanmayanlar")]
        NotComplated = 5,


    }
}
