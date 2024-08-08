namespace WpfNavigator.Core.Navigation
{
    public interface INavigationAsyncAware<TToken> where TToken : INavigationToken
    {
        Task OnNavigatedToAsync(TToken token);
        Task OnNavigatedFromAsync(INavigationToken nextToken);
    }
}
