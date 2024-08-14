using WpfNavigator.Core.Navigation;

namespace WpfNavigator.Core.Controls
{
    internal interface INavigatedToStrategy
    {
        Task OnNavigatedToAsync<TToken>(TToken token, object viewModel) where TToken : INavigationToken;
    }
}