namespace DataShareHub.Extensions
{
    public static class CommonExtension
    {
        public static string GetDescription(this Type type, string propertyName = "")
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                var descriptionAttr = type.GetCustomAttribute<DescriptionAttribute>();
                return descriptionAttr?.Description ?? "";
            }
            else
            {
                var property = type.GetProperty(propertyName);
                var descriptionAttr = property.GetCustomAttribute<DescriptionAttribute>();
                return descriptionAttr?.Description ?? "";
            }
        }
    }
}
