namespace WpfNavigator.Core.Navigation
{
    public class RegionNavigationToken : INavigationToken
    {
        public RegionNavigationToken(INavigationToken target, string regionName)
        {
            this.Target = target;
            this.RegionName = regionName;
        }

        public string Icon => this.Target.Icon;

        public string Label => this.Target.Label;

        public INavigationToken Target { get; }

        public string RegionName { get; }
    }

}
