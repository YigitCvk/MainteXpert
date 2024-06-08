namespace MainteXpert.Common.Enums
{
    public enum ActivityStatus
    {
        [Display(Name = "-", Description = "-")]
        None = 0,

        [Display(Name = "Aktivite hazır", Description = "Aktivite üzerinde çalışan kimse yok")]
        Ready = 1,

        [Display(Name = "Aktivite işleniyor", Description = "Aktivite üzerinde çalışılıyor")]
        Proccessing = 2,

        [Display(Name = "Aktivite bekleniyor", Description = "Aktivite zamanı bekleniyor")]
        Pending = 3,

        [Display(Name = "Aktivite Tamamlandı", Description = "Aktivite sonlandırıldı")]
        Complated = 4,

        [Display(Name = "Hata", Description = "Aktivitede hata ile karşılaşıldı")]
        Error = 5,

        [Display(Name = "Aktivite önerildi", Description = "Aktivite onay bekliyor")]
        Suggested = 6,

        [Display(Name = "Aktiivite reddedildi", Description = "Aktiivite önerisi reddedildi")]
        Rejected = 7,

        [Display(Name = "Aktiivite onaylandı", Description = "Aktiivite önerisi onaylandı")]
        Approved = 8,

        [Display(Name = "Sistem tarafından kapatıldı", Description = "Aktivite zaman aşımına uğradı ve sistem tarafından kapatıldı")]
        ActivityTimeOut = 9,

        [Display(Name = "Aktivite üzerinde işlem yapılmadı", Description = "Aktivite üzerinde çalışan kimse olmadığı için kapatıldı")]
        ActivityNonProcessing = 10,
    }
}
