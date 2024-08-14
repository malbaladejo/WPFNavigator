using WpfNavigator.Core.Navigation;

namespace WpfNavigator.Core.Controls
{
    internal interface INavigatedFromStrategy
    {
        Task OnNavigatedFromAsync(INavigationToken token, NavigationControl control);
    }
}