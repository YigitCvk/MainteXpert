namespace MainteXpert.Common.Extensions
{
    public static class DisplayAttributeExtensions
    {
        public static DisplayAttribute GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>();
        }
    }
}
