namespace MainteXpert.Common.Enums
{
    public enum DocumentStatus
    {
        [Display(Name = "-", Description = "-")]
        None = 0,

        [Display(Name = "Oluşturuldu", Description = "Oluşturuldu")]
        Created = 1,

        [Display(Name = "Güncellendi", Description = "Güncellendi")]
        Updated = 2,

        [Display(Name = "Silindi", Description = "Silindi")]
        Deleted = 3,

    }
}
