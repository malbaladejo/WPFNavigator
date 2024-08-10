namespace WpfNavigator.Core.Navigation
{
    public interface INavigationService
    {
        Task NavigateAsync<TToken>(TToken token) where TToken : INavigationToken;
    }
}
