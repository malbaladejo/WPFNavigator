namespace WpfNavigator.Core.Navigation
{
    public interface INavigationService
    {
        Task NavigateAsync(INavigationToken token);
    }
}
