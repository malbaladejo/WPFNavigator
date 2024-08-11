using WpfNavigator.Core.Containers;
using WpfNavigator.Core.Loggers;
using WpfNavigator.Core.Navigation;
using WpfNavigator.Core.Windows;

namespace WpfNavigator.Core
{
    public abstract class WpfNavigatorBoostraperCore
    {
        private IContainer container;

        protected abstract IContainer CreateContainer();

        protected abstract ILogger CreateLogger();

        protected virtual INavigatableWindow CreateNavigatableWindow()
            => this.container.Resolve<WindowController>().CreateWindow();

        protected virtual void RegisterNavigatableWindow()
            => this.container.Register<INavigatableWindow, NavigatableWindow>();

        private INavigatableWindow startupToken;

        public WpfNavigatorBoostraperCore RegisterStartupToken<TToken>(TToken token) where TToken : INavigatableWindow
        {
            this.startupToken = token;
            return this;
        }

        public void Run()
        {
            // container
            this.container = this.CreateContainer();
            this.container.RegisterInstance(this.container);

            // logger
            this.container.RegisterInstance(this.CreateLogger());

            // register mandatory impl
            this.RegisterNavigatableWindow();

            this.container.Register<IWindowController, WindowController>();
            //this.container.Register<INavigationService, NavigationService>();
            this.container.RegisterSingleton<INavigationContainer, NavigationContainer>();
            //this.container.Register<IViewResolver, ViewResolver>();

            // window
            var window = this.CreateNavigatableWindow();
            window.Show();

            if (this.startupToken != null)
            {
                window.NavigateAsync(this.startupToken);
            }
        }
    }
}
