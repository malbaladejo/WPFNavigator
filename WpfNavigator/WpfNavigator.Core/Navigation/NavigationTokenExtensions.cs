using System.Windows;

namespace WpfNavigator.Core.Navigation
{
    public static class NavigationTokenExtensions
    {
        public static NewWindowNavigationToken OpenInNewWindow(this INavigationToken token)
            => new NewWindowNavigationToken(token);

        public static RegionNavigationToken OpenInRegion(this INavigationToken token, string regionName)
             => new RegionNavigationToken(token, regionName);

        public static NewWindowNavigationToken Size(this NewWindowNavigationToken token, double width, double height)
        {
            token.Width = width;
            token.Height = height;
            return token;
        }

        public static NewWindowNavigationToken Width(this NewWindowNavigationToken token, double width)
        {
            token.Width = width;
            return token;
        }

        public static NewWindowNavigationToken Height(this NewWindowNavigationToken token, double height)
        {
            token.Height = height;
            return token;
        }

        public static NewWindowNavigationToken WindowStartupLocation(this NewWindowNavigationToken token, WindowStartupLocation windowStartupLocation)
        {
            token.WindowStartupLocation = windowStartupLocation;
            return token;
        }

        public static NewWindowNavigationToken SizeToContent(this NewWindowNavigationToken token, SizeToContent sizeToContent)
        {
            token.SizeToContent = sizeToContent;
            return token;
        }

        public static NewWindowNavigationToken CurrentWindowAsOwner(this NewWindowNavigationToken token)
        {
            token.CurrentWindowAsOwner = true;
            return token;
        }

        public static NewWindowNavigationToken Owner(this NewWindowNavigationToken token, Window owner)
        {
            token.Owner = owner;
            return token;
        }
    }
}
