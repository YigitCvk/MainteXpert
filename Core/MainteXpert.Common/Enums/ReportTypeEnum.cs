namespace MainteXpert.Common.Enums
{
    public enum ReportTypeEnum
    {
        [Display(Name = "-", Description = "-")]
        None = 0,

        [Display(Name = "Activity", Description = "Aktivite")]
        Activity = 1,

        [Display(Name = "ErrorCard", Description = "Hata kartı")]
        ErrorCard = 2,

        [Display(Name = "ErrorCardCall", Description = "Acil çağrı")]
        ErrorCardCall = 3,

    }
}
