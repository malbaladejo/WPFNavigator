using WpfNavigator.Core.Containers;
using WpfNavigator.Core.Loggers;
using WpfNavigator.Core.Navigation;
using WpfNavigator.Core.ViewResolvers;
using WpfNavigator.Core.Windows;

namespace WpfNavigator.Core
{
    public abstract class WpfNavigatorBoostraperCore
    {
        private IContainer container;

        protected abstract IContainer CreateContainer();

        protected abstract ILogger CreateLogger();

        protected virtual INavigatableWindow CreateNavigatableWindow()
            => this.container.Resolve<IWindowController>().CreateWindow();

        protected virtual void RegisterNavigatableWindow()
            => this.container.Register<INavigatableWindow, NavigatableWindow>();

        public void Initialize(INavigationToken homeNavigationToken)
        {
            // container
            this.container = this.CreateContainer();
            this.container.RegisterInstance(this.container);

            // logger
            this.container.RegisterInstance(this.CreateLogger());

            // register mandatory impl
            this.RegisterNavigatableWindow();

            this.container.Register<IWindowController, WindowController>();
            this.container.Register<INavigationService, NavigationService>();
            this.container.RegisterSingleton<INavigationContainer, NavigationContainer>();
            this.container.Register<IViewResolver, ViewResolver>();

            // window
            var window = this.CreateNavigatableWindow();
            window.Show();

            window.NavigateAsync(homeNavigationToken);
        }
    }
}
