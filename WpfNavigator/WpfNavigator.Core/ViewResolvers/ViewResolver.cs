using System.Windows;
using WpfNavigator.Core.Containers;
using WpfNavigator.Core.Loggers;
using WpfNavigator.Core.Navigation;

namespace WpfNavigator.Core.ViewResolvers
{
    internal class ViewResolver : IViewResolver
    {
        private readonly INavigationContainer navigationContainer;
        private readonly ILogger logger;
        private readonly IContainer container;

        public ViewResolver(
            INavigationContainer navigationContainer,
            ILogger logger,
            IContainer container)
        {
            this.navigationContainer = navigationContainer;
            this.logger = logger;
            this.container = container;
        }

        public async Task<FrameworkElement> ResolveView<TToken>(TToken token) where TToken : INavigationToken
        {
            var viewContainer = this.navigationContainer.Resolve(token);

            var view = this.ResolveView(viewContainer.ViewType);
            var viewModel = this.ResolveViewModel(viewContainer.ViewType);

            view.DataContext = viewModel;

            return view;
        }

        private FrameworkElement ResolveView(Type viewType)
        {
            try
            {
                var view = this.container.Resolve(viewType);
                if (view is FrameworkElement)
                    return (FrameworkElement)view;

                throw new InvalidOperationException($"Try to resolve view {viewType}. {viewType} must be a {nameof(FrameworkElement)}.");
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Try to resolve view {viewType}", ex);
                throw;
            }
        }

        private object ResolveViewModel(Type viewType)
        {
            try
            {
                return this.container.Resolve(viewType);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Try to resolve view-model {viewType}", ex);
                throw;
            }
        }
    }
}
