using System.Runtime.CompilerServices;

namespace api.Core.Utilities
{
    public interface ILoggerManager
    {
        void LogInfo(string message, [CallerMemberName]string name = "");
        void LogWarn(string message, [CallerMemberName]string name = "");
        void LogDebug(string message, [CallerMemberName]string name = "");
        void LogError(string message, [CallerMemberName]string name = "");
    }
}