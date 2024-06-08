namespace MainteXpert.Common.Enums
{
    public enum ErrorCardCallAddDropEnum
    {
        [Display(Name = "-", Description = "-")]
        None = 0,

        [Display(Name = "Başlat", Description = "Çağrı işleme alındı")]
        Add = 1,

        [Display(Name = "Bitir", Description = "Çağrı kapatıldı")]
        Complete = 2,

        [Display(Name = "Bırak", Description = "Çağrı bırakıldı")]
        Drop = 3,
    }
}
