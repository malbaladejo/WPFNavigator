using WpfNavigator.Core.Navigation;

namespace WpfNavigator.Demo.Views.ViewA
{
    [Navigatable(typeof(ViewAView), typeof(ViewAViewModel))]
    internal class ViewANavigationToken : INavigationToken
    {
        public ViewANavigationToken(string label = "View A")
        {
            this.Label = label;
        }

        public string Icon => string.Empty;

        public string Label { get; }
    }
}
