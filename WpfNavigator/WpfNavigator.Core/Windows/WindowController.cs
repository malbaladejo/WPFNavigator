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
            var window = childContainer.Resolve<INavigatableWindow>();

            windowController.CurrentWindow = window;

            // INavigationService
            var navigationService = childContainer.Resolve<NavigationService>();
            childContainer.RegisterInstance<INavigationService>(navigationService);

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
