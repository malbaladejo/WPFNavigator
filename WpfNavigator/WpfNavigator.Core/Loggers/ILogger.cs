namespace WpfNavigator.Core.Loggers
{
    public interface ILogger
    {
        void LogDebug(string message);
        void LogInformation(string message);
        void LogWarning(string message);

        void LogError(string message);
        void LogError(string message, Exception exception);
        void LogError(Exception exception);
    }
}
