namespace MainteXpert.Helper.EnumHelper
{
    public static class EnumHelper
    {
        private static ResourceManager _resource;
        /// <summary>
        /// Enum'ın [Display] attribute ile tanımlı ismini döner.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static DisplayAttribute GetDisplayName(this System.Enum enumValue)
        {
            var displayAttribute = enumValue.GetType().GetTypeInfo().GetMember(enumValue.ToString()).FirstOrDefault()?.GetCustomAttribute<DisplayAttribute>();
            if (displayAttribute != null && displayAttribute.ResourceType != null)
                _resource = new ResourceManager(displayAttribute.ResourceType);
            return displayAttribute;
        }
        public static string Convert(this string value)
        {
            if (_resource == null) return value;
            string displayName = _resource.GetString(value);
            return string.IsNullOrEmpty(displayName)
                ? value
                : displayName;
        }
        public static string GetDescription<T>(this T enumerationValue)
            where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }
            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }
    }
}
