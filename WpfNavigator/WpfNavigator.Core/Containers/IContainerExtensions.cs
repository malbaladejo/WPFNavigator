namespace WpfNavigator.Core.Containers
{
    public static class IContainerExtensions
    {
        public static void RegisterWithName<TSource, TTarget>(this IContainer container) where TTarget : TSource
        {
            if (typeof(TTarget) == null)
                throw new ArgumentNullException("TTarget can not be null;");

#pragma warning disable CS8604 // Possible null reference argument.
            container.Register<TSource, TTarget>(typeof(TTarget).FullName);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public static T Resolve<T>(this IContainer container)
            => (T)container.Resolve(typeof(T));
    }
}
