namespace WpfNavigator.Core.Containers
{
    public interface IContainer
    {
        void Register<TSource, TTarget>() where TTarget : TSource;
        void Register<TSource, TTarget>(string name) where TTarget : TSource;

        void Register(Type source, Type target);
        void Register(Type source, Type target, string name);

        void RegisterSingleton<TSource, TTarget>() where TTarget : TSource;
        void RegisterSingleton(Type source, Type target);

        void RegisterInstance<TSource>(TSource source);

        T Resolve<T>();
        object Resolve(Type type);

        IContainer CreateChildContainer();
    }
}
