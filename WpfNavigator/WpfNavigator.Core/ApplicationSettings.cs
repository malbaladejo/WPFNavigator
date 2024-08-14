using WpfNavigator.Core.Modules;
using WpfNavigator.Core.Navigation;

namespace WpfNavigator.Core
{
    internal class ApplicationSettings : IApplicationSettings
    {
        private readonly List<Type> modules = new List<Type>();

        public IApplicationSettings RegisterStartupToken<TToken>(TToken token) where TToken : INavigationToken
        {
            this.StartupToken = token;
            return this;
        }

        public IApplicationSettings DefaultWindowSize(double width, double height)
        {
            this.WindowWidth = width;
            this.WindowHeight = height;
            return this;
        }

        public IApplicationSettings RegisterModule<TModule>() where TModule : IModule
        {
            this.modules.Add(typeof(TModule));
            return this;
        }

        public INavigationToken? StartupToken { get; private set; }

        public double? WindowWidth { get; private set; }

        public double? WindowHeight { get; private set; }

        public IEnumerable<Type> ModuleTypes => this.modules;
    }
}
