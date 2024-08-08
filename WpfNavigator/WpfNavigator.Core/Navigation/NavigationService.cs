using WpfNavigator.Core.Windows;

namespace WpfNavigator.Core.Navigation
{
    internal class NavigationService : INavigationService
    {
        private readonly IWindowController windowController;

        public NavigationService(IWindowController windowController)
        {
            this.windowController = windowController;
        }

        public async Task NavigateAsync(INavigationToken token)
        {
            var window = this.windowController.GetWidow(token);

            if (token is NewWindowNavigationToken newWindowNavigationToken)
                await window.NavigateAsync(newWindowNavigationToken.Target);

            await window.NavigateAsync(token);
        }
    }
}
