using System.Runtime.CompilerServices;
using NLog;

namespace api.Core.Utilities
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message, [CallerMemberName]string name = "")
        {
            logger.Debug($"{name} | {message}");
        }

        public void LogError(string message, [CallerMemberName]string name = "")
        {
            logger.Error($"{name} | {message}");
        }

        public void LogInfo(string message, [CallerMemberName]string name = "")
        {
            logger.Info($"{name} | {message}");
        }

        public void LogWarn(string message, [CallerMemberName]string name = "")
        {
            logger.Warn($"{name} | {message}");
        }
    }
}