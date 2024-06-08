namespace MainteXpert.Common.Enums
{
    public enum UserTypeEnum
    {
        [Display(Name = "-", Description = "-")]
        None = 0,

        [Display(Name = "Operatör", Description = "Operatör")]
        Operator = 1,

        [Display(Name = "Teknisyen", Description = "Teknisyen")]
        Technician = 2,

        [Display(Name = "Müdür/Şef", Description = "Müdür/Şef")]
        Manager = 3,

        [Display(Name = "Admin", Description = "Admin")]
        Admin = 4,


    }
}
