using WpfNavigator.Core.Navigation;

namespace WpfNavigator.Demo.Views.Home
{
    [Navigatable(typeof(HomeView), typeof(HomeViewModel))]
    internal class HomeNavigationToken : INavigationToken
    {
        private static int index = 0;
        public HomeNavigationToken(string label = "Home")
        {
            index++;
            this.Label = $"{label} {index}";
        }

        public string Icon => string.Empty;

        public string Label { get; }
    }
}
