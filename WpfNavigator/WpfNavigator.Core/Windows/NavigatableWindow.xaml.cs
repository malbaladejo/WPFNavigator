using System.Windows;
using WpfNavigator.Core.Loggers;
using WpfNavigator.Core.Navigation;
using WpfNavigator.Core.ViewResolvers;

namespace WpfNavigator.Core.Windows
{
    /// <summary>
    /// Interaction logic for NavigatableWindow.xaml
    /// </summary>
    public partial class NavigatableWindow : Window, INavigatableWindow
    {
        private INavigationToken navigationToken;
        public NavigatableWindow()
        {
            this.Loaded += (s, e) => this.NavigatableWindow_Loaded(s, e);
        }

        private async Task NavigatableWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.navigationToken != null)
            {
                await this.NavigateAsync(this.navigationToken);
                this.navigationToken = null;
            }
        }

        public IViewResolver ViewResolver { get; set; }

        public ILogger Logger { get; set; }

        public async Task NavigateAsync(INavigationToken token)
        {
            if (this.IsLoaded)
                // await this.navigatableControl.NavigateAsync(token);

                this.navigationToken = token;
        }
    }
}
