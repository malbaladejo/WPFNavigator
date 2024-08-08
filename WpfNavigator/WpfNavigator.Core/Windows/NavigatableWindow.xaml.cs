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
        //private INavigationToken? navigationToken;
        public NavigatableWindow()
        {
            this.InitializeComponent();
        }

        public IViewResolver ViewResolver { get; set; }

        public ILogger Logger { get; set; }

        public async Task NavigateAsync(INavigationToken token)
        {
            await this.navigatableControl.NavigateAsync(token);
        }
    }
}
