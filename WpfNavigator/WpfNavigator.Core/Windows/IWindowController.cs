using WpfNavigator.Core.Navigation;

namespace WpfNavigator.Core.Windows
{
    public interface IWindowController
    {
        INavigatableWindow GetWidow(INavigationToken token);

        INavigatableWindow CreateWindow();
    }
}
