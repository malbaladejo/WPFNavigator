using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfNavigator.Core.Navigation;

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

        [RelayCommand]
        private void OpenNewWindow()
        {
            this.navigationService.NavigateAsync(new HomeNavigationToken().OpenInNewWindow());
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
