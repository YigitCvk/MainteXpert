namespace MainteXpert.Common.Enums
{
    public enum LogTypeEnum
    {
        [Display(Name = "Default", Description = "Default")]
        None = 0,

        [Display(Name = "Error", Description = "Error")]
        Error = 1,

        [Display(Name = "Warning", Description = "Warning")]
        Warning = 2,

        [Display(Name = "Debug", Description = "Debug")]
        Debug = 3,

        [Display(Name = "Audit", Description = "Audit")]
        Audit = 4,

        [Display(Name = "Information", Description = "Information")]
        Information = 5,
    }
}
