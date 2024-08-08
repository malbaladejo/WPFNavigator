using System.Windows;

namespace WpfNavigator.Core.Navigation
{
    public interface INavigationContainer
    {
        void Register<TToken, TView, TViewModel>()
            where TToken : INavigationToken
            where TView : FrameworkElement;

        bool IsRegistered(INavigationToken token);

        ViewViewModelContainer Resolve(INavigationToken token);
    }
}
