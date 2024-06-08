namespace MainteXpert.Common.Enums
{
    public enum PriorityEnum
    {
        [Display(Name = "-", Description = "-")]
        None = 0,

        [Display(Name = "Normal", Description = "1. Seviye")]
        Normal = 1,

        [Display(Name = "Acil", Description = "2. Seviye")]
        Urgent = 2,

        [Display(Name = "Çok acil", Description = "3. Seviye")]
        Immediate = 3,


    }
}
