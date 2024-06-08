namespace MainteXpert.Common.Enums
{
    public enum ErrorCardStatus
    {
        [Display(Name = "-", Description = "-")]
        None = 0,

        [Display(Name = "Açık", Description = "Hata kartı açık")]
        Opened = 1,

        [Display(Name = "Müdahale", Description = "Müdahalede")]
        InProccess = 2,

        [Display(Name = "Kapalı", Description = "Hata kapandı")]
        Closed = 3
    }
}
