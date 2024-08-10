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
        public NavigatableWindow(ILogger logger, IViewResolver viewResolver)
        {
            this.InitializeComponent();
            this.Logger = logger;
            this.ViewResolver = viewResolver;
        }

        public IViewResolver ViewResolver { get; }

        public ILogger Logger { get; }

        public async Task NavigateAsync<TToken>(TToken token) where TToken : INavigationToken
        {
            await this.navigatableControl.NavigateAsync(token);
        }
    }
}
