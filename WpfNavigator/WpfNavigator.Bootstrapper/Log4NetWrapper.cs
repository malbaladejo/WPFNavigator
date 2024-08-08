using log4net;
using WpfNavigator.Core.Loggers;

namespace WpfNavigator.Bootstrapper
{
    internal class Log4NetWrapper : ILogger
    {
        private readonly ILog logger;
        public Log4NetWrapper()
        {
            this.logger = LogManager.GetLogger(typeof(Log4NetWrapper));
        }

        public void LogDebug(string message) => this.logger.Debug(message);

        public void LogError(string message) => this.logger.Error(message);

        public void LogError(string message, Exception exception) => this.LogError(message, exception);

        public void LogError(Exception exception) => this.logger.Error(exception);

        public void LogInformation(string message) => this.logger.Info(message);

        public void LogWarning(string message) => this.logger.Warn(message);
    }
}
