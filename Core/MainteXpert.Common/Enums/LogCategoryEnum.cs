namespace MainteXpert.Common.Enums
{
    public enum LogCategoryEnum
    {
        [Display(Name = "Default", Description = "Default")]
        None = 0,

        [Display(Name = "Insert", Description = "Insert")]
        Insert = 1,

        [Display(Name = "Update", Description = "Update")]
        Update = 2,

        [Display(Name = "Delete", Description = "Delete")]
        Delete = 3,

        [Display(Name = "ActivityUpdateJobNotify", Description = "ActivityUpdateJobNotify")]
        ActivityUpdateJobNotify = 4,


    }
}
