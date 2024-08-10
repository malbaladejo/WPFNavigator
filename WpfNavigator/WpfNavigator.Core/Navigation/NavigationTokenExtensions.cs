namespace WpfNavigator.Core.Navigation
{
    public static class NavigationTokenExtensions
    {
        public static INavigationToken OpenInNewWindow(this INavigationToken token)
            => new NewWindowNavigationToken(token);
    }
}
