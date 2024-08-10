namespace WpfNavigator.Core.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetGenericTypeDefinition<TType>(this Type type)
            => type.GetInterfaces().Where(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(TType));

        public static IEnumerable<Type> GetGenericTypeDefinition(this Type type, Type targetType)
           => type.GetInterfaces().Where(x =>
                   x.IsGenericType &&
                   x.GetGenericTypeDefinition() == targetType);

        public static IEnumerable<Type> GetGenericTypeDefinition<TType>(this object item)
           => item.GetType().GetGenericTypeDefinition<TType>();

        public static IEnumerable<Type> GetGenericTypeDefinition(this object item, Type targetType)
           => item.GetType().GetGenericTypeDefinition(targetType);
    }
}
