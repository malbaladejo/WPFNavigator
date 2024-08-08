using WpfNavigator.Core.Containers;
using WpfNavigator.Core.Loggers;
using WpfNavigator.Core.Navigation;
using WpfNavigator.Core.ViewResolvers;

namespace WpfNavigator.Core.Windows
{
    internal class WindowController : IWindowController
    {
        private readonly IContainer container;

        public WindowController(IContainer container)
        {
            this.container = container;
        }

        internal INavigatableWindow CurrentWindow { get; set; }

        public INavigatableWindow CreateWindow()
        {
            var childContainer = this.container.CreateChildContainer();
            childContainer.RegisterInstance(childContainer);

            var window = this.container.Resolve<INavigatableWindow>();
            window.ViewResolver = childContainer.Resolve<IViewResolver>();

            var windowController = childContainer.Resolve<WindowController>();
            windowController.CurrentWindow = window;
            childContainer.RegisterInstance<IWindowController>(windowController);

            var navigationService = childContainer.Resolve<INavigationService>();
            childContainer.RegisterInstance(navigationService);

            var logger = childContainer.Resolve<ILogger>();
            window.Logger = logger;
            childContainer.RegisterInstance(logger);

            return window;
        }

        public INavigatableWindow GetWidow(INavigationToken token)
        {
            if (token is NewWindowNavigationToken newWindowNavigationToken)
                return this.CreateWindow();

            return this.CurrentWindow;
        }
    }
}
