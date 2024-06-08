namespace MainteXpert.Common.Enums
{
    public enum ErrorCardCallStatusEnum
    {
        [Display(Name = "-", Description = "-")]
        None = 0,

        [Display(Name = "Çağrı Açık", Description = "Müdahele için mevcut çağrı bulunmaktadır")]
        Opened = 1,

        [Display(Name = "Çağrı Müdahelede", Description = "Teknisyen çağrı üzerinde çalışıyor")]
        Proccessing = 2,

        [Display(Name = "Çağrı Kapatıldı", Description = "Çağrı kapatıldı")]
        Closed = 3,
    }
}
