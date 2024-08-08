using WpfNavigator.Core;
using WpfNavigator.Core.Containers;
using WpfNavigator.Core.Loggers;

namespace WpfNavigator.Bootstrapper
{
    public class WpfNavigatorBootstrapper : WpfNavigatorBoostraperCore
    {
        protected override IContainer CreateContainer() => new UnityContainerWrapper();

        protected override ILogger CreateLogger() => new Log4NetWrapper();
    }
}
