namespace MainteXpert.Common.Enums
{
    public enum ActivityAddDropEnum
    {
        [Display(Name = "-", Description = "-")]
        None = 0,

        [Display(Name = "Ekle", Description = "Ekle")]
        Add = 1,

        [Display(Name = "Bitir", Description = "Bitir")]
        Complete = 2,

        [Display(Name = "Bırak", Description = "Bırak")]
        Drop = 3,
    }
}
