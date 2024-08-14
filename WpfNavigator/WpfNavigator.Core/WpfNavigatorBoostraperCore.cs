using WpfNavigator.Core.Containers;
using WpfNavigator.Core.Controls;
using WpfNavigator.Core.Loggers;
using WpfNavigator.Core.Modules;
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

        public WpfNavigatorBoostraperCore RegisterStartupToken<TToken>(TToken token) where TToken : INavigationToken
        {
            this.container.Resolve<IApplicationSettings>().RegisterStartupToken(token);
            return this;
        }

        public WpfNavigatorBoostraperCore DefaultWindowSize(double width, double height)
        {
            this.container.Resolve<IApplicationSettings>().DefaultWindowSize(width, height);
            return this;
        }

        public WpfNavigatorBoostraperCore RegisterModule<TModule>() where TModule : IModule
        {
            this.container.Resolve<IApplicationSettings>().RegisterModule<TModule>();
            return this;
        }

        public WpfNavigatorBoostraperCore Build()
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
            this.container.RegisterSingleton<IApplicationSettings, ApplicationSettings>();
            this.container.RegisterSingleton<INavigatedToStrategy, NavigatedToStrategy>();
            this.container.RegisterSingleton<INavigatedFromStrategy, NavigatedFromStrategy>();

            return this;
        }

        public async Task RunAsync()
        {
            await InitializeModulesAsync();

            CreateWindow();
        }

        private void CreateWindow()
        {
            var window = this.CreateNavigationWindow();
            window.Show();

            var startupToken = this.container.Resolve<IApplicationSettings>().StartupToken;

            if (startupToken != null)
                window.NavigateAsync(startupToken);
        }

        private async Task InitializeModulesAsync()
        {
            foreach (var moduleType in this.container.Resolve<IApplicationSettings>().ModuleTypes)
            {
                try
                {
                    Logger.LogInformation($"Initialize module {moduleType.FullName}");
                    var module = (IModule)this.container.Resolve(moduleType);
                    await module.InitializeAsync();
                    Logger.LogInformation($"Module {module.GetType().FullName} initialized");
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Module {moduleType.FullName} in error", ex);
                }
            }
        }

        private ILogger Logger => this.container.Resolve<ILogger>();
    }
}
