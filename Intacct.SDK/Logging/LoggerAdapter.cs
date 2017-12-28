using System;
using Microsoft.Extensions.Logging;

namespace Intacct.SDK.Logging
{
    public class LoggerAdapter : ILoggerAdapter
    {
        private readonly ILogger _logger;
        
        public LoggerAdapter(ILogger logger)
        {
            _logger = logger;
        }

        public void LogTrace(string message)
        {
            _logger.LogTrace(message);
        }

        public void LogDebug(string message)
        {
            _logger.LogDebug(message);
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }

        public void LogError(string message)
        {
            _logger.LogError(message);
        }
        public void LogError(string message, Exception exception)
        {
            _logger.LogError(message, exception);
        }

        public void LogCritical(string message)
        {
            _logger.LogCritical(message);
        }
    }
}