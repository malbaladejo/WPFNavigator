using System.Windows;
using WpfNavigator.Core.Navigation;

namespace WpfNavigator.Core.ViewResolvers
{
    public interface IViewResolver
    {
        Task<FrameworkElement> ResolveView<TToken>(TToken token) where TToken : INavigationToken;
    }
}