using System;
using Microsoft.Extensions.Logging;

namespace Intacct.SDK.Logging
{
    public interface ILoggerAdapter
    {
        void LogTrace(string message);
        
        void LogDebug(string message);
        
        void LogInformation(string message);
        
        void LogWarning(string message);
        
        void LogError(string message);
        void LogError(string message, Exception exception);
        
        void LogCritical(string message);
    }
}