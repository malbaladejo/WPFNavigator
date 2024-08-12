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

        protected virtual INavigationWindow CreateNavigationWindow()
            => this.container.Resolve<WindowController>().CreateWindow();

        protected virtual void RegisterNavigationWindow()
            => this.container.Register<INavigationWindow, NavigationWindow>();

        private INavigationToken startupToken;

        public WpfNavigatorBoostraperCore RegisterStartupToken<TToken>(TToken token) where TToken : INavigationToken
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
            this.RegisterNavigationWindow();

            this.container.Register<IWindowController, WindowController>();
            this.container.RegisterSingleton<INavigationContainer, NavigationContainer>();

            // window
            var window = this.CreateNavigationWindow();
            window.Show();

            if (this.startupToken != null)
            {
                window.NavigateAsync(this.startupToken);
            }
        }
    }
}
