using WpfNavigator.Core.Modules;
using WpfNavigator.Core.Navigation;

namespace WpfNavigator.Core
{
    public interface IApplicationSettings
    {
        IApplicationSettings RegisterStartupToken<TToken>(TToken token) where TToken : INavigationToken;

        IApplicationSettings DefaultWindowSize(double width, double height);

        IApplicationSettings RegisterModule<TModule>() where TModule : IModule;

        INavigationToken? StartupToken { get; }

        double? WindowWidth { get; }

        double? WindowHeight { get; }

        IEnumerable<Type> ModuleTypes { get; }
    }
}
