using WpfNavigator.Core.Navigation;

namespace WpfNavigator.Demo.Views.ViewB
{
    [Navigatable(typeof(ViewBView), typeof(ViewBViewModel))]
    internal class ViewBNavigationToken : INavigationToken
    {
        public ViewBNavigationToken(string label = "View B")
        {
            this.Label = label;
        }

        public string Icon => string.Empty;

        public string Label { get; }
    }
}
