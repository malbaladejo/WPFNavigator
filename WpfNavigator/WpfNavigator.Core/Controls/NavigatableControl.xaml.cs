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
    /// Interaction logic for NavigatableControl.xaml
    /// </summary>
    public partial class NavigatableControl : UserControl
    {
        public NavigatableControl()
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
                typeof(NavigatableControl),
                new FrameworkPropertyMetadata(null, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                {
                    var navigatableControl = (NavigatableControl)d;
                    navigatableControl.OnNavigationTokenChanged(e.NewValue as INavigationToken);
                }));

        private void OnNavigationTokenChanged(INavigationToken? newValue)
        {
            this.NavigateAsync(newValue);
        }

        public async Task NavigateAsync<TToken>(TToken token) where TToken : INavigationToken
        {
            var view = await this.ViewResolver.ResolveView(token);
            var viewModel = view.DataContext;

            await InvokeOnNavigatedFromAsync(token);

            await InvokeOnNavigatedToAsync(token, viewModel);

            this.Content = view;
        }
        private async Task InvokeOnNavigatedFromAsync<TToken>(TToken token) where TToken : INavigationToken
        {
            var children = this.FindMyselfAndVisualChildren<FrameworkElement>().Reverse();

            foreach (var child in children)
            {
                var viewModel = child.DataContext;

                if (viewModel is INavigationAware<TToken> viewModelNavigationAware)
                {
                    this.Logger.LogInformation($"{viewModel.GetType()} is INavigationAware: Calling OnNavigatedTo.");
                    viewModelNavigationAware.OnNavigatedFrom(token);
                }
                else if (viewModel is INavigationAsyncAware<TToken> viewModelNavigationAsyncAware)
                {
                    this.Logger.LogInformation($"{viewModel.GetType()} is INavigationAsyncAware: Calling OnNavigatedToAsync.");
                    await viewModelNavigationAsyncAware.OnNavigatedFromAsync(token);
                }
            }
        }

        private async Task InvokeOnNavigatedToAsync<TToken>(TToken token, object viewModel) where TToken : INavigationToken
        {
            if (viewModel is INavigationAware<TToken> viewModelNavigationAware)
            {
                this.Logger.LogInformation($"{viewModel.GetType()} is INavigationAware: Calling OnNavigatedTo.");
                viewModelNavigationAware.OnNavigatedTo(token);
            }
            else if (viewModel is INavigationAsyncAware<TToken> viewModelNavigationAsyncAware)
            {
                this.Logger.LogInformation($"{viewModel.GetType()} is INavigationAsyncAware: Calling OnNavigatedToAsync.");
                await viewModelNavigationAsyncAware.OnNavigatedToAsync(token);
            }
        }

        private INavigatableWindow CurrentWindow
        {
            get
            {
                var window = Window.GetWindow(this) as INavigatableWindow;

                if (window == null)
                    throw new InvalidOperationException($"{nameof(NavigatableControl)} must be used in {nameof(INavigatableWindow)}");

                return window;
            }
        }

        private IViewResolver ViewResolver => this.CurrentWindow.ViewResolver;
        private ILogger Logger => this.CurrentWindow.Logger;
    }
}
