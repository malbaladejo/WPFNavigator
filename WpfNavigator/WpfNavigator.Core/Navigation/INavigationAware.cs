namespace WpfNavigator.Core.Navigation
{
    public interface INavigationAware<TToken> where TToken : INavigationToken
    {
        void OnNavigatedTo(TToken token);
        void OnNavigatedFrom(INavigationToken nextToken);
    }
}
