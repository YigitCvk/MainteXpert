namespace MainteXpert.Common.Enums
{
    public enum ControlTypeEnum
    {
        [Display(Name = "-", Description = "-")]
        None = 0,

        [Display(Name = "Değer", Description = "Değer")]
        Value = 1,

        [Display(Name = "Ok/Nok", Description = "Ok/Nok")]
        Ok_Nok = 2,

        [Display(Name = "Açıklama", Description = "Açıklama")]
        Description = 3,

    }
}
