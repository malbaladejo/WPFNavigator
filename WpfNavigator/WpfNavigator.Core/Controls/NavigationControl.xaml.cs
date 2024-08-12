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
            var view = await this.ViewResolver.ResolveView(token);
            var viewModel = view.DataContext;

            await InvokeOnNavigatedFromAsync(token);

            await InvokeOnNavigatedToAsync(token, viewModel);

            this.Content = view;
        }
        private async Task InvokeOnNavigatedFromAsync(INavigationToken token)
        {
            var children = this.FindMyselfAndVisualChildren<FrameworkElement>().Reverse();

            foreach (var child in children)
            {
                var viewModel = child.DataContext;

                if (viewModel is INavigationAware<INavigationToken> viewModelNavigationAware)
                {
                    this.Logger.LogInformation($"{viewModel.GetType()} is INavigationAware: Calling OnNavigatedTo.");
                    viewModelNavigationAware.OnNavigatedFrom(token);
                }
                else if (viewModel is INavigationAsyncAware<INavigationToken> viewModelNavigationAsyncAware)
                {
                    this.Logger.LogInformation($"{viewModel.GetType()} is INavigationAsyncAware: Calling OnNavigatedToAsync.");
                    await viewModelNavigationAsyncAware.OnNavigatedFromAsync(token);
                }
            }
        }

        private async Task InvokeOnNavigatedToAsync<TToken>(TToken token, object viewModel) where TToken : INavigationToken
        {
            if (this.TryInvokeOnNavigatedTo<TToken>(token, viewModel))
                return;

            if (await this.TryInvokeOnNavigatedToAsync<TToken>(token, viewModel))
                return;
        }

        private bool TryInvokeOnNavigatedTo<TToken>(TToken token, object viewModel) where TToken : INavigationToken
        {
            var navigationAwareIterfaces = viewModel.GetGenericTypeDefinition(typeof(INavigationAware<>));

            foreach (var navigationAwareIterface in navigationAwareIterfaces)
            {
                if (navigationAwareIterface.GetGenericArguments().Length != 1)
                    continue;

                var genericArg = navigationAwareIterface.GetGenericArguments()[0];
                if (token.GetType() != genericArg)
                    continue;

                this.Logger.LogInformation($"{viewModel.GetType()} is INavigationAware: Calling OnNavigatedTo by reflexion.");
                var method = viewModel.GetType().GetMethod(nameof(INavigationAware<TToken>.OnNavigatedTo));
                if (method == null)
                    continue;

                method.Invoke(viewModel, [token]);
                return true;
            }

            this.Logger.LogInformation($"{viewModel.GetType()} does not implement INavigationAware<{token.GetType()}>.");

            return false;
        }

        private async Task<bool> TryInvokeOnNavigatedToAsync<TToken>(TToken token, object viewModel) where TToken : INavigationToken
        {
            var navigationAwareIterfaces = viewModel.GetGenericTypeDefinition(typeof(INavigationAsyncAware<>));

            foreach (var navigationAwareIterface in navigationAwareIterfaces)
            {
                if (navigationAwareIterface.GetGenericArguments().Length != 1)
                    continue;

                var genericArg = navigationAwareIterface.GetGenericArguments()[0];
                if (token.GetType() != genericArg)
                    continue;

                this.Logger.LogInformation($"{viewModel.GetType()} is INavigationAsyncAware: Calling OnNavigatedToAsync by reflexion.");
                var method = viewModel.GetType().GetMethod(nameof(INavigationAsyncAware<TToken>.OnNavigatedToAsync));
                if (method == null)
                    continue;

                var task = method.Invoke(viewModel, [token]) as Task;
                if (task == null)
                    continue;

                await task;
                return true;
            }

            this.Logger.LogInformation($"{viewModel.GetType()} does not implement INavigationAsyncAware<{token.GetType()}>.");

            return false;
        }

        private INavigationWindow CurrentWindow => this.GetWindow();

        private IViewResolver ViewResolver => this.CurrentWindow.ViewResolver;

        private ILogger Logger => this.CurrentWindow.Logger;
    }
}
