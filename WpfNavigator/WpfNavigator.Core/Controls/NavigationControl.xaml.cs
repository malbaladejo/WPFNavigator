using System.Windows;
using System.Windows.Controls;
using WpfNavigator.Core.Extensions;
using WpfNavigator.Core.Loggers;
using WpfNavigator.Core.Navigation;
using WpfNavigator.Core.ViewResolvers;
using WpfNavigator.Core.Windows;

namespace WpfNavigator.Core.Controls
{
    /// <summary>
    /// Interaction logic for NavigationControl.xaml
    /// </summary>
    public partial class NavigationControl : UserControl
    {
        public NavigationControl()
        {
            InitializeComponent();
        }

        public INavigationToken NavigationToken
        {
            get { return (INavigationToken)GetValue(NavigationTokenProperty); }
            set { SetValue(NavigationTokenProperty, value); }
        }

        public static readonly DependencyProperty NavigationTokenProperty =
            DependencyProperty.Register(
                nameof(NavigationToken),
                typeof(INavigationToken),
                typeof(NavigationControl),
                new FrameworkPropertyMetadata(null, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                {
                    var navigatableControl = (NavigationControl)d;
                    navigatableControl.OnNavigationTokenChanged(e.NewValue as INavigationToken);
                }));

        private void OnNavigationTokenChanged(INavigationToken? newValue)
        {
            this.NavigateAsync(newValue);
        }

        public async Task NavigateAsync<TToken>(TToken token) where TToken : INavigationToken
        {
            this.GetTargetNavigationControl(token, out var target, out var targetToken);

            await target.ExecNavigateAsync(targetToken);
        }

        private async Task ExecNavigateAsync(INavigationToken token)
        {
            var view = await this.ViewResolver.ResolveView(token);
            var viewModel = view.DataContext;

            await this.NavigatedFromStrategy.OnNavigatedFromAsync(token, this);

            await this.NavigatedToStrategy.OnNavigatedToAsync(token, viewModel);

            this.Content = view;
        }

        private void GetTargetNavigationControl<TToken>(TToken token, out NavigationControl target, out INavigationToken targtToken) where TToken : INavigationToken
        {
            target = this;
            targtToken = token;

            if (token is RegionNavigationToken regionNavigationToken)
            {
                target = this.FindMyselfAndVisualChildren<NavigationControl>().FirstOrDefault(x => x.Name == regionNavigationToken.RegionName);
                targtToken = regionNavigationToken.Target;
                if (target == null)
                    throw new InvalidOperationException($"No region '{regionNavigationToken.RegionName}' found.");
            }
        }

        private INavigationWindow CurrentWindow => this.GetWindow();

        private IViewResolver ViewResolver => this.CurrentWindow.ViewResolver;

        private INavigatedToStrategy NavigatedToStrategy => this.CurrentWindow.Container.Resolve<INavigatedToStrategy>();

        private INavigatedFromStrategy NavigatedFromStrategy => this.CurrentWindow.Container.Resolve<INavigatedFromStrategy>();

        private ILogger Logger => this.CurrentWindow.Logger;
    }
}
