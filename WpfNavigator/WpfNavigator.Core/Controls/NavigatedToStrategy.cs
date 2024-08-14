using WpfNavigator.Core.Extensions;
using WpfNavigator.Core.Loggers;
using WpfNavigator.Core.Navigation;

namespace WpfNavigator.Core.Controls
{
    internal class NavigatedToStrategy : INavigatedToStrategy
    {
        private readonly ILogger logger;

        public NavigatedToStrategy(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task OnNavigatedToAsync<TToken>(TToken token, object viewModel) where TToken : INavigationToken
        {
            if (this.TryInvokeOnNavigatedTo(token, viewModel))
                return;

            await this.TryInvokeOnNavigatedToAsync(token, viewModel);
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

                this.logger.LogInformation($"{viewModel.GetType()} is INavigationAware: Calling OnNavigatedTo by reflexion.");
                var method = viewModel.GetType().GetMethod(nameof(INavigationAware<TToken>.OnNavigatedTo));
                if (method == null)
                    continue;

                method.Invoke(viewModel, [token]);
                return true;
            }

            this.logger.LogInformation($"{viewModel.GetType()} does not implement INavigationAware<{token.GetType()}>.");

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

                this.logger.LogInformation($"{viewModel.GetType()} is INavigationAsyncAware: Calling OnNavigatedToAsync by reflexion.");
                var method = viewModel.GetType().GetMethod(nameof(INavigationAsyncAware<TToken>.OnNavigatedToAsync));
                if (method == null)
                    continue;

                var task = method.Invoke(viewModel, [token]) as Task;
                if (task == null)
                    continue;

                await task;
                return true;
            }

            this.logger.LogInformation($"{viewModel.GetType()} does not implement INavigationAsyncAware<{token.GetType()}>.");

            return false;
        }
    }
}
