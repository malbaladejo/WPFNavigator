using System.Windows;
using WpfNavigator.Core.Loggers;
using WpfNavigator.Core.Navigation;
using WpfNavigator.Core.ViewResolvers;

namespace WpfNavigator.Core.Windows
{
    /// <summary>
    /// Interaction logic for NavigationWindow.xaml
    /// </summary>
    public partial class NavigationWindow : Window, INavigationWindow
    {
        //private INavigationToken? navigationToken;
        public NavigationWindow(ILogger logger, IViewResolver viewResolver)
        {
            this.InitializeComponent();
            this.Logger = logger;
            this.ViewResolver = viewResolver;
        }

        public IViewResolver ViewResolver { get; }

        public INavigationService NavigationService { get; set; }

        public ILogger Logger { get; }

        public async Task NavigateAsync<TToken>(TToken token) where TToken : INavigationToken
        {
            await this.navigatableControl.NavigateAsync(token);
        }
    }
}
