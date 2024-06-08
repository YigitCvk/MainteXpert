namespace MainteXpert.Common.Enums
{
    public enum ErrorCardAddDropEnum
    {
        [Display(Name = "-", Description = "-")]
        None = 0,

        [Display(Name = "Başlat", Description = "Hata kartı işleme alındı")]
        Add = 1,

        [Display(Name = "Bitir", Description = "Hata kartı kapatıldı")]
        Complete = 2,

        [Display(Name = "Bırak", Description = "Hata kartı bırakıldı")]
        Drop = 3,




    }
}
