namespace MainteXpert.Common.Enums
{
    public enum ReportFilterEnum
    {
        [Display(Name = "-", Description = "-")]
        None = 0,

        [Display(Name = "Operator", Description = "Operatör")]
        Operator = 1,

        [Display(Name = "Station name group", Description = "İstasyon isim grubu")]
        StationGroup = 2,

        [Display(Name = "Station Code", Description = "İstasyon kodu")]
        StationCode = 3,

        [Display(Name = "Activity name", Description = "Aktivite ismi")]
        ActivityName = 4,

        [Display(Name = "Activity group name", Description = "Aktivite grup ismi")]
        ActivityGroupName = 5,

        [Display(Name = "Control type", Description = "Kontrol tipi")]
        ControlType = 6,

        [Display(Name = "Otonom score", Description = "Otonom skoru")]
        OtonomScore = 7,

        [Display(Name = "Is complated", Description = "Tamamlanma")]
        IsComplated = 8,

        [Display(Name = "Technician", Description = "Teknisyen")]
        Technician = 9,

        [Display(Name = "Error card", Description = "Hata kartı")]
        ErrorCard = 10,

        [Display(Name = "Description", Description = "Açıklama")]
        Description = 11,

        [Display(Name = "Activity working total time", Description = "Aktivite toplam çalışma süresi")]
        ActivityWorkingTime = 12,

        [Display(Name = "Priority", Description = "Önem derecesi")]
        Priority = 13,

        [Display(Name = "Activity created date", Description = "Aktivite oluşturulma zamanı")]
        ActivityCreatedDate = 14,

        [Display(Name = "Activity status", Description = "Aktivite durumu")]
        ActivityStatus = 15,
    }
}
