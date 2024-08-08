using WpfNavigator.Core.Navigation;

namespace WpfNavigator.Demo.Views.Home
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    internal partial class HomeView
    {
        public HomeView()
        {
            InitializeComponent();
        }
    }

    internal class HomeViewModel
    {

    }

    [Navigatable(typeof(HomeView), typeof(HomeViewModel))]
    internal class HomeNavigationToken : INavigationToken
    {
        public string Icon => string.Empty;

        public string Label => "Home";
    }
}
