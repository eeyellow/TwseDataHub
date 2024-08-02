namespace DataShareHub.Extensions
{
    /// <summary> DI擴充 </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="target"></param>
        public static void Inject(this IServiceProvider provider, object target)
        {
            var type = target.GetType();
            var props = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (prop.GetCustomAttribute<InjectAttribute>() != null && prop.SetMethod != null)
                {
                    prop.SetValue(target, provider.GetRequiredService(prop.PropertyType));
                }
            }

            var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                if (field.GetCustomAttribute<InjectAttribute>() != null)
                {
                    field.SetValue(target, provider.GetRequiredService(field.FieldType));
                }
            }
        }
    }
}
