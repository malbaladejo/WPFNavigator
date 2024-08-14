using WpfNavigator.Core;
using WpfNavigator.Core.Modules;
using WpfNavigator.Demo.Views.Home;

namespace WpfNavigator.Demo
{
    internal class MainModule : IModule
    {
        private readonly IApplicationSettings applicationSettings;

        public MainModule(IApplicationSettings applicationSettings)
        {
            this.applicationSettings = applicationSettings;
        }

        public async Task InitializeAsync()
        {
            this.applicationSettings.RegisterStartupToken(new HomeNavigationToken());
        }
    }
}
