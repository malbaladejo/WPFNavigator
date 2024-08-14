using System.Windows;
using WpfNavigator.Core.Extensions;
using WpfNavigator.Core.Loggers;
using WpfNavigator.Core.Navigation;

namespace WpfNavigator.Core.Controls
{
    internal class NavigatedFromStrategy : INavigatedFromStrategy
    {
        private readonly ILogger logger;

        public NavigatedFromStrategy(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task OnNavigatedFromAsync(INavigationToken token, NavigationControl control)
        {
            var children = control.FindMyselfAndVisualChildren<FrameworkElement>().Reverse();

            foreach (var child in children)
            {
                var viewModel = child.DataContext;

                if (viewModel is INavigationAware<INavigationToken> viewModelNavigationAware)
                {
                    this.logger.LogInformation($"{viewModel.GetType()} is INavigationAware: Calling OnNavigatedTo.");
                    viewModelNavigationAware.OnNavigatedFrom(token);
                }
                else if (viewModel is INavigationAsyncAware<INavigationToken> viewModelNavigationAsyncAware)
                {
                    this.logger.LogInformation($"{viewModel.GetType()} is INavigationAsyncAware: Calling OnNavigatedToAsync.");
                    await viewModelNavigationAsyncAware.OnNavigatedFromAsync(token);
                }
            }
        }
    }
}
