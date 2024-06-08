namespace MainteXpert.Common.Enums
{
    public enum PeriodTypeEnum
    {
        [Display(Name = "-", Description = "-")]
        None = 0,

        [Display(Name = "Saat", Description = "Saatlik")]
        Hour = 1,

        [Display(Name = "Gün", Description = "Günlük")]
        Daily = 2,

        [Display(Name = "Hafta", Description = "Haftalık")]
        Weekly = 3,

        [Display(Name = "Ay", Description = "Aylık")]
        Monthly = 4,

        [Display(Name = "Yıl", Description = "Yıllık")]
        Yearly = 5,

        [Display(Name = "Vardiya", Description = "Vardiya")]
        Shifting = 6,

    }
}
