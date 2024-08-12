using WpfNavigator.Core.Navigation;

namespace WpfNavigator.Core.Windows
{
    public interface IWindowController
    {
        INavigationWindow GetWidow(INavigationToken token);

        INavigationWindow CreateWindow(INavigationToken token = null);
    }
}
