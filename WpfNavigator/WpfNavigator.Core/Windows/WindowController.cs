using System.Windows;
using WpfNavigator.Core.Containers;
using WpfNavigator.Core.Loggers;
using WpfNavigator.Core.Navigation;
using WpfNavigator.Core.ViewResolvers;

namespace WpfNavigator.Core.Windows
{
    internal class WindowController : IWindowController
    {
        private readonly IContainer container;
        private readonly IApplicationSettings applicationSettings;

        public WindowController(IContainer container, IApplicationSettings applicationSettings)
        {
            this.container = container;
            this.applicationSettings = applicationSettings;
        }

        internal INavigationWindow CurrentWindow { get; set; }

        public INavigationWindow CreateWindow(INavigationToken token = null)
        {
            // Container
            var childContainer = this.container.CreateChildContainer();
            childContainer.RegisterInstance(childContainer);

            // Logger
            var logger = childContainer.Resolve<ILogger>();
            childContainer.RegisterInstance(logger);

            // WindowController
            var windowController = childContainer.Resolve<WindowController>();
            childContainer.RegisterInstance<IWindowController>(windowController);

            // ViewResolver
            var viewResolver = childContainer.Resolve<ViewResolver>();
            childContainer.RegisterInstance<IViewResolver>(viewResolver);

            // Window
            var window = childContainer.Resolve<INavigationWindow>();
            this.SetWindowMetada(window, token);
            windowController.CurrentWindow = window;

            if (this.applicationSettings.WindowWidth.HasValue)
                window.Width = this.applicationSettings.WindowWidth.Value;
            if (this.applicationSettings.WindowHeight.HasValue)
                window.Height = this.applicationSettings.WindowHeight.Value;

            // INavigationService
            var navigationService = childContainer.Resolve<NavigationService>();
            childContainer.RegisterInstance<INavigationService>(navigationService);
            window.NavigationService = navigationService;

            return window;
        }

        private void SetWindowMetada(INavigationWindow window, INavigationToken? token)
        {
            if (token is NewWindowNavigationToken newWindowNavigationToken)
            {
                if (newWindowNavigationToken.Width.HasValue)
                    window.Width = newWindowNavigationToken.Width.Value;

                if (newWindowNavigationToken.Height.HasValue)
                    window.Height = newWindowNavigationToken.Height.Value;

                if (newWindowNavigationToken.WindowStartupLocation.HasValue)
                    window.WindowStartupLocation = newWindowNavigationToken.WindowStartupLocation.Value;

                if (newWindowNavigationToken.SizeToContent.HasValue)
                    window.SizeToContent = newWindowNavigationToken.SizeToContent.Value;

                if (newWindowNavigationToken.Owner != null)
                    window.Owner = newWindowNavigationToken.Owner;

                if (newWindowNavigationToken.CurrentWindowAsOwner)
                    window.Owner = (Window)this.CurrentWindow;
            }
        }

        public INavigationWindow GetWidow(INavigationToken token)
        {
            if (token is NewWindowNavigationToken newWindowNavigationToken)
                return this.CreateWindow(token);

            return this.CurrentWindow;
        }
    }
}
