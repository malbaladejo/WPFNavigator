using Unity;
using WpfNavigator.Core.Containers;

namespace WpfNavigator.Bootstrapper
{
    internal class UnityContainerWrapper : IContainer
    {
        private readonly IUnityContainer container = new UnityContainer();

        public UnityContainerWrapper(IUnityContainer? container = null)
        {
            this.container = container ?? new UnityContainer();
        }

        public IContainer CreateChildContainer()
            => new UnityContainerWrapper(this.container.CreateChildContainer());

        public void Register<TSource, TTarget>() where TTarget : TSource
            => this.container.RegisterType<TSource, TTarget>();

        public void Register<TSource, TTarget>(string name) where TTarget : TSource
            => this.container.RegisterType<TSource, TTarget>(name);

        public void Register(Type source, Type target)
            => this.container.RegisterType(source, target);

        public void Register(Type source, Type target, string name)
            => this.container.RegisterType(source, target, name);

        public void RegisterInstance<TSource>(TSource source)
            => this.container.RegisterInstance(source);

        public void RegisterSingleton<TSource, TTarget>() where TTarget : TSource
            => this.container.RegisterSingleton<TSource, TTarget>();

        public void RegisterSingleton(Type source, Type target)
            => this.container.RegisterSingleton(source, target);

        public T Resolve<T>() => this.container.Resolve<T>();

        public object Resolve(Type type) => this.container.Resolve(type);
    }
}
