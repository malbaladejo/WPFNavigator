namespace WpfNavigator.Core.Navigation
{
    public class NewWindowNavigationToken : INavigationToken
    {
        public NewWindowNavigationToken(INavigationToken target)
        {
            this.Target = target;
        }

        public string Icon => this.Target.Icon;

        public string Label => this.Target.Label;

        public INavigationToken Target { get; }
    }
}
