﻿using WpfNavigator.Core.Windows;

namespace WpfNavigator.Core.Navigation
{
    internal class NavigationService : INavigationService
    {
        private readonly IWindowController windowController;

        public NavigationService(IWindowController windowController)
        {
            this.windowController = windowController;
        }

        public async Task NavigateAsync<TToken>(TToken token) where TToken : INavigationToken
        {
            var window = this.windowController.GetWidow(token);
            window.Show();

            if (token is NewWindowNavigationToken newWindowNavigationToken)
                await window.NavigateAsync(newWindowNavigationToken.Target);
            else
                await window.NavigateAsync(token);
        }
    }
}
