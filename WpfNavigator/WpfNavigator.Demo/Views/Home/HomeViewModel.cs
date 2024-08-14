﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WpfNavigator.Core.Navigation;
using WpfNavigator.Demo.Views.ViewA;
using WpfNavigator.Demo.Views.ViewB;

namespace WpfNavigator.Demo.Views.Home
{
    internal partial class HomeViewModel : ObservableObject, INavigationAware<HomeNavigationToken>
    {
        private readonly INavigationService navigationService;

        [ObservableProperty]
        private string? label;

        public HomeViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public INavigationToken HomeToken { get; } = new HomeNavigationToken();

        public INavigationToken ViewAToken { get; } = new ViewANavigationToken().OpenInRegion("MainContent");
        public INavigationToken ViewBToken { get; } = new ViewBNavigationToken().OpenInRegion("MainContent");

        [RelayCommand]
        private void OpenNewWindow()
        {
            this.navigationService.NavigateAsync(new HomeNavigationToken().OpenInNewWindow());
        }

        [RelayCommand]
        private void OpenBigWindow()
        {
            this.navigationService.NavigateAsync(
                new HomeNavigationToken()
                .OpenInNewWindow()
                .Width(2048)
                .Height(1024));
        }

        [RelayCommand]
        private void OpenCenterScreenWindow()
        {
            this.navigationService.NavigateAsync(
                new HomeNavigationToken()
                .OpenInNewWindow()
                .WindowStartupLocation(WindowStartupLocation.CenterScreen));
        }

        [RelayCommand]
        private void OpenCenterOwnerWindow()
        {
            this.navigationService.NavigateAsync(
                new HomeNavigationToken()
                .OpenInNewWindow()
                .CurrentWindowAsOwner()
                .WindowStartupLocation(WindowStartupLocation.CenterOwner));
        }

        [RelayCommand]
        private void Navigate()
        {
            this.navigationService.NavigateAsync(new HomeNavigationToken());
        }

        public void OnNavigatedTo(HomeNavigationToken token)
        {
            this.Label = token.Label;
        }

        public void OnNavigatedFrom(INavigationToken nextToken)
        {
            // nothing to do
        }
    }
}
