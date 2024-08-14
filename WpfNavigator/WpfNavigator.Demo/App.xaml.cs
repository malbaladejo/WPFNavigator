using System.Windows;
using WpfNavigator.Bootstrapper;

namespace WpfNavigator.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += this.App_Startup;

        }

        private async void App_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new WpfNavigatorBootstrapper();

            await bootstrapper.Build()
                 .RegisterModule<MainModule>()
                 .DefaultWindowSize(1024, 600)
                 .RunAsync();
        }
    }
}
