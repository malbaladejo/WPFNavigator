using System.Windows;
using WpfNavigator.Bootstrapper;
using WpfNavigator.Demo.Views.Home;

namespace WpfNavigator.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var bootstrapper = new WpfNavigatorBootstrapper();

            bootstrapper.Initialize(new HomeNavigationToken());
        }
    }
}
